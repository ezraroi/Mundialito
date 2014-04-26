angular.module('mundialitoApp').controller('DashboardCtrl', ['$scope','$log', 'security','GamesManager', function ($scope, $log, Security, GamesManager) {
    Security.authenticate();

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