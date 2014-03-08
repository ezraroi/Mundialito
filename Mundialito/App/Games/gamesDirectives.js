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
        templateUrl: "App/Partials/Templates/gamesTemplate.html"
    };
})
.directive('addGameButton', ['$rootScope', '$log', 'GamesService', 'Alert', function ($rootScope, $log, GamesService, Alert) {
    return {
        restrict: "A",
        scope : {
            game : '='
        },
        link: function (scope, element) {
            element.bind("click", function () {
                scope.game.AwayTeam = angular.fromJson(scope.game.AwayTeam);
                scope.game.HomeTeam = angular.fromJson(scope.game.HomeTeam);
                scope.game.Stadium = angular.fromJson(scope.game.Stadium);
                GamesService.addGame(scope.game).success(function (data) {
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
        link: function (scope, element) {
            element.bind("click", function () {
                if (confirm("Are you sure you want to delete the game?") == true) {
                    GamesService.deleteGame(scope.game.GameId).success(function (data) {
                        $log.log("Game " + data.GameId + " was deleted");
                        Alert.new('success', 'Game was deleted successfully', 2000);
                        $rootScope.$emit("refreshGames");
                    });
                }
            });
        }
    };
}])
.directive('updateGameButton', ['$rootScope', '$log', 'GamesService', 'Alert', function ($rootScope, $log, GamesService, Alert) {
    return {
        restrict: "A",
        link: function (scope, element) {
            element.bind("click", function () {
                GamesService.editGame(scope.updatedGame).success(function (data) {
                    $log.log("Game " + data.GameId + " was updated");
                    Alert.new('success', 'Game was updated successfully', 2000);
                    scope.game = data;
                    $rootScope.$emit("refreshGames");
                });

            });
        }
    };
}]);