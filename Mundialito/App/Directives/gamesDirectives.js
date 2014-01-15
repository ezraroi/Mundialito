'use strict';

angular.module('mundialitoApp')
.directive('mundialitoGames', ['$scope', 'TeamsService', function ($scope, TeamsService) {
    return {
        restrict: "E",
        scope: {
            games: '=info'
        },
        templateUrl: "App/gamesTemplate.html"
    };
}]);