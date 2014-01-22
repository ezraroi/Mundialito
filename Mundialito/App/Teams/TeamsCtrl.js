angular.module('mundialitoApp')
.controller('TeamsCtrl', ['$scope', '$rootScope', '$log', 'TeamsService', 'security', 'Alert', function ($scope, $rootScope, $log, TeamsService, Security, Alert) {

    Security.authenticate();

    var getTeams = function () {
        TeamsService.getTeams().success(function (data, status, headers, config) {
            $log.debug("TeamsCtrl: TeamsService.getTeams Success (" + status + "): " + angular.toJson(data));
            $scope.teams = data;
        });
    }

    $scope.showNewTeam = false;
    $scope.newTeam = null;

    $scope.addNewTeam = function () {
        $scope.newTeam = TeamsService.getEmptyTeamObject();
    }

    var refreshTeamsBind = $rootScope.$on('refreshTeams', function () {
        $log.debug("TeamsCtrl: got 'refreshTeams' event");
        getTeams();
        $scope.newTeam = null;
        
    });

    $scope.$on('$destroy', refreshTeamsBind);

    $scope.schema = TeamsService.schema;
}]);