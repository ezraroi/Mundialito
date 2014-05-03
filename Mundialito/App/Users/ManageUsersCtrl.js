'use strict';
angular.module('mundialitoApp').controller('ManageUsersCtrl', ['$scope', '$log', 'Alert', 'users', function ($scope, $log, Alert, users) {
    $scope.users = users;

    $scope.deleteUser = function(user) {
        var scope = user;
        if (confirm('Are you sure you would like to delete ' + user.Name + '?')) {
            user.delete().success(function () {
                Alert.new('success', 'User was deleted successfully', 2000);
                $scope.users.splice($scope.users.indexOf(scope), 1);
            })
        }
    };

    $scope.makeAdmin = function(user) {
        if (confirm('Are you sure you would like to make ' + user.Name + ' Admin?')) {
            user.makeAdmin.success(function () {
                Alert.new('success', 'User was isnow admin', 2000);
                user.IsAdmin = true;
            })
        }
    };

}]);