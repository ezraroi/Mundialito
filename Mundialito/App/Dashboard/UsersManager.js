'use strict';
angular.module('mundialitoApp').factory('UsersManager', ['$http', '$q', 'User','$log', function($http,$q,User,$log) {
    var usersManager = {
        _pool: {},
        _retrieveInstance: function(gameId, userData) {
            var instance = this._pool[gameId];

            if (instance) {
                $log.debug('UsersManager: updating existing instance of user ' + gameId);
                instance.setData(userData);
            } else {
                $log.debug('UsersManager: saving new instance of user ' + gameId);
                instance = new User(userData);
                this._pool[gameId] = instance;
            }
            return instance;
        },
        _search: function(gameId) {
            $log.debug('UsersManager: will fetch user ' + gameId + ' from local pool');
            return this._pool[gameId];
        },
        _load: function(gameId, deferred) {
            var scope = this;
            $log.debug('UsersManager: will fetch user ' + gameId + ' from server');
            $http.get('api/users/' + gameId, { tracker: 'getUser' })
                .success(function(userData) {
                    var user = scope._retrieveInstance(userData.GameId, userData);
                    deferred.resolve(user);
                })
                .error(function() {
                    deferred.reject();
                });
        },

        /* Public Methods */
        /* Use this function in order to get a user instance by it's id */
        getUser: function(gameId,fresh) {
            var deferred = $q.defer();
            var user = undefined;
            if ((!angular.isDefined(fresh) || (!fresh))) {
                user = this._search(gameId);
            }
            if (user) {
                deferred.resolve(user);
            } else {
                this._load(gameId, deferred);
            }
            return deferred.promise;
        },

        /* Use this function in order to get instances of all the users */
        loadAllUsers: function() {
            var deferred = $q.defer();
            var scope = this;
            $log.debug('UsersManager: will fetch all users from server');
            $http.get('api/users', { tracker: 'getUsers' })
                .success(function(usersArray) {
                    var users = [];
                    usersArray.forEach(function(userData) {
                        var user = scope._retrieveInstance(userData.GameId, userData);
                        users.push(user);
                    });

                    deferred.resolve(users);
                })
                .error(function() {
                    deferred.reject();
                });
            return deferred.promise;
        },

        /*  This function is useful when we got somehow the user data and we wish to store it or update the pool and get a user instance in return */
        setUser: function(userData) {
            $log.debug('UsersManager: will set user ' + userData.GameId + ' to -' + angular.toJson(userData));
            var scope = this;
            var user = this._search(userData.GameId);
            if (user) {
                user.setData(userData);
            } else {
                user = scope._retrieveInstance(userData);
            }
            return user;
        }

    };
    return usersManager;
}]);
