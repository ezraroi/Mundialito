'use strict';

angular.module('mundialitoApp')
.directive('deleteTeamButton', ['$rootScope', '$log', 'TeamsService', 'Alert', function ($rootScope, $log, TeamsService, Alert) {
    return {
        restrict: "A",
        link: function (scope, element, attrs) {
            element.bind("click", function () {
                TeamsService.deleteTeam(scope.team.TeamId).success(function (data, status, headers, config) {
                    $log.log("Team " + scope.team.TeamId + " was deleted");
                    Alert.new('success', 'Team was deleted successfully', 2000);
                    $rootScope.$emit("refreshTeams");
                });
            });
        }
    };
}])
.directive('addTeamButton', ['$rootScope', '$log', 'TeamsService', 'Alert', function ($rootScope, $log, TeamsService, Alert) {
    return {
        restrict: "A",
        link: function (scope, element, attrs) {
            element.bind("click", function () {
                TeamsService.addTeam(scope.newTeam).success(function (data, status, headers, config) {
                    $log.log("Team " + data.TeamId + " was added");
                    Alert.new('success', 'Team was added successfully', 2000);
                    $rootScope.$emit("refreshTeams");
                });
            });
        }
    };
}])
.directive('editTeamButton', ['$rootScope', '$log', 'TeamsService', 'Alert', function ($rootScope, $log, TeamsService, Alert) {
    return {
        restrict: "A",
        link: function (scope, element, attrs) {
            element.bind("click", function () {
                TeamsService.editTeam(scope.updatedTeam).success(function (data, status, headers, config) {
                    $log.log("Team " + data.TeamId + " was edited");
                    Alert.new('success', 'Team was edited successfully', 2000);
                    scope.team = data;
                    $rootScope.$emit("refreshTeams");
                });
            });
        }
    };
}]);