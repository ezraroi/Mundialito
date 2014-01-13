angular.module('mundialitoApp')
.controller('TeamCtrl', ['$scope', '$rootScope', '$log', 'TeamsService', 'security', 'Alert', '$routeParams', function ($scope, $rootScope, $log, TeamsService, Security, Alert, $routeParams) {

    Security.authenticate();

    var teamId = $routeParams.action;

    $scope.showEditForm = false;

    TeamsService.getTeam(teamId).success(function (data, status, headers, config) {
        $log.debug("TeamsService.getTeam Success (" + status + "):" + angular.toJson(data));
        $scope.team = data;
        $scope.updatedTeam = angular.copy($scope.team);
    });

    TeamsService.getTeamGames(teamId).success(function (data, status, headers, config) {
        $log.debug("TeamsService.getTeamGames (" + status + "): " + angular.toJson(data));
        $scope.games = data;
    });

    $scope.$on('refreshTeams', function () {
        $scope.updatedTeam = angular.copy($scope.team);
    });

    $scope.schema = TeamsService.schema;
    
}]);