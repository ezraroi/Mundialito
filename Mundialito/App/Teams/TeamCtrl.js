angular.module('mundialitoApp')
.controller('TeamCtrl', ['$scope', '$log', 'TeamsManager', 'security', 'team', 'games', 'Alert', function ($scope, $log, TeamsManager, Security, team, games, Alert) {
    Security.authenticate();
    $scope.team = team;
    $scope.games = games;
    $scope.showEditForm = false;

    $scope.updateTeam = function() {
        $scope.team.update().success(function() {
            Alert.new('success', 'Team was updated successfully', 2000);
        })
    };

    $scope.schema =  TeamsManager.getTeamSchema();
    
}]);