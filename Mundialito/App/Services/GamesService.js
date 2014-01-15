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
        gameTeam: function (gameId) {
            return $http.delete("api/games/" + gameId, { tracker: 'deleteGame' });
        },
        getGame: function (gameId) {
            return $http.get("api/games/" + gameId, { tracker: 'getGame' });
        },

        schema: [
                { property: 'Name', label: 'Name', type: 'text', attr: { required: true } },
                { property: 'Flag', label: 'Flag', type: 'url', attr: { required: true } },
                { property: 'Logo', label: 'Logo', type: 'url', attr: { required: true } },
                { property: 'ShortName', label: 'Short Name', type: 'text', attr: { ngMaxlength: 3, ngMinlength: 3, required: true } },
        ]
    };
    return GamesApi;
}]);