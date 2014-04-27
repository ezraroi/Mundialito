'use strict';
angular.module('mundialitoApp').controller('DashboardCtrl', ['$scope','$log', 'GamesManager', function ($scope, $log, GamesManager) {
    GamesManager.loadAllGames().then(function(games) {
        $scope.games = games;
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

}]);