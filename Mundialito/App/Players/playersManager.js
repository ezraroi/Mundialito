'use strict';
angular.module('mundialitoApp').factory('PlayersManager', ['$http', '$q', 'Player', '$log', function ($http, $q, Player, $log) {
    var playersPromise = undefined;
    var playersManager = {
        _pool: {},
        _retrieveInstance: function (playerId, playerData) {
            var instance = this._pool[playerId];

            if (instance) {
                $log.debug('playersPromise: updating existing instance of player ' + playerId);
                instance.setData(playerData);
            } else {
                $log.debug('playersPromise: saving new instance of player ' + playerId);
                instance = new Player(playerData);
                this._pool[playerId] = instance;
            }
            instance.LoadTime = new Date();
            return instance;
        },

        /* Public Methods */

        getPlayerSchema: function () {
            return [
                { property: 'Name', label: 'Name', type: 'text', attr: { required: true } }
            ];
        },

        /* Use this function in order to get instances of all the players */
        loadAllPlayers: function () {
            if (playersPromise) {
                return playersPromise;
            }
            var deferred = $q.defer();
            var scope = this;
            $log.debug('PlayersManager: will fetch all players from server');
            $http.get("api/players", { tracker: 'getPlayers', cache: true })
                .success(function (playersArray) {
                    var players = [];
                    playersArray.forEach(function (playerData) {
                        var player = scope._retrieveInstance(playerData.PlayerId, playerData);
                        players.push(player);
                    });
                    deferred.resolve(players);
                })
                .error(function () {
                    deferred.reject();
                });
            playersPromise = deferred.promise;
            return deferred.promise;
        },

    };
    return playersManager;
}]);
