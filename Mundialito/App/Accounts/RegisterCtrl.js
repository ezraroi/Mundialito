'use strict';
angular.module('mundialitoApp').controller('RegisterCtrl', ['$scope', 'security', function ($scope, Security) {
    Security.redirectAuthenticated('/');
    $scope.mundialitoApp.authenticating = false;

    var User = function () {
        return {
            firstname: '',
            lastname: '',
            email: '',
            username: '',
            password: '',
            confirmPassword: '',
            privateKey: ''
        }
    }

    $scope.user = new User();
    $scope.join = function () {
        if (!$scope.joinForm.$valid) return;
        $scope.isJoinActive = true;
        $scope.mundialitoApp.message = "Processing Registration...";
        Security.register(angular.copy($scope.user)).finally(function () {
            $scope.mundialitoApp.message = null;
            $scope.isJoinActive = false;
        });
    };

    $scope.schema = [
            { property: 'firstname', label: 'First Name', type: 'text', attr: { required: true } },
            { property: 'lastname', label: 'Last Name', type: 'text', attr: { required: true } },
            { property: 'email', label: 'Email Address', type: 'email', attr: { required: true } },
            { property: 'username', type: 'text', attr: { ngMinlength: 4, required: true } },
            { property: 'password', type: 'password', attr: { required: true } },
            { property: 'confirmPassword', label: 'Confirm Password', type: 'password', attr: { confirmPassword: 'user.password', required: true } },
            { property: 'privateKey', help: 'The Private Key was given in the e-mail of the payment confirmation', label: 'Private Key', type: 'text', attr: { required: true } }
    ];
}]);