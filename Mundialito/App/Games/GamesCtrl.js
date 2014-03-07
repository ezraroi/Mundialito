angular.module('mundialitoApp').controller('GamesCtrl', ['$scope', '$rootScope', '$filter', '$log', 'GamesService', 'security', 'games','teams','stadiums', function ($scope, $rootScope, $filter, $log, GamesService, Security, games, teams, stadiums) {
    Security.authenticate();
    $scope.newGame = null;
    $scope.gamesFilter = "All";
    $scope.games = games;
    $scope.teams = teams;
    $scope.stadiums = stadiums;
    $scope.pendingUpdateGames = $filter('pendingUpdateGamesFilter')(games);

    $scope.addNewGame = function () {
        $log.debug('GamesCtrl: addNewGame clicked');
        $('.selectpicker').selectpicker('refresh');
        $scope.newGame = GamesService.getEmptyGameObject();
    };

    var refreshGamesBind = $rootScope.$on('refreshGames', function () {
        $log.debug("GamesCtrl: got 'refreshGames' event");
        GamesService.getGames().success(function (data, status) {
            $scope.games = data;
            $scope.pendingUpdateGames = $filter('pendingUpdateGamesFilter')(data);
            $log.debug("GamesCtrl: GamesService.getGames (" + status + "): " + angular.toJson(data));
            $scope.newGame = null;
        });

    });

    $scope.$on('$destroy', refreshGamesBind);
}]);