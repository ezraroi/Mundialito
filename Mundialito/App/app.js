angular.module('mundialitoApp', ['security', 'ngSanitize', 'ngRoute','ngAnimate','ui.bootstrap', 'autoFields', 'cgBusy', 'ajoslin.promise-tracker','angular-bootstrap-select','angular-bootstrap-select.extra','ui.bootstrap.datetimepicker'])
.config(['$routeProvider', '$httpProvider', '$locationProvider', '$parseProvider', 'securityProvider', function ($routeProvider, $httpProvider, $locationProvider, $parseProvider, securityProvider) {
    $locationProvider.html5Mode(true);
    $httpProvider.defaults.headers.common["X-Requested-With"] = "XMLHttpRequest";
    $httpProvider.interceptors.push('myHttpInterceptor');
    securityProvider.urls.login = '/login';

    $routeProvider.
    when('/', {
        templateUrl: 'App/Partials/Home.html',
    }).
    when('/teams', {
        templateUrl: 'App/Partials/Teams.html',
        controller: function ($scope, $log, teams) {
            $scope.teams = angular.copy(teams);
        },
        resolve: {
            teams: function ($q, $log, TeamsService) {
                var deferred = $q.defer();
                TeamsService.getTeams().success(function (data, status, headers, config) {
                    $log.debug("resolve: TeamsService.getTeams Success (" + status + "): " + angular.toJson(data));
                    deferred.resolve(data);
                });
                return deferred.promise;
            }
        }
    }).
    when('/teams/:teamId', {
        templateUrl: 'App/Partials/Team.html',
        controller: function ($scope, $log, $routeParams, team) {
            $scope.team = angular.copy(team);
            $scope.teamId = $routeParams.teamId;
        },
        resolve: {
            team: function ($q, $route, $log, TeamsService) {
                var deferred = $q.defer();
                var teamId = $route.current.params.teamId;
                TeamsService.getTeam(teamId).success(function (data, status, headers, config) {
                    $log.debug("resolve: TeamsService.getTeam Success (" + status + "):" + angular.toJson(data));
                    deferred.resolve(data);
                });
                return deferred.promise;
            }
        }
    }).
    when('/games/:gameId', {
        templateUrl: 'App/Partials/Game.html',
        controller: function ($scope, $log, $routeParams, game) {
            $scope.game = angular.copy(game);
            $scope.gameId = $routeParams.gameId;
        },
        resolve: {
            game: function ($q, $route, $log, GamesService) {
                var deferred = $q.defer();
                var gameId = $route.current.params.gameId;
                GamesService.getGame(gameId).success(function (data, status, headers, config) {
                    $log.debug("resolve: GamesService.getGame Success (" + status + "):" + angular.toJson(data));
                    deferred.resolve(data);
                });
                return deferred.promise;
            }
        }
    }).
    when('/games', {
        templateUrl: 'App/Partials/Games.html',
        controller: function ($scope, $log, $filter, games) {
            $scope.games = angular.copy(games);
            $scope.pendingUpdateGames = $filter('pendingUpdateGamesFilter')(games);
        },
        resolve: {
            games: function ($q, $log, GamesService) {
                var deferred = $q.defer();
                
                GamesService.getGames().success(function (data, status, headers, config) {
                    $log.debug("resolve: GamesService.getGames (" + status + "): " + angular.toJson(data));
                    deferred.resolve(data);
                });

                return deferred.promise;
            }
        }
    }).
    when('/login', {
        templateUrl: 'App/Partials/Login.html',
    }).
    when('/join', {
        templateUrl: 'App/Partials/Register.html',
    }).
    when('/manage', {
        templateUrl: 'App/Partials/Manage.html',
    }).
    otherwise({
        redirectTo: '/'
    });
}])
.run(['$rootScope', '$log', 'security', '$route', function ($rootScope, $log, security, $route) {
    $rootScope.mundialitoApp = {
        params: null,
        loading: true,
        authenticating: true,
        message: null
    };
    $rootScope.security = security;

    security.events.login = function (security, user) {
        $log.log("Current user details: " + angular.toJson(user));
        $rootScope.mundialitoApp.authenticating = false;
    }

    security.events.reloadUser = function (security, user) {
        $log.log("User reloaded" + angular.toJson(user));
        $rootScope.mundialitoApp.authenticating = false;
    }

    security.events.logout = function (security) {
        $log.log("User logged out");
        security.authenticate();
    }
   
    $rootScope.$on("$routeChangeStart", function (event, next, current) {
        $log.debug('$routeChangeStart');
        $rootScope.mundialitoApp.loading = true;
    });
    $rootScope.$on("$routeChangeSuccess", function (event, current, previous) {
        $log.debug('$routeChangeSuccess');
        $rootScope.mundialitoApp.loading = false;
    });

}]);