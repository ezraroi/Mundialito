'use strict';
angular.module('mundialitoApp').controller('LoginCtrl', ['$scope', '$rootScope' , 'security', function ($scope, $rootScope, Security) {
    Security.redirectAuthenticated('/');

    $rootScope.mundialitoApp.authenticating = false;

    var LoginModel = function () {
        return {
            username: '',
            password: '',
            rememberMe: false
        }
    };

    $scope.user = new LoginModel();
    $scope.login = function () {
        if (!$scope.loginForm.$valid) return;
        $rootScope.mundialitoApp.message = "Processing Login...";
        Security.login(angular.copy($scope.user)).finally(function () {
            $rootScope.mundialitoApp.message = null;
        });
        
    }
    $scope.schema = [
            { property: 'username', type: 'text', attr: { ngMinlength: 4, required: true } },
            { property: 'password', type: 'password', attr: { ngMinlength: 4, required: true } },
            { property: 'rememberMe', label: 'Keep me logged in', type: 'checkbox' }
    ];
}]);