'use strict';

angular.module('mundialitoApp')
.factory('BetsService', ['$http', function ($http) {
    var BetsApi = {
        getUserBetOnGame: function(gameId) {
            return $http.get("api/games/" + gameId + "/MyBet", { tracker: 'getUserBetOnGame' });
        },
        getGameBets: function (gameId) {
            return $http.get("api/games/" + gameId + "/Bets", { tracker: 'getGameBets' });
        }
    };
    return BetsApi;
}]);