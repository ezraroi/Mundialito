'use strict';
angular.module('mundialitoApp').controller('TeamCtrl', ['$scope', '$log', 'TeamsManager', 'team', 'games', 'Alert', function ($scope, $log, TeamsManager, team, games, Alert) {
    $scope.team = team;
    $scope.games = games;
    $scope.showEditForm = false;

    $scope.updateTeam = function() {
        $scope.team.update().success(function(data) {
            Alert.new('success', 'Team was updated successfully', 2000);
            TeamsManager.setTeam(data);
        })
    };

    $scope.schema =  TeamsManager.getTeamSchema();
    
}]);