'use strict';
angular.module('mundialitoApp').factory('GeneralBetsManager', ['$http', '$q', 'GeneralBet','$log','MundialitoUtils', function($http,$q,GeneralBet,$log,MundialitoUtils) {
    var generalBetsManager = {
        _pool: {},
        _retrieveInstance: function(betId, betData) {
            var instance = this._pool[betId];

            if (instance) {
                $log.debug('GeneralBetsManager: updating existing instance of bet ' + betId);
                instance.setData(betData);
            } else {
                $log.debug('GeneralBetsManager: saving new instance of bet ' + betId);
                instance = new GeneralBet(betData);
                this._pool[betId] = instance;
            }
            instance.LoadTime = new Date();
            return instance;
        },
        _search: function(betId) {
            $log.debug('GeneralBetsManager: will fetch bet ' + betId + ' from local pool');
            var instance = this._pool[betId];
            if (angular.isDefined(instance) && MundialitoUtils.shouldRefreshInstance(instance)) {
                $log.debug('GeneralBetsManager: Instance was loaded at ' + instance,LoadTime + ', will reload it from server');
                return undefined;
            }
            return instance;
        },
        _load: function(betId, deferred) {
            var scope = this;
            $log.debug('GeneralBetsManager: will fetch bet ' + betId + ' from server');
            $http.get('api/generalbets/' + betId, { tracker: 'getGeneralBet' })
                .success(function(betData) {
                    var bet = scope._retrieveInstance(betData.GeneralBetId, betData);
                    deferred.resolve(bet);
                })
                .error(function() {
                    deferred.reject();
                });
        },

        /* Public Methods */
        /* Use this function in order to add a new general bet */
        addGeneralBet: function(betData) {
            var deferred = $q.defer();
            var scope = this;
            $log.debug('GeneralBetsManager: will add new bet - ' + angular.toJson(betData));
            $http.post('api/generalbets/', betData, { tracker: 'addGeneralBet' }).success(function(data) {
                var bet = scope._retrieveInstance(data.GeneralBetId, data);
                deferred.resolve(bet);
            })
                .error(function() {
                    deferred.reject();
                });
            return deferred.promise;
        },

        /* Use this function in order to get a general bet instance by it's id */
        getGeneralBet: function(betId,fresh) {
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

        /* Use this function in order to get instances of all the general bets */
        loadAllGeneralBets: function() {
            var deferred = $q.defer();
            var scope = this;
            $log.debug('GeneralBetsManager: will fetch all general bets from server');
            $http.get('api/generalbets', { tracker: 'getGeneralBets' })
                .success(function(gamesArray) {
                    var generalBets = [];
                    gamesArray.forEach(function(betData) {
                        var bet = scope._retrieveInstance(betData.GeneralBetId, betData);
                        generalBets.push(bet);
                    });

                    deferred.resolve(generalBets);
                })
                .error(function() {
                    deferred.reject();
                });
            return deferred.promise;
        },

        hasGeneralBet : function(username) {
            var deferred = $q.defer();
            $log.debug('GeneralBetsManager: will check if user ' + username + ' has general bets');
            $http.get('api/generalbets/has-bet/' + username, { tracker: 'getUserGeneralBet' })
                .success(function(answer) {
                    deferred.resolve(answer);
                })
                .error(function() {
                    deferred.reject();
                });
            return deferred.promise;
        },

        canSubmtiGeneralBet : function() {
            var deferred = $q.defer();
            $log.debug('GeneralBetsManager: will check if user general bets are closed');
            $http.get('api/generalbets/cansubmitbets/', { tracker: 'getCanSubmitGeneralBets' })
                .success(function(answer) {
                    deferred.resolve(answer);
                })
                .error(function() {
                    deferred.reject();
                });
            return deferred.promise;
        },

        /* Use this function in order to get a general bet instance by it's owner username */
        getUserGeneralBet: function(username) {
            var deferred = $q.defer();
            var scope = this;
            $log.debug('GeneralBetsManager: will fetch user ' + username +  ' general bet from server');
            $http.get('api/generalbets/user/' + username, { tracker: 'getUserGeneralBet' })
                .success(function(betData) {
                    var bet = scope._retrieveInstance(betData.GeneralBetId, betData);
                    deferred.resolve(bet);
                })
                .error(function() {
                    deferred.reject();
                });
            return deferred.promise;
        },

        /*  This function is useful when we got somehow the bet data and we wish to store it or update the pool and get a general bet instance in return */
        setGeneralBet: function(betData) {
            $log.debug('GeneralBetsManager: will set bet ' + betData.GeneralBetId + ' to -' + angular.toJson(betData));
            var scope = this;
            var bet = this._search(betData.GeneralBetId);
            if (bet) {
                bet.setData(betData);
            } else {
                bet = scope._retrieveInstance(betData.GeneralBetId,betData);
            }
            return bet;
        }
    };
    return generalBetsManager;
}]);
