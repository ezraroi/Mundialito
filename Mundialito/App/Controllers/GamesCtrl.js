angular.module('mundialitoApp')
.controller('GamesCtrl', ['$scope', '$log', 'GamesService', 'security', 'Alert', function ($scope, $log, GamesService, Security, Alert) {
    Security.authenticate();
    
    GamesService.getGames().success(function (data, status, headers, config) {
        $log.debug("GamesService.getGames (" + status + "): " + angular.toJson(data));
        $scope.games = data;
    });

    $scope.gamesFilter = "OnlyOpen";
    
    
}]);