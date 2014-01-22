angular.module('mundialitoApp')
.controller('TeamCtrl', ['$scope', '$rootScope', '$log', 'TeamsService', 'security', 'Alert', function ($scope, $rootScope, $log, TeamsService, Security, Alert) {

    Security.authenticate();
    $scope.showEditForm = false;

    TeamsService.getTeamGames($scope.teamId).success(function (data, status, headers, config) {
        $log.debug("TeamCtrl: TeamsService.getTeamGames (" + status + "): " + angular.toJson(data));
        $scope.games = data;
    });

    var refreshTeamsBind = $scope.$on('refreshTeams', function () {
        $log.debug("TeamCtrl: got 'refreshTeams' event");
        $scope.updatedTeam = angular.copy($scope.team);
    });

    $scope.$on('$destroy', refreshTeamsBind);

    $scope.schema = TeamsService.schema;
    
}]);