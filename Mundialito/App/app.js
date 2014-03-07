angular.module('mundialitoApp', ['security', 'ngSanitize', 'ngRoute', 'ngAnimate', 'ui.bootstrap', 'autoFields', 'cgBusy', 'ajoslin.promise-tracker', 'angular-bootstrap-select', 'angular-bootstrap-select.extra', 'ui.bootstrap.datetimepicker', 'FacebookPluginDirectives'])
.value('cgBusyTemplateName','App/Partials/angular-busy.html')
.config(['$routeProvider', '$httpProvider', '$locationProvider', '$parseProvider', 'securityProvider', function ($routeProvider, $httpProvider, $locationProvider, $parseProvider, securityProvider) {
    $locationProvider.html5Mode(true);
    $httpProvider.defaults.headers.common['X-Requested-With'] = 'XMLHttpRequest';
    $httpProvider.interceptors.push('myHttpInterceptor');
    securityProvider.urls.login = '/login';

    $routeProvider.
    when('/', {
        templateUrl: 'App/Partials/Home.html'
    }).
    when('/teams', {
        templateUrl: 'App/Partials/Teams.html',
        controller: 'TeamsCtrl',
        resolve: {
            teams: function ($q,  TeamsService) {
                var deferred = $q.defer();
                TeamsService.getTeams().success(function (data) {
                    deferred.resolve(data);
                });
                return deferred.promise;
            }
        }
    }).
    when('/teams/:teamId', {
        templateUrl: 'App/Partials/Team.html',
        controller: 'TeamCtrl',
        resolve: {
            team: function ($q, $route, TeamsService) {
                var deferred = $q.defer();
                var teamId = $route.current.params.teamId;
                TeamsService.getTeam(teamId).success(function (data) {
                    deferred.resolve(data);
                });
                return deferred.promise;
            }
        }
    }).
    when('/games/:gameId', {
        templateUrl: 'App/Partials/Game.html',
        controller: 'GameCtrl',
        resolve: {
            game: function ($q, $route, GamesService) {
                var deferred = $q.defer();
                var gameId = $route.current.params.gameId;
                GamesService.getGame(gameId).success(function (data) {
                    deferred.resolve(data);
                });
                return deferred.promise;
            },
            userBet: function ($q, $route, BetsService) {
                var deferred = $q.defer();
                var gameId = $route.current.params.gameId;
                BetsService.getUserBetOnGame(gameId).success(function (data) {
                    deferred.resolve(data);
                });
                return deferred.promise;
            }
        }
    }).
    when('/games', {
        templateUrl: 'App/Partials/Games.html',
        controller: 'GamesCtrl',
        resolve: {
            games: function ($q, GamesService) {
                var deferred = $q.defer();
                GamesService.getGames().success(function (data) {
                    deferred.resolve(data);
                });
                return deferred.promise;
            },
            teams : function ($q, TeamsService) {
                var deferred = $q.defer();
                TeamsService.getTeams().success(function(data) {
                    deferred.resolve(data);
                });
                return deferred.promise;
            },
            stadiums : function ($q, StadiumsService) {
                var deferred = $q.defer();
                StadiumsService.getStadiums().success(function(data) {
                    deferred.resolve(data);
                });
                return deferred.promise;
            }
        }
    }).
    when('/login', {
        templateUrl: 'App/Partials/Login.html'
    }).
    when('/join', {
        templateUrl: 'App/Partials/Register.html'
    }).
    when('/manage', {
        templateUrl: 'App/Partials/Manage.html'
    }).
    otherwise({
        redirectTo: '/'
    });
}])
.run(['$rootScope', '$log', 'security', '$route', '$location', function ($rootScope, $log, security, $route, $location) {
    $rootScope.location = $location;
    $rootScope.mundialitoApp = {
        params: null,
        loading: true,
        authenticating: true,
        message: null
    };
    $rootScope.security = security;

    security.events.login = function (security, user) {
        $log.log('Current user details: ' + angular.toJson(user));
        $rootScope.mundialitoApp.authenticating = false;
    };

    security.events.reloadUser = function (security, user) {
        $log.log('User reloaded' + angular.toJson(user));
        $rootScope.mundialitoApp.authenticating = false;
    };

    security.events.logout = function (security) {
        $log.log('User logged out');
        security.authenticate();
    };
   
    $rootScope.$on('$locationChangeStart', function () {
        $log.debug('$locationChangeStart');
        $rootScope.mundialitoApp.loading = true;
    });
    $rootScope.$on('$locationChangeSuccess', function () {
        $log.debug('$locationChangeSuccess');
        $rootScope.mundialitoApp.params = angular.copy($route.current.params);
        $rootScope.mundialitoApp.loading = false;
    });

    $rootScope.$on('$routeChangeStart', function () {
        $log.debug('$routeChangeStart');
        $rootScope.mundialitoApp.message = 'Loading...';
    });
    $rootScope.$on('$routeChangeSuccess', function () {
        $log.debug('$routeChangeSuccess');
        $rootScope.mundialitoApp.message = null;
    });

}]);