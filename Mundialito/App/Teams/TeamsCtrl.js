'use strict';
angular.module('mundialitoApp').controller('TeamsCtrl', ['$scope', '$log', 'TeamsManager', 'teams', 'Alert', function ($scope, $log, TeamsManager, teams, Alert) {
    $scope.teams = teams;
    $scope.showNewTeam = false;
    $scope.newTeam = null;

    $scope.addNewTeam = function () {
        $scope.newTeam = TeamsManager.getEmptyTeamObject();
    };

    $scope.saveNewTeam = function() {
        TeamsManager.addTeam($scope.newTeam).then(function(data) {
            Alert.success('Team was added successfully');
            $scope.newTeam = null;
            $scope.teams.push(data);
        });
    };

    $scope.deleteTeam = function(team) {
        var scope = team;
        team.delete().success(function() {
            Alert.success('Team was deleted successfully');
            $scope.teams.splice($scope.teams.indexOf(scope),1);
        })
    };

    $scope.schema =  TeamsManager.getTeamSchema();
}]);