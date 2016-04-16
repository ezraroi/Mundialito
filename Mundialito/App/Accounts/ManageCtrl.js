'use strict';
angular.module('mundialitoApp').controller('ManageCtrl', ['$scope','Alert', function ($scope, Alert) {
    var ChangePasswordModel = function () {
        return {
            oldPassword: '',
            newPassword: '',
            confirmPassword: ''
        }
    };
    
    $scope.changingPassword = null;
    $scope.changePassword = function () {
        $scope.changingPassword = new ChangePasswordModel();
    }
    $scope.cancel = function () {
        $scope.changingPassword = null;
    }
    $scope.updatePassword = function () {
        if (!$scope.manageForm.$valid) return;
        var newPassword = angular.copy($scope.changingPassword);
        $scope.changingPassword = null;
        $scope.security.changePassword(newPassword).then(function () {
            Alert.success("Password was changed sucessfully");
        }, function () {
            Alert.error("Failed to change password");
            $scope.changingPassword = newPassword;
        });
    }
    $scope.changePasswordSchema = [
            { property: 'oldPassword', type: 'password', attr: { required: true } },
            { property: 'newPassword', type: 'password', attr: { ngMinlength: 4, required: true } },
            { property: 'confirmPassword', type: 'password', attr: { confirmPassword: 'changingPassword.newPassword', required: true } }
    ];
}]);