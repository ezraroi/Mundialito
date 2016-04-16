'use strict';
angular.module('mundialitoApp').factory('BetsManager', ['$http', '$q', 'Bet', '$log', 'MundialitoUtils', 'GamesManager', function ($http, $q, Bet, $log, MundialitoUtils, GamesManager) {
    var betsManager = {
        _pool: {},
        _retrieveInstance: function(betId, betData) {
            var instance = this._pool[betId];

            if (instance) {
                $log.debug('BetsManager: updating existing instance of bet ' + betId);
                instance.setData(betData);
            } else {
                $log.debug('BetsManager: saving new instance of bet ' + betId);
                instance = new Bet(betData);
                this._pool[betId] = instance;
            }
            instance.LoadTime = new Date();
            return instance;
        },
        _search: function(betId) {
            $log.debug('BetsManager: will fetch bet ' + betId + ' from local pool');
            var instance = this._pool[betId];
            if (angular.isDefined(instance) && MundialitoUtils.shouldRefreshInstance(instance)) {
                $log.debug('BetsManager: Instance was loaded at ' + instance,LoadTime + ', will reload it from server');
                return undefined;
            }
            return instance;
        },
        _load: function(betId, deferred) {
            var scope = this;
            $log.debug('BetsManager: will fetch bet ' + betId + ' from server');
            $http.get('api/bets/' + betId, { tracker: 'getBet' })
                .success(function(betData) {
                    var bet = scope._retrieveInstance(betData.BetId, betData);
                    deferred.resolve(bet);
                })
                .error(function() {
                    deferred.reject();
                });
        },

        /* Public Methods */
        /* Use this function in order to add a new bet */
        addBet: function(betData) {
            var deferred = $q.defer();
            var scope = this;
            $log.debug('BetsManager: will add new bet - ' + angular.toJson(betData));
            $http.post('api/bets/', betData, { tracker: 'addBetOnGame' }).success(function(data) {
                var bet = scope._retrieveInstance(data.BetId, data);
                GamesManager.clearGamesCache();
                deferred.resolve(bet);
            }).error(function (err) {
                $log.error('Failed to add bet');
                deferred.reject(err);
            });
            return deferred.promise;
        },

        /* Use this function in order to get a bet instance by it's id */
        getBet: function(betId,fresh) {
            var deferred = $q.defer();
            var bet = undefined;
            if ((!angular.isDefined(fresh) || (!fresh))) {
                bet = this._search(betId);
            }
            if (bet) {
                deferred.resolve(bet);
            } else {
                this._load(betId, deferred);
            }
            return deferred.promise;
        },

        /* Use this function in order to get instances of all the game bets */
        getGameBets: function(gameId) {
            var deferred = $q.defer();
            var scope = this;
            $log.debug('BetsManager: will fetch all bets of game ' + gameId + ' from server');
            $http.get('api/games/' + gameId + '/bets', { tracker: 'getGameBets' })
                .success(function(betsArray) {
                    var bets = [];
                    betsArray.forEach(function(betData) {
                        var bet = scope._retrieveInstance(betData.BetId, betData);
                        bets.push(bet);
                    });

                    deferred.resolve(bets);
                })
                .error(function() {
                    deferred.reject();
                });
            return deferred.promise;
        },

        getUserBets : function(username) {
            var deferred = $q.defer();
            var scope = this;
            $log.debug('BetsManager: will fetch user ' + username +' bets from server');
            $http.get('api/bets/user/' + username, { tracker: 'getUserBets' })
                .success(function(betsArray) {
                    var bets = [];
                    betsArray.forEach(function(betData) {
                        var bet = scope._retrieveInstance(betData.BetId, betData);
                        bets.push(bet);
                    });

                    deferred.resolve(bets);
                })
                .error(function() {
                    deferred.reject();
                });
            return deferred.promise;
        },

        getUserBetOnGame : function(gameId) {
            var deferred = $q.defer();
            var scope = this;
            $log.debug('BetsManager: will fetch user bet of game ' + gameId + ' from server');
            $http.get('api/games/' + gameId + '/mybet', { tracker: 'getUserBetOnGame' })
                .success(function(betData) {
                    if (betData.BetId != -1)
                    {
                        var bet = scope._retrieveInstance(betData.BetId, betData);
                        deferred.resolve(bet);
                    }
                    deferred.resolve(betData);
                })
                .error(function() {
                    deferred.reject();
                });
            return deferred.promise;
        },

        /*  This function is useful when we got somehow the bet data and we wish to store it or update the pool and get a general bet instance in return */
        setBet: function(betData) {
            $log.debug('BetsManager: will set bet ' + betData.BetId + ' to -' + angular.toJson(betData));
            var scope = this;
            var bet = this._search(betData.BetId);
            if (bet) {
                bet.setData(betData);
            } else {
                bet = scope._retrieveInstance(betData.BetId, betData);
            }
            return bet;
        }

    };
    return betsManager;
}]);
