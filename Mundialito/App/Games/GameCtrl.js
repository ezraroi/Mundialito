angular.module('mundialitoApp')
.controller('GameCtrl', ['$scope', '$rootScope', '$log', 'GamesService', 'BetsService', 'security', 'Alert', function ($scope, $rootScope, $log, GamesService, BetsService, Security, Alert) {
    Security.authenticate();
    $scope.showEditForm = false;
    $scope.updatedGame = angular.copy($scope.game);

    BetsService.getGameBets($scope.game.GameId).success(function (data, status, headers, config) {
        $log.debug("GameCtrl: BetsService.getGameBets Success (" + status + "): " + angular.toJson(data));
        $scope.gameBets = data;
    });

    var refreshGamesBind = $rootScope.$on('refreshGames', function () {
        $log.debug("GameCtrl: got 'refreshGames' event");
        $scope.updatedGame = angular.copy($scope.game);
    });

    $scope.$on('$destroy', refreshGamesBind);
    

}]);