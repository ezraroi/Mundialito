'use strict';
angular.module('mundialitoApp').controller('DashboardCtrl', ['$scope','$log','$location','GamesManager','UsersManager','GeneralBetsManager','security', function ($scope, $log, $location, GamesManager, UsersManager, GeneralBetsManager, security) {
    $scope.generalBetsAreOpen = false;

    GamesManager.loadAllGames().then(function(games) {
        $scope.games = games;
    });

    GeneralBetsManager.hasGeneralBet(security.user.userName).then(function(data) {
        $scope.submittedGeneralBet = data === 'true';
    });

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
            return !item.IsOpen && !item.IsPendingUpdate;;
        };
    };

    $scope.gridOptions = {
        data: 'users',
        columnDefs: [
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