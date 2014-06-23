'use strict';
angular.module('mundialitoApp').controller('DashboardCtrl', ['$scope','$log','$location','$timeout','GamesManager','UsersManager','GeneralBetsManager','teams', function ($scope, $log, $location, $timeout, GamesManager, UsersManager, GeneralBetsManager, teams) {
    $scope.generalBetsAreOpen = false;
    $scope.submittedGeneralBet = true;
    $scope.pendingUpdateGames = false;

    $scope.teamsDic = {};

    for(var i=0; i<teams.length; i++) {
        $scope.teamsDic[teams[i].TeamId] = teams[i];
    }

    GamesManager.loadAllGames().then(function(games) {
        $scope.games = games;
        $scope.pendingUpdateGames = _.findWhere($scope.games,{IsPendingUpdate: true}) !== undefined;
    });

    var userHasGeneralBet = function() {
        if (!angular.isDefined($scope.security.user) || ($scope.security.user == null))
        {
            $log.debug('DashboardCtrl: user info not loaded yet, will retry in 1 second');
            $timeout(userHasGeneralBet,1000);
        }
        else {
            GeneralBetsManager.hasGeneralBet($scope.security.user.userName).then(function (data) {
                $scope.submittedGeneralBet = data === 'true';
            });
        }
    };

    userHasGeneralBet();

    GeneralBetsManager.canSubmtiGeneralBet().then(function(data) {
        $scope.generalBetsAreOpen = (data === 'true');
        if (!$scope.generalBetsAreOpen) {
            GeneralBetsManager.loadAllGeneralBets().then(function(data) {
                $scope.generalBets = data;
                $scope.winningTeams = {};
                for (var i=0; i < $scope.generalBets.length ; i++ ){
                    if (!angular.isDefined($scope.winningTeams[$scope.generalBets[i].WinningTeamId])) {
                        $scope.winningTeams[$scope.generalBets[i].WinningTeamId] = 0;
                    }
                    $scope.winningTeams[$scope.generalBets[i].WinningTeamId] += 1;
                }

                var chart1 = {};
                chart1.type = "PieChart";
                chart1.options = {
                    displayExactValues: true,
                    is3D: true,
                    backgroundColor: { fill:'transparent' },
                    chartArea: {left:10,top:20,bottom:0,height:"100%"},
                    title: 'Winning Team Bets Distribution'
                };
                chart1.data = [
                    ['Team', 'Number Of Users']
                ];
                for (var teamId in $scope.winningTeams) {
                    chart1.data.push([$scope.teamsDic[teamId].Name, $scope.winningTeams[teamId]]);
                }
                $scope.chart = chart1;
            });
        }
    });

    UsersManager.loadAllUsers().then(function(users) {
        $scope.users = users;
    });

    $scope.isOpenForBetting = function() {
        return function( item ) {
            return item.IsOpen;
        };
    };

    $scope.isPendingUpdate = function() {
        return function( item ) {
            return item.IsPendingUpdate;
        };
    };

    $scope.isDecided = function() {
        return function( item ) {
            return !item.IsOpen && !item.IsPendingUpdate;
        };
    };

    $scope.gridOptions = {
        data: 'users',
        rowTemplate:'<div style="height: 100%" ng-class="{\'text-primary\': row.getProperty(\'Username\') === security.user.userName}"><div ng-style="{ \'cursor\': row.cursor }" ng-repeat="col in renderedColumns" ng-class="col.colIndex()" class="ngCell ">' +
            '<div class="ngVerticalBar" ng-style="{height: rowHeight}" ng-class="{ ngVerticalBarVisible: !$last }"> </div>' +
            '<div ng-cell></div>' +
            '</div></div>',
        columnDefs: [
            {field:'Place', displayName:'', resizable: false, width: 30},
            {field:'Name', displayName:'Name', resizable: true},
            {field:'Results', displayName:'Results', resizable: true},
            {field:'Marks', displayName:'Marks', resizable: true},
            {field:'YellowCards', displayName:'Yellow Cards Marks', resizable: true},
            {field:'Corners', displayName:'Corners Marks', resizable: true},
            {field:'Points', displayName:'Points', resizable: true},
            {field:'PlaceDiff', displayName:'', resizable: false, width: 50, cellTemplate: '<div ng-class="{\'text-success\': row.getProperty(col.field).indexOf(\'+\') !== -1, \'text-danger\': (row.getProperty(col.field).indexOf(\'+\') === -1) && (row.getProperty(col.field) !== \'0\')}"><div class="ngCellText">{{row.getProperty(col.field)}}</div></div>'}
        ],
        plugins: [new ngGridFlexibleHeightPlugin()],
        multiSelect: false,
        afterSelectionChange: function (rowItem) {
            if (rowItem.selected)  {
                $location.path(rowItem.entity.getUrl())
            }
        }
    };

}]);