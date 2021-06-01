'use strict';
angular.module('mundialitoApp').controller('DashboardCtrl', ['$scope', '$log', '$location', '$timeout', 'GamesManager', 'UsersManager', 'GeneralBetsManager', 'teams', 'players',
    function ($scope, $log, $location, $timeout, GamesManager, UsersManager, GeneralBetsManager, teams, players) {
    $scope.generalBetsAreOpen = false;
    $scope.submittedGeneralBet = true;
    $scope.pendingUpdateGames = false;

    $scope.teamsDic = {};
    $scope.playersDic = {};

    for (var i = 0; i < teams.length; i++) {
        $scope.teamsDic[teams[i].TeamId] = teams[i];
    }

    for (var i = 0; i < players.length; i++) {
        $scope.playersDic[players[i].PlayerId] = players[i];
    }

    GamesManager.loadAllGames().then(function (games) {
        $scope.games = games;
        $scope.pendingUpdateGames = _.findWhere($scope.games, { IsPendingUpdate: true }) !== undefined;
    });

    var userHasGeneralBet = function () {
        if (!angular.isDefined($scope.security.user) || ($scope.security.user == null)) {
            $log.debug('DashboardCtrl: user info not loaded yet, will retry in 1 second');
            $timeout(userHasGeneralBet, 1000);
        }
        else {
            GeneralBetsManager.hasGeneralBet($scope.security.user.userName).then(function (data) {
                $scope.submittedGeneralBet = data === true;
            });
        }
    };

    userHasGeneralBet();

    GeneralBetsManager.canSubmtiGeneralBet().then(function (data) {
        $scope.generalBetsAreOpen = (data === true);
        if (!$scope.generalBetsAreOpen) {
            GeneralBetsManager.loadAllGeneralBets().then(function (data) {
                $scope.generalBets = data;
                $scope.winningTeams = {};
                $scope.winningPlayers = {};
                for (var i = 0; i < $scope.generalBets.length ; i++) {
                    if (!angular.isDefined($scope.winningTeams[$scope.generalBets[i].WinningTeamId])) {
                        $scope.winningTeams[$scope.generalBets[i].WinningTeamId] = 0;
                    }
                    $scope.winningTeams[$scope.generalBets[i].WinningTeamId] += 1;
                    if (!angular.isDefined($scope.winningPlayers[$scope.generalBets[i].GoldenBootPlayerId])) {
                        $scope.winningPlayers[$scope.generalBets[i].GoldenBootPlayerId] = 0;
                    }
                    $scope.winningPlayers[$scope.generalBets[i].GoldenBootPlayerId] += 1;
                }

                var chart1 = {};
                chart1.type = "PieChart";
                chart1.options = {
                    displayExactValues: true,
                    is3D: true,
                    backgroundColor: { fill: 'transparent' },
                    chartArea: { left: 10, top: 20, bottom: 0, height: "100%" },
                    title: 'Winning Team Bets Distribution'
                };
                chart1.data = [
                    ['Team', 'Number Of Users']
                ];
                for (var teamId in $scope.winningTeams) {
                    chart1.data.push([$scope.teamsDic[teamId].Name, $scope.winningTeams[teamId]]);
                }
                $scope.chart = chart1;
                chart1 = {};
                chart1.type = "PieChart";
                chart1.options = {
                    displayExactValues: true,
                    is3D: true,
                    backgroundColor: { fill: 'transparent' },
                    chartArea: { left: 10, top: 20, bottom: 0, height: "100%" },
                    title: 'Winning Golden Boot Player Bets Distribution'
                };
                chart1.data = [
                    ['Player', 'Number Of Users']
                ];
                for (var playerId in $scope.winningPlayers) {
                    chart1.data.push([$scope.playersDic[playerId].Name, $scope.winningPlayers[playerId]]);
                }
                $scope.playersChart = chart1;
            });
        }
    });

    UsersManager.getTable().then(function (users) {
        $scope.users = users;
    });

    $scope.isOpenForBetting = function () {
        return function (item) {
            return item.IsOpen;
        };
    };

    $scope.isPendingUpdate = function () {
        return function (item) {
            return item.IsPendingUpdate;
        };
    };

    $scope.isDecided = function () {
        return function (item) {
            return !item.IsOpen && !item.IsPendingUpdate;
        };
    };

    function getRowTemplate() {
        var rowtpl = '<div ng-click="grid.appScope.goToUser(row)" style="cursor: pointer" ng-class="{\'text-primary\':row.entity.Username===grid.appScope.security.user.userName }"><div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader }" ui-grid-cell></div></div>';
        return rowtpl;
    }

    $scope.gridOptions = {
        data: 'users',
        saveWidths: true,
        saveVisible: true,
        saveOrder: true,
        enableRowSelection: false,
        enableSelectAll: false,
        multiSelect: false,
        rowTemplate: getRowTemplate(),
        columnDefs: [
            { field: 'Place', displayName: '', resizable: false, maxWidth: 30 },
            { field: 'Name', displayName: 'Name', resizable: true, minWidth: 150},
            { field: 'Results', displayName: 'Results', resizable: true },
            { field: 'Marks', displayName: 'Marks', resizable: true },
            { field: 'Points', displayName: 'Points', resizable: true },
            { field: 'PlaceDiff', displayName: 'Position Delta', resizable: true, cellTemplate: '<div ng-class="{\'text-success\': COL_FIELD.indexOf(\'+\') !== -1, \'text-danger\': (COL_FIELD.indexOf(\'+\') === -1) && (COL_FIELD !== \'0\')}"><div class="ngCellText">{{::COL_FIELD}}</div></div>' }
        ],
        onRegisterApi: function(gridApi){
            $scope.gridApi = gridApi;
            $scope.gridApi.colResizable.on.columnSizeChanged($scope, saveState);
            $scope.gridApi.core.on.columnVisibilityChanged($scope, saveState);
            $scope.gridApi.core.on.sortChanged($scope, saveState);
        }
    };

    function saveState() {
        var state = $scope.gridApi.saveState.save();
        localStorage.setItem('gridState', state);
    };

    function restoreState() {
        $timeout(function () {
            var state = localStorage.getItem('gridState');
            if (state) $scope.gridApi.saveState.restore($scope, state);
        });
    };

    $scope.getTableHeight = function () {
        var rowHeight = 30; // your row height
        var headerHeight = 30; // your header height
        var total = ( ($scope.users ? $scope.users.length : 0) * rowHeight + headerHeight);
        $log.debug('Total Height: ' + total);
        return {
            height: total + "px"
        };
    };

    $scope.goToUser = function (rowItem) {
        $location.path(rowItem.entity.getUrl())
    }

}]);
