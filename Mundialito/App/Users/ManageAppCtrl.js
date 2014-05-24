'use strict';
angular.module('mundialitoApp').controller('ManageAppCtrl', ['$scope', '$log', 'Alert', 'users','generalBets','UsersManager', function ($scope, $log, Alert, users, generalBets, UsersManager) {
    $scope.users = users;
    $scope.generalBets = generalBets;
    $scope.privateKey = {};

    $scope.deleteUser = function(user) {
        var scope = user;
        if (confirm('Are you sure you would like to delete ' + user.Name + '?')) {
            user.delete().success(function () {
                Alert.new('success', 'User was deleted successfully', 2000);
                $scope.users.splice($scope.users.indexOf(scope), 1);
            })
        }
    };

    $scope.generateKey = function() {
        $scope.privateKey.key = '';
        UsersManager.generatePrivateKey($scope.privateKey.email).then(function(data) {
            $log.debug('ManageAppCtrl: got private key ' + data);
            $scope.privateKey.key = data;
        });
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