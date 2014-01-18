'use strict';

angular.module('mundialitoApp')
.directive('mundialitoGames', function () {
    return {
        restrict: "E",
        scope: {
            games: '=info',
            gamesType: '=filter',
            onAdd: '&'
        },
        templateUrl: "App/gamesTemplate.html"
    };
})
.directive('addGameButton', ['$rootScope', '$log', 'GamesService', 'Alert', function ($rootScope, $log, GamesService, Alert) {
    return {
        restrict: "A",
        link: function (scope, element, attrs) {
            element.bind("click", function () {
                scope.newGame.AwayTeam = angular.fromJson(scope.newGame.AwayTeam);
                scope.newGame.HomeTeam = angular.fromJson(scope.newGame.HomeTeam);
                scope.newGame.Stadium = angular.fromJson(scope.newGame.Stadium);
                GamesService.addGame(scope.newGame).success(function (data, status, headers, config) {
                    $log.log("Game " + data.GameId + " was added");
                    Alert.new('success', 'Game was added successfully', 2000);
                    $rootScope.$emit("refreshGames");
                });
            });
        }
    };
}])
.directive('deleteGameButton', ['$rootScope', '$log', 'GamesService', 'Alert', function ($rootScope, $log, GamesService, Alert) {
    return {
        restrict: "A",
        link: function (scope, element, attrs) {
            element.bind("click", function () {
                if (confirm("Are you sure you want to delete the game?") == true) {
                    GamesService.deleteGame(scope.game.GameId).success(function (data, status, headers, config) {
                        $log.log("Game " + data.GameId + " was deleted");
                        Alert.new('success', 'Game was deleted successfully', 2000);
                        $rootScope.$emit("refreshGames");
                    });
                }
            });
        }
    };
}]);