angular.module('mundialitoApp')
.controller('TeamsCtrl', ['$scope', '$log', 'TeamsManager', 'security', 'teams', 'Alert', function ($scope, $log, TeamsManager, Security, teams, Alert) {
    Security.authenticate();
    $scope.teams = teams;
    $scope.showNewTeam = false;
    $scope.newTeam = null;

    $scope.addNewTeam = function () {
        $scope.newTeam = TeamsManager.getEmptyTeamObject();
    };

    $scope.saveNewTeam = function() {
        TeamsManager.addTeam($scope.newTeam).then(function(data) {
            Alert.new('success', 'Team was added successfully', 2000);
            $scope.newTeam = null;
            $scope.teams.push(data);
        });
    };

    $scope.deleteTeam = function(team) {
        var scope = team;
        team.delete().success(function() {
            Alert.new('success', 'Team was deleted successfully', 2000);
            $scope.teams.splice($scope.teams.indexOf(scope),1);
        })
    };

    $scope.schema =  TeamsManager.getTeamSchema();
}]);