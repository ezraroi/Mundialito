'use strict';
angular.module('mundialitoApp').factory('UsersManager', ['$http', '$q', 'User','$log','MundialitoUtils','DSCacheFactory', function($http,$q,User,$log,MundialitoUtils,DSCacheFactory) {
    var usersManager = {
        _cacheManager: DSCacheFactory('UsersManager', { cacheFlushInterval : 1800000 }),
        _pool: {},
        _retrieveInstance: function(username, userData) {
            var instance = this._pool[username];

            if (instance) {
                $log.debug('UsersManager: updating existing instance of user ' + username);
                instance.setData(userData);
            } else {
                $log.debug('UsersManager: saving new instance of user ' + username);
                instance = new User(userData);
                this._pool[username] = instance;
            }
            instance.LoadTime = new Date();
            return instance;
        },
        _search: function(username) {
            $log.debug('UsersManager: will fetch user ' + username + ' from local pool');
            var instance = this._pool[username];
            if (angular.isDefined(instance) && MundialitoUtils.shouldRefreshInstance(instance)) {
                $log.debug('UsersManager: Instance was loaded at ' + instance,LoadTime + ', will reload it from server');
                return undefined;
            }
            return instance;
        },
        _load: function(username, deferred) {
            var scope = this;
            $log.debug('UsersManager: will fetch user ' + username + ' from server');
            $http.get('api/users/' + username, { tracker: 'getUser', cahce: this._cacheManager })
                .success(function(userData) {
                    var user = scope._retrieveInstance(userData.Username, userData);
                    deferred.resolve(user);
                })
                .error(function() {
                    deferred.reject();
                });
        },

        /* Public Methods */
        /* Use this function in order to get a user instance by it's id */
        getUser: function(username,fresh) {
            var deferred = $q.defer();
            var user = undefined;
            if ((!angular.isDefined(fresh) || (!fresh))) {
                user = this._search(username);
            }
            if (user) {
                deferred.resolve(user);
            } else {
                this._load(username, deferred);
            }
            return deferred.promise;
        },

        generatePrivateKey : function(email) {
            var deferred = $q.defer();
            $log.debug('UsersManager: will generate private key for ' + email);
            $http.get('api/users/generateprivatekey/' + encodeURIComponent(email) + '/', { tracker: 'generatePrivateKey' })
                .success(function(answer) {
                    deferred.resolve(answer);
                })
                .error(function() {
                    deferred.reject();
                });
            return deferred.promise;
        },

        /* Use this function in order to get instances of all the users */
        loadAllUsers: function() {
            var deferred = $q.defer();
            var scope = this;
            $log.debug('UsersManager: will fetch all users from server');
            $http.get('api/users', { tracker: 'getUsers', cache: this._cacheManager })
                .success(function(usersArray) {
                    var users = [];
                    usersArray.forEach(function(userData) {
                        var user = scope._retrieveInstance(userData.Username, userData);
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
            $log.debug('UsersManager: will set user ' + userData.Username + ' to -' + angular.toJson(userData));
            var scope = this;
            var user = this._search(userData.Username);
            if (user) {
                user.setData(userData);
            } else {
                user = scope._retrieveInstance(userData.Username,userData);
            }
            return user;
        }

    };
    return usersManager;
}]);
