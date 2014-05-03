'use strict';
angular.module('mundialitoApp').controller('DashboardCtrl', ['$scope','$log', 'GamesManager','UsersManager', function ($scope, $log, GamesManager, UsersManager) {
    GamesManager.loadAllGames().then(function(games) {
        $scope.games = games;
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
            {field:'Points', displayName:'Points'}
        ],
        plugins: [new ngGridFlexibleHeightPlugin()],
        multiSelect: false
    };

}]);