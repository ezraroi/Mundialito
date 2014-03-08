angular.module('mundialitoApp')
.controller('TeamsCtrl', ['$scope', '$rootScope', '$log', 'TeamsService', 'security', 'teams', function ($scope, $rootScope, $log, TeamsService, Security, teams) {
    Security.authenticate();
    $scope.teams = teams;
    $scope.showNewTeam = false;
    $scope.newTeam = null;

    $scope.addNewTeam = function () {
        $scope.newTeam = TeamsService.getEmptyTeamObject();
    }

    var refreshTeamsBind = $rootScope.$on('refreshTeams', function () {
        $log.debug("TeamsCtrl: got 'refreshTeams' event");
        TeamsService.getTeams().success(function (data) {
            $scope.teams = teams;
        });
        $scope.newTeam = null;
        
    });

    $scope.$on('$destroy', refreshTeamsBind);

    $scope.schema = TeamsService.schema;
}]);