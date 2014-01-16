angular.module('mundialitoApp', ['security', 'ngSanitize', 'ngRoute','ngAnimate','ui.bootstrap', 'autoFields', 'cgBusy', 'ajoslin.promise-tracker','angular-bootstrap-select','angular-bootstrap-select.extra'])
.config(['$routeProvider', '$httpProvider', '$locationProvider', '$parseProvider', 'securityProvider', function ($routeProvider, $httpProvider, $locationProvider, $parseProvider, securityProvider) {
    $locationProvider.html5Mode(true);
    $httpProvider.defaults.headers.common["X-Requested-With"] = "XMLHttpRequest";
    $httpProvider.interceptors.push('myHttpInterceptor');
    securityProvider.urls.login = '/login';

    $routeProvider
	.when('/:controller?/:action?/:id?', {
	    template: '<ng-include src="include"></ng-include>',
	    controller: 'DynamicCtrl'
	})
	.otherwise({
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

    $rootScope.$on('$locationChangeStart', function (event) {
        $log.debug('$locationChangeStart');
        $rootScope.mundialitoApp.loading = true;
    });
    $rootScope.$on('$locationChangeSuccess', function (event) {
        $log.debug('$locationChangeSuccess');
        $rootScope.mundialitoApp.params = angular.copy($route.current.params);
        $rootScope.mundialitoApp.loading = false;
    });
}])
.controller('DynamicCtrl', ['$rootScope', '$scope', '$routeParams', '$log' , function ($rootScope, $scope, $routeParams, $log) {
    var route = [];
    if ($routeParams.controller != null && $routeParams.controller != '') route.push($routeParams.controller);
    if ($routeParams.action != null && $routeParams.action != '') {
        if (angular.isNumber(parseInt($routeParams.action))) {
            route.push('id');
        } else {
            route.push($routeParams.action);
        }
    }
    $scope.include = ('/' + route.join('/')).toLowerCase();
    $log.debug("Adding Dynamic Template: " + $scope.include);
}]);
