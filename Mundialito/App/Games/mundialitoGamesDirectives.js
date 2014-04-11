'use strict';

angular.module('mundialitoApp').directive('mundialitoGames',['Alert', function (Alert) {
    return {
        restrict: 'E',
        scope: {
            games: '=info',
            gamesType: '=filter',
            showOnly : '=',
            onAdd: '&'
        },
        templateUrl: 'App/Partials/Templates/gamesTemplate.html',
        link : function($scope) {
            $scope.deleteGame = function(game) {
                var scope = game;
                game.delete().success(function() {
                    Alert.new('success', 'Game was deleted successfully', 2000);
                    $scope.games.splice($scope.games.indexOf(scope),1);
                });
            }
            $scope.matchGameType = function(gameType) {
                return function(item) {
                    if ((!gameType) || (gameType === "All")) {
                        return true;
                    }
                    return item.IsOpen;
                }
            }
        }
    };
}]);
