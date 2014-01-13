'use strict';

angular.module('mundialitoApp')
.directive('mundialitoGame', ['$rootScope', 'TeamsService', function ($rootScope, TeamsService) {
    return {
        restrict: "E",
        scope: {
            game: '=info'
        },
        templateUrl: "App/gameTemplate.html"
    };
}])
.directive('mundialitoGames', ['$rootScope', 'TeamsService', function ($rootScope, TeamsService) {
    return {
        restrict: "E",
        scope: {
            games: '=info'
        },
        templateUrl: "App/gamesTemplate.html"
    };
}]);