angular.module('mundialitoApp')
.controller('TeamsCtrl', ['$scope', '$rootScope', '$log', 'TeamsService', 'security', 'Alert', function ($scope, $rootScope, $log, TeamsService, Security, Alert) {

    Security.authenticate();

    var getTeams = function () {
        TeamsService.getTeams().success(function (data, status, headers, config) {
            $log.debug("TeamsService.getTeams Success (" + status + "): " + angular.toJson(data));
            $scope.teams = data;
            $scope.showNewTeam = false;
        });
    }

    $scope.showNewTeam = false;

    var Team = function () {
        return {
            Name: '',
            Flag: '',
            Logo: '',
            ShortName: ''
        }
    }
    
    getTeams();

    $scope.$on('refreshTeams', function () {
        getTeams();
    });

    $scope.team = new Team();

    $scope.schema = TeamsService.schema;
}]);