'use strict';
angular.module('mundialitoApp').controller('TeamCtrl', ['$scope', '$log', 'TeamsManager', 'team', 'games', 'Alert', function ($scope, $log, TeamsManager, team, games, Alert) {
    $scope.team = team;
    $scope.games = games;
    $scope.showEditForm = false;

    $scope.updateTeam = function() {
        $scope.team.update().success(function(data) {
            Alert.success('Team was updated successfully');
            TeamsManager.setTeam(data);
        })
    };

    $scope.schema =  TeamsManager.getTeamSchema();
    
}]);