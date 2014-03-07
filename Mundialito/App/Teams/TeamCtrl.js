angular.module('mundialitoApp')
.controller('TeamCtrl', ['$scope', '$rootScope', '$log', 'TeamsService', 'security', '$routeParams', 'team', function ($scope, $rootScope, $log, TeamsService, Security, $routeParams, team) {
    Security.authenticate();
    $scope.team = angular.copy(team);
    $scope.teamId = $routeParams.teamId;
    $scope.showEditForm = false;
    $scope.updatedTeam = angular.copy($scope.team);

    TeamsService.getTeamGames($scope.teamId).success(function (data, status, headers, config) {
        $log.debug("TeamCtrl: TeamsService.getTeamGames (" + status + "): " + angular.toJson(data));
        $scope.games = data;
    });

    var refreshTeamsBind = $rootScope.$on('refreshTeams', function () {
        $log.debug("TeamCtrl: got 'refreshTeams' event");
        $scope.updatedTeam = angular.copy($scope.team);
    });

    $scope.$on('$destroy', refreshTeamsBind);

    $scope.schema = TeamsService.schema;
    
}]);