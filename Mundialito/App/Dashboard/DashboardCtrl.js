'use strict';
angular.module('mundialitoApp').controller('DashboardCtrl', ['$scope','$log','$location','$timeout','GamesManager','UsersManager','GeneralBetsManager', function ($scope, $log, $location, $timeout, GamesManager, UsersManager, GeneralBetsManager) {
    $scope.generalBetsAreOpen = false;
    $scope.submittedGeneralBet = true;

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
        $scope.generalBetsAreOpen = data === 'true';
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
            {field:'Place', displayName:'', resizable: false, width: 25},
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