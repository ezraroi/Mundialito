'use strict';
angular.module('mundialitoApp').controller('ManageAppCtrl', ['$scope', '$log', 'Alert', 'users','teams', 'generalBets','UsersManager', function ($scope, $log, Alert, users, teams, generalBets, UsersManager) {
    $scope.users = users;
    $scope.generalBets = generalBets;
    $scope.privateKey = {};
    $scope.teamsDic = {};

    for(var i=0; i<teams.length; i++) {
        $scope.teamsDic[teams[i].TeamId] = teams[i];
    }

    $scope.deleteUser = function(user) {
        var scope = user;
        user.delete().success(function () {
            Alert.success('User was deleted successfully');
            $scope.users.splice($scope.users.indexOf(scope), 1);
        })
    };

    $scope.resolveBet = function(bet) {
        bet.resolve().success(function() {
            Alert.success('General bet was resolved successfully');
        });
    };

    $scope.generateKey = function() {
        $scope.privateKey.key = '';
        UsersManager.generatePrivateKey($scope.privateKey.email).then(function(data) {
            $log.debug('ManageAppCtrl: got private key ' + data);
            $scope.privateKey.key = data;
        });
    };

    $scope.makeAdmin = function(user) {
        user.makeAdmin().success(function () {
            Alert.success('User was is now admin');
            user.IsAdmin = true;
        })
    };

}]);