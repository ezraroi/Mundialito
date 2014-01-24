angular.module('mundialitoApp')
.controller('JoinCtrl', ['$scope', 'security', '$modal', function ($scope, Security, $modal) {
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
        }
    }

    $scope.user = new User();
    $scope.join = function () {
        if (!$scope.joinForm.$valid) return;
        $scope.mundialitoApp.message = "Processing Registration...";
        Security.register(angular.copy($scope.user)).finally(function () {
            $scope.mundialitoApp.message = null;
        });
    };
    $scope.schema = [
            { property: 'firstname', label: 'First Name', type: 'text', attr: { required: true } },
            { property: 'lastname', label: 'Last Name', type: 'text', attr: { required: true } },
            { property: 'email', label: 'Email Address', type: 'email', attr: { required: true } },
            { property: 'username', type: 'text', attr: { ngMinlength: 4, required: true } },
            { property: 'password', type: 'password', attr: { required: true } },
            { property: 'confirmPassword', label: 'Confirm Password', type: 'password', attr: { confirmPassword: 'user.password', required: true } }
    ];
}]);