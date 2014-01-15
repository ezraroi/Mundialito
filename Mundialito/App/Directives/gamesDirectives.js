'use strict';

angular.module('mundialitoApp')
.directive('mundialitoGames', ['TeamsService', function (TeamsService) {
    return {
        restrict: "E",
        scope: {
            games: '=info',
            gamesType: '=filter'
        },
        templateUrl: "App/gamesTemplate.html"
    };
}]);