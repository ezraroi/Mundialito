angular.module('mundialitoApp')
.controller('GameCtrl', ['$scope', '$rootScope', '$log', 'GamesService', 'security', 'Alert', function ($scope, $rootScope, $log, GamesService, Security, Alert) {
    Security.authenticate();
    $scope.showEditForm = false;
    $scope.updatedGame = angular.copy($scope.game);

    var refreshGamesBind = $rootScope.$on('refreshGames', function () {
        $log.debug("GameCtrl: got 'refreshGames' event");
        $scope.updatedGame = angular.copy($scope.game);
    });

    $scope.$on('$destroy', refreshGamesBind);
    

}]);