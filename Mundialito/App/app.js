angular.module('mundialitoApp', ['security', 'ngSanitize', 'ngRoute', 'ngAnimate', 'ui.bootstrap', 'autoFields', 'cgBusy', 'ajoslin.promise-tracker', 'ui.select2', 'ui.bootstrap.datetimepicker', 'FacebookPluginDirectives','ngGrid','googlechart','angular-data.DSCacheFactory'])
    .value('cgBusyTemplateName','App/Partials/angular-busy.html')
    .config(['$routeProvider', '$httpProvider', '$locationProvider', '$parseProvider', 'securityProvider','Constants', function ( $routeProvider, $httpProvider, $locationProvider, $parseProvider, securityProvider, Constants) {
        $locationProvider.html5Mode(true);
        $httpProvider.defaults.headers.common['X-Requested-With'] = 'XMLHttpRequest';
        $httpProvider.interceptors.push('myHttpInterceptor');
        securityProvider.urls.login = Constants.LOGIN_PATH;
        securityProvider.usePopups = false;

        $routeProvider.
            when('/', {
                templateUrl: 'App/Dashboard/Dashboard.html',
                controller: 'DashboardCtrl',
                resolve : {
                    teams : ['TeamsManager', function ( TeamsManager) {
                        return TeamsManager.loadAllTeams();
                    }]
                }
            }).
            when('/bets_center', {
                templateUrl: 'App/Bets/BetsCenter.html',
                controller: 'BetsCenterCtrl',
                resolve : {
                    games : ['GamesManager', function (GamesManager) {
                        return GamesManager.loadOpenGames();
                    }]
                }
            }).
            when('/users/:username', {
                templateUrl: 'App/Users/UserProfile.html',
                controller: 'UserProfileCtrl',
                resolve : {
                    profileUser: ['$route', 'UsersManager', function ($route, UsersManager) {
                        var username = $route.current.params.username;
                        return UsersManager.getUser(username, true);
                    }],
                    userGameBets : ['$route', 'BetsManager', function ($route, BetsManager) {
                        var username = $route.current.params.username;
                        return BetsManager.getUserBets(username);
                    }],
                    teams : ['TeamsManager', function ( TeamsManager) {
                        return TeamsManager.loadAllTeams();
                    }],
                    generalBetsAreOpen : ['GeneralBetsManager', function (GeneralBetsManager) {
                        return GeneralBetsManager.canSubmtiGeneralBet();
                    }]
                }
            }).
            when('/manage_users', {
                templateUrl: 'App/Users/ManageApp.html',
                controller: 'ManageAppCtrl',
                resolve: {
                    users : ['UsersManager', function (UsersManager) {
                        return UsersManager.loadAllUsers();
                    }],
                    teams : ['TeamsManager', function (TeamsManager) {
                        return TeamsManager.loadAllTeams();
                    }],
                    generalBets: ['GeneralBetsManager' , function (GeneralBetsManager) {
                        return GeneralBetsManager.loadAllGeneralBets();
                    }]
                }
            }).
            when('/teams', {
                templateUrl: 'App/Teams/Teams.html',
                controller: 'TeamsCtrl',
                resolve: {
                    teams: ['TeamsManager', function (TeamsManager) {
                        return TeamsManager.loadAllTeams();
                    }]
                }
            }).
            when('/teams/:teamId', {
                templateUrl: 'App/Teams/Team.html',
                controller: 'TeamCtrl',
                resolve: {
                    team: ['$route','TeamsManager',  function ($route, TeamsManager) {
                        var teamId = $route.current.params.teamId;
                        return TeamsManager.getTeam(teamId);
                    }],
                    games : ['$route','GamesManager', function ($route, GamesManager) {
                        var teamId = $route.current.params.teamId;
                        return GamesManager.getTeamGames(teamId);
                    }]
                }
            }).
            when('/games/:gameId', {
                templateUrl: 'App/Games/Game.html',
                controller: 'GameCtrl',
                resolve: {
                    game: ['$route','GamesManager', function ($route, GamesManager) {
                        var gameId = $route.current.params.gameId;
                        return  GamesManager.getGame(gameId);
                    }],
                    userBet: ['$route','BetsManager', function ($route, BetsManager) {
                        var gameId = $route.current.params.gameId;
                        return BetsManager.getUserBetOnGame(gameId);
                    }]
                }
            }).
            when('/games', {
                templateUrl: 'App/Games/Games.html',
                controller: 'GamesCtrl',
                resolve: {
                    games: ['GamesManager', function (GamesManager) {
                        return GamesManager.loadAllGames();
                    }],
                    teams : ['TeamsManager', function (TeamsManager) {
                        return TeamsManager.loadAllTeams();
                    }],
                    stadiums : ['StadiumsManager', function (StadiumsManager) {
                        return StadiumsManager.loadAllStadiums();
                    }]
                }
            }).
            when('/stadiums/:stadiumId', {
                templateUrl: 'App/Stadiums/Stadium.html',
                controller: 'StadiumCtrl',
                resolve: {
                    stadium: ['$q','$route','StadiumsManager',  function ($q, $route, StadiumsManager) {
                        var stadiumId = $route.current.params.stadiumId;
                        return StadiumsManager.getStadium(stadiumId, true);
                    }]
                }
            }).
            when('/stadiums', {
                templateUrl: 'App/Stadiums/Stadiums.html',
                controller: 'StadiumsCtrl',
                resolve: {
                    stadiums : ['StadiumsManager', function (StadiumsManager) {
                        return StadiumsManager.loadAllStadiums();
                    }]
                }
            }).
            when('/login', {
                templateUrl: 'App/Accounts/Login.html'
            }).
            when('/join', {
                templateUrl: 'App/Accounts/Register.html'
            }).
            when('/manage', {
                templateUrl: 'App/Accounts/Manage.html'
            }).
            otherwise({
                redirectTo: '/'
            });
    }])
    .run(['$rootScope', '$log', 'security', '$route', '$location', function ($rootScope, $log, security, $route, $location) {
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
        $rootScope.mundialitoApp = {
            params: null,
            loading: true,
            authenticating: true,
            message: null
        };
        security.authenticate();
        $rootScope.security = security;

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

        // Fix for bootstrap navbar on SPA applications
        $(document).on('click.nav','.navbar-collapse.in',function(e) {
            if( $(e.target).is('a') ) {
                $(this).removeClass('in').addClass('collapse');
            }
        });

    }]);