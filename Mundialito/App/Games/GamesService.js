'use strict';

angular.module('mundialitoApp')
.factory('GamesService', ['$http', function ($http) {
    var GamesApi = {
        getGames: function () {
            return $http.get("api/games", { tracker: 'getGames' });
        },
        addGame: function (gameData) {
            return $http.post("api/games", gameData, { tracker: 'addGame' });
        },
        editGame: function (gameData) {
            return $http.put("api/games/" + gameData.GameId, gameData, { tracker: 'editGame' });
        },
        deleteGame: function (gameId) {
            return $http.delete("api/games/" + gameId, { tracker: 'deleteGame' });
        },
        getGame: function (gameId) {
            return $http.get("api/games/" + gameId, { tracker: 'getGame' });
        },

        getEmptyGameObject: function() {
            return {
                HomeTeam: '',
                AwayTeam: ''
            };
        }
    };
    return GamesApi;
}]);