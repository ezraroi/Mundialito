angular.module('mundialitoApp')
.controller('GameCtrl', ['$scope', '$rootScope', '$log', 'GamesService', 'security', 'Alert', function ($scope, $rootScope, $log, GamesService, Security, Alert) {
    Security.authenticate();

    $scope.showEditForm = false;
    var refreshGamesBind = $scope.$on('refreshGames', function () {
        $log.debug("GameCtrl: got 'refreshGames' event");
        $scope.updatedGame = angular.copy($scope.team);
    });

    $scope.$on('$destroy', refreshGamesBind);

    $scope.schema = GamesService.schema;

}]);