'use strict';

angular.module('mundialitoApp')
.factory('BetsService', ['$http', function ($http) {
    var BetsApi = {
        getUserBetOnGame: function(gameId) {
            return $http.get("api/games/" + gameId + "/MyBet", { tracker: 'getUserBetOnGame' });
        },
        getGameBets: function (gameId) {
            return $http.get("api/games/" + gameId + "/Bets", { tracker: 'getGameBets' });
        },
        addBetOnGame: function(betData) {
            return $http.post("api/bets/", betData, { tracker: 'addBetOnGame' });
        },
        updateBetOnGame: function(data) {
            return $http.put("api/bets/" + data.BetId, data, { tracker: 'updateBetOnGame' });
        }
    };
    return BetsApi;
}]);