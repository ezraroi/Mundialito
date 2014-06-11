'use strict';
angular.module('mundialitoApp').factory('GamesManager', ['$http', '$q', 'Game','$log','MundialitoUtils', function($http,$q,Game,$log,MundialitoUtils) {
    var gamesManager = {
        _pool: {},
        _retrieveInstance: function(gameId, gameData) {
            var instance = this._pool[gameId];

            if (instance) {
                $log.debug('GamesManager: updating existing instance of game ' + gameId);
                instance.setData(gameData);
            } else {
                $log.debug('GamesManager: saving new instance of game ' + gameId);
                instance = new Game(gameData);
                this._pool[gameId] = instance;
            }
            instance.LoadTime = new Date();
            return instance;
        },
        _search: function(gameId) {
            $log.debug('GamesManager: will fetch game ' + gameId + ' from local pool');
            var instance = this._pool[gameId];
            if (angular.isDefined(instance) && MundialitoUtils.shouldRefreshInstance(instance)) {
                $log.debug('GamesManager: Instance was loaded at ' + instance,LoadTime + ', will reload it from server');
                return undefined;
            }
            return instance;
        },
        _load: function(gameId, deferred) {
            var scope = this;
            $log.debug('GamesManager: will fetch game ' + gameId + ' from server');
            $http.get('api/games/' + gameId, { tracker: 'getGame' })
                .success(function(gameData) {
                    var game = scope._retrieveInstance(gameData.GameId, gameData);
                    deferred.resolve(game);
                })
                .error(function() {
                    deferred.reject();
                });
        },

        /* Public Methods */
        /* Use this function in order to get a new empty game data object */
        getEmptyGameObject: function() {
            return {
                HomeTeam: '',
                AwayTeam: '',
                Date: '',
                Stadium: ''
            };
        },

        /* Use this function in order to add a new game */
        addGame: function(gameData) {
            var deferred = $q.defer();
            if (!angular.isObject(gameData.AwayTeam)) {
                gameData.AwayTeam = angular.fromJson(gameData.AwayTeam);
            }
            if (!angular.isObject(gameData.HomeTeam)) {
                gameData.HomeTeam = angular.fromJson(gameData.HomeTeam);
            }
            if (!angular.isObject(gameData.Stadium)) {
                gameData.Stadium = angular.fromJson(gameData.Stadium);
            }
            var scope = this;
            $log.debug('GamesManager: will add new game - ' + angular.toJson(gameData));
            $http.post("api/games", gameData, { tracker: 'addGame' }).success(function(data) {
                var game = scope._retrieveInstance(data.GameId, data);
                deferred.resolve(game);
            })
            .error(function() {
                deferred.reject();
            });
            return deferred.promise;
        },

        /* Use this function in order to get a game instance by it's id */
        getGame: function(gameId,fresh) {
            var deferred = $q.defer();
            var game = undefined;
            if ((!angular.isDefined(fresh) || (!fresh))) {
                game = this._search(gameId);
            }
            if (game) {
                deferred.resolve(game);
            } else {
                this._load(gameId, deferred);
            }
            return deferred.promise;
        },

        /* Use this function in order to get instances of all the games */
        loadAllGames: function() {
            var deferred = $q.defer();
            var scope = this;
            $log.debug('GamesManager: will fetch all games from server');
            $http.get('api/games', { tracker: 'getGames', cache: true })
                .success(function(gamesArray) {
                    var games = [];
                    gamesArray.forEach(function(gameData) {
                        var game = scope._retrieveInstance(gameData.GameId, gameData);
                        games.push(game);
                    });

                    deferred.resolve(games);
                })
                .error(function() {
                    deferred.reject();
                });
            return deferred.promise;
        },

        /* Use this function in order to get instances of all the open games */
        loadOpenGames: function() {
            var deferred = $q.defer();
            var scope = this;
            $log.debug('GamesManager: will fetch all open games from server');
            $http.get('api/games/open', { tracker: 'getOpenGames', cache: true })
                .success(function(gamesArray) {
                    var games = [];
                    gamesArray.forEach(function(gameData) {
                        var game = scope._retrieveInstance(gameData.GameId, gameData);
                        games.push(game);
                    });

                    deferred.resolve(games);
                })
                .error(function() {
                    deferred.reject();
                });
            return deferred.promise;
        },

        /* Use this function in order to get instances of all the games of a specific team */
        getTeamGames: function(teamId) {
            var deferred = $q.defer();
            var scope = this;
            $log.debug('GamesManager: will fetch all games of team ' + teamId + '  from server');
            $http.get('api/teams/' + teamId + '/games', { tracker: 'getTeamGames', cache: true })
                .success(function(gamesArray) {
                    var games = [];
                    gamesArray.forEach(function(gameData) {
                        var game = scope._retrieveInstance(gameData.GameId, gameData);
                        games.push(game);
                    });

                    deferred.resolve(games);
                })
                .error(function() {
                    deferred.reject();
                });
            return deferred.promise;
        } ,

        /* Use this function in order to get instances of all the games in specific stadium */
        getStadiumGames: function(stadiumId) {
            var deferred = $q.defer();
            var scope = this;
            $log.debug('GamesManager: will fetch all games in stadium ' + stadiumId + '  from server');
            $http.get('api/games/Stadium/' + stadiumId, { tracker: 'getStadiumGames', cache: true })
                .success(function(gamesArray) {
                    var games = [];
                    gamesArray.forEach(function(gameData) {
                        var game = scope._retrieveInstance(gameData.GameId, gameData);
                        games.push(game);
                    });

                    deferred.resolve(games);
                })
                .error(function() {
                    deferred.reject();
                });
            return deferred.promise;
        } ,

        /*  This function is useful when we got somehow the game data and we wish to store it or update the pool and get a game instance in return */
        setGame: function(gameData) {
            $log.debug('GamesManager: will set game ' + gameData.GameId + ' to -' + angular.toJson(gameData));
            var scope = this;
            var game = this._search(gameData.GameId);
            if (game) {
                game.setData(gameData);
            } else {
                game = scope._retrieveInstance(gameData.GameId,gameData);
            }
            return game;
        }

    };
    return gamesManager;
}]);
