angular.module('mundialitoApp')
.controller('GameCtrl', ['$scope', '$rootScope', '$log', '$routeParams', 'security', 'BetsService', 'game', 'userBet', function ($scope, $rootScope, $log, $routeParams, Security, BetsService, game, userBet) {
    Security.authenticate();
    $scope.game = game;
    $scope.userBet = userBet;
    $scope.gameId = $routeParams.gameId;
    $scope.showEditForm = false;
    $scope.updatedGame = angular.copy($scope.game);

    BetsService.getGameBets($scope.game.GameId).success(function (data, status) {
        $log.debug("GameCtrl: BetsService.getGameBets Success (" + status + "): " + angular.toJson(data));
        $scope.gameBets = data;
    });

    var refreshGamesBind = $rootScope.$on('refreshGames', function () {
        $log.debug("GameCtrl: got 'refreshGames' event");
        $scope.updatedGame = $scope.game;
    });

    $scope.$on('$destroy', refreshGamesBind);
}]);