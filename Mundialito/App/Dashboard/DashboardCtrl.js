'use strict';
angular.module('mundialitoApp').controller('DashboardCtrl', ['$scope','$log','$location','$timeout','GamesManager','UsersManager','GeneralBetsManager','teams', function ($scope, $log, $location, $timeout, GamesManager, UsersManager, GeneralBetsManager, teams) {
    $scope.generalBetsAreOpen = false;
    $scope.submittedGeneralBet = true;

    $scope.teamsDic = {};

    for(var i=0; i<teams.length; i++) {
        $scope.teamsDic[teams[i].TeamId] = teams[i];
    }

    GamesManager.loadAllGames().then(function(games) {
        $scope.games = games;
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

    $scope.isDecided = function() {
        return function( item ) {
            return !item.IsOpen && !item.IsPendingUpdate;
        };
    };

    $scope.gridOptions = {
        data: 'users',
        columnDefs: [
            {field:'Place', displayName:'', resizable: false, width: 30},
            {field:'Name', displayName:'Name'},
            {field:'Results', displayName:'Results'},
            {field:'Marks', displayName:'Marks'},
            {field:'YellowCards', displayName:'Yellow Cards Marks'},
            {field:'Corners', displayName:'Corners Marks'},
            {field:'Points', displayName:'Points'}
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