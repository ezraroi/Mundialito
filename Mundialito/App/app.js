angular.module('mundialitoApp', ['security', 'ngSanitize', 'ngRoute', 'ui.bootstrap', 'autoFields'])
.config(['$routeProvider', '$httpProvider', '$locationProvider', '$parseProvider', 'securityProvider', function ($routeProvider, $httpProvider, $locationProvider, $parseProvider, securityProvider) {
    $locationProvider.html5Mode(true);
    $httpProvider.defaults.headers.common["X-Requested-With"] = "XMLHttpRequest";
    $httpProvider.interceptors.push('myHttpInterceptor');
    securityProvider.urls.login = '/login';


    securityProvider.events.login = function (security, user) {
        console.log("Current user details: ");
        console.dir(user);
    }

    securityProvider.events.reloadUser = function (security, user) {
        console.log("User reloaded: ");
        console.dir(user);
    }

    securityProvider.events.logout = function (security) {
        console.log("User logged out");
        security.authenticate();
    }

    $routeProvider
	.when('/:controller?/:action?/:id?', {
	    template: '<ng-include src="include"></ng-include>',
	    controller: 'DynamicCtrl'
	})
	.otherwise({
	    redirectTo: '/'
	});
}])
.run(['$rootScope', 'security', '$route', function ($rootScope, security, $route) {
    $rootScope.mundialitoApp = {
        params: null,
        loading: true
    };
    $rootScope.security = security;

    $rootScope.$on('$locationChangeStart', function (event) {
        $rootScope.mundialitoApp.loading = true;
    });
    $rootScope.$on('$locationChangeSuccess', function (event) {
        $rootScope.mundialitoApp.params = angular.copy($route.current.params);
        $rootScope.mundialitoApp.loading = false;
    });
}])
.controller('DynamicCtrl', ['$rootScope', '$scope', '$routeParams', function ($rootScope, $scope, $routeParams) {
    $rootScope.mundialitoApp.title = $routeParams.action;
    var route = [];
    if ($routeParams.controller != null && $routeParams.controller != '') route.push($routeParams.controller);
    if ($routeParams.action != null && $routeParams.action != '') route.push($routeParams.action);
    $scope.include = ('/' + route.join('/')).toLowerCase();
}]);