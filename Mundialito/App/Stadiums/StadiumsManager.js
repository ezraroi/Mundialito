'use strict';
angular.module('mundialitoApp').factory('StadiumsManager', ['$http', '$q', 'Stadium', '$log', 'MundialitoUtils', function ($http, $q, Stadium, $log, MundialitoUtils) {
    var stadiumsPromise = undefined;
    var stadiumsManager = {
        _pool: {},
        _retrieveInstance: function(stadiumId, stadiumData) {
            var instance = this._pool[stadiumId];

            if (instance) {
                $log.debug('StadiumsManager: updating existing instance of stadium ' + stadiumId);
                instance.setData(stadiumData);
            } else {
                $log.debug('StadiumsManager: saving new instance of stadium ' + stadiumId);
                instance = new Stadium(stadiumData);
                this._pool[stadiumId] = instance;
            }
            instance.LoadTime = new Date();
            return instance;
        },
        _search: function(stadiumId) {
            $log.debug('StadiumsManager: will fetch stadium ' + stadiumId + ' from local pool');
            var instance = this._pool[stadiumId];
            if (angular.isDefined(instance) && MundialitoUtils.shouldRefreshInstance(instance)) {
                $log.debug('StadiumsManager: Instance was loaded at ' + instance,LoadTime + ', will reload it from server');
                return undefined;
            }
            return instance;
        },
        _load: function(stadiumId, deferred) {
            var scope = this;
            $log.debug('StadiumsManager: will fetch stadium ' + stadiumId + ' from server');
            $http.get('api/stadiums/' + stadiumId, { tracker: 'getStadium' })
                .success(function(stadiumData) {
                    var stadium = scope._retrieveInstance(stadiumData.StadiumId, stadiumData);
                    deferred.resolve(stadium);
                })
                .error(function() {
                    deferred.reject();
                });
        },

        /* Public Methods */

        getStaidumSchema: function() {
            return [
                { property: 'Name', label: 'Name', type: 'text', attr: { required: true } },
                { property: 'City', label: 'City', type: 'text', attr: { required: true } },
                { property: 'Capacity', label: 'Capacity', type: 'number', attr: { required: true } }
            ];
        },

        /* Use this function in order to get a new empty stadium data object */
        getEmptyStadiumObject: function() {
            return {
                HomeTeam: '',
                AwayTeam: ''
            };
        },

        /* Use this function in order to add a new stadium */
        addStadium: function(stadiumData) {
            var deferred = $q.defer();
            var scope = this;
            $log.debug('StadiumsManager: will add new stadium - ' + angular.toJson(stadiumData));
            $http.post("api/stadiums", stadiumData, { tracker: 'addStadium' }).success(function(data) {
                var stadium = scope._retrieveInstance(data.StadiumId, data);
                deferred.resolve(stadium);
            })
                .error(function() {
                    deferred.reject();
                });
            return deferred.promise;
        },

        /* Use this function in order to get a stadium instance by it's id */
        getStadium: function(stadiumId,fresh) {
            var deferred = $q.defer();
            var stadium = undefined;
            if ((!angular.isDefined(fresh) || (!fresh))) {
                stadium = this._search(stadiumId);
            }
            if (stadium) {
                deferred.resolve(stadium);
            } else {
                this._load(stadiumId, deferred);
            }
            return deferred.promise;
        },

        /* Use this function in order to get instances of all the stadiums */
        loadAllStadiums: function () {
            if (stadiumsPromise) {
                return stadiumsPromise;
            }
            var deferred = $q.defer();
            var scope = this;
            $log.debug('StadiumsManager: will fetch all games from server');
            $http.get("api/stadiums", { tracker: 'getStadiums', cache: true })
                .success(function(stadiumsArray) {
                    var stadiums = [];
                    stadiumsArray.forEach(function(stadiumData) {
                        var stadium = scope._retrieveInstance(stadiumData.StadiumId, stadiumData);
                        stadiums.push(stadium);
                    });
                    deferred.resolve(stadiums);
                })
                .error(function() {
                    deferred.reject();
                });
            stadiumsPromise = deferred.promise;
            return deferred.promise;
        },

        /*  This function is useful when we got somehow the stadium data and we wish to store it or update the pool and get a stadium instance in return */
        setStadium: function(stadiumData) {
            $log.debug('StadiumsManager: will set stadium ' + stadiumData.StadiumId + ' to -' + angular.toJson(stadiumData));
            var scope = this;
            var stadium = this._search(stadiumData.StadiumId);
            if (stadium) {
                stadium.setData(stadiumData);
            } else {
                stadium = scope._retrieveInstance(stadiumData.StadiumId,stadiumData);
            }
            return stadium;
        }
    };
    return stadiumsManager;
}]);
