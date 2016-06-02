'use strict';
angular.module('mundialitoApp').controller('UserProfileCtrl', ['$scope', '$log', '$routeParams', 'Alert','GeneralBetsManager', 'profileUser','userGameBets', 'teams', 'generalBetsAreOpen', function ($scope, $log, $routeParams, Alert, GeneralBetsManager, profileUser, userGameBets, teams, generalBetsAreOpen) {
    $scope.profileUser = profileUser;
    $scope.userGameBets = userGameBets;
    $scope.teams = teams;
    $scope.noGeneralBetWasSubmitted = false;
    $scope.generalBetsAreOpen = (generalBetsAreOpen === true);
    $log.debug('UserProfileCtrl: generalBetsAreOpen = ' + generalBetsAreOpen);

    $scope.isLoggedUserProfile = function() {
        var res = ($scope.security.user != null) && ($scope.security.user.userName === $scope.profileUser.Username);
        $log.debug('UserProfileCtrl: isLoggedUserProfile = ' + res);
        return ($scope.security.user != null) && ($scope.security.user.userName === $scope.profileUser.Username);
    };

    $scope.isGeneralBetClosed = function() {
        var res = !$scope.generalBetsAreOpen;
        $log.debug('UserProfileCtrl: isGeneralBetClosed = ' + res);
        return res;
    };

    $scope.isGeneralBetReadOnly = function() {
        var res = (!$scope.isLoggedUserProfile() || ($scope.isGeneralBetClosed()));
        $log.debug('UserProfileCtrl: isGeneralBetReadOnly = ' + res);
        return res;
    }

    $scope.shoudLoadGeneralBet = function() {
        var res = ($scope.isLoggedUserProfile() || ($scope.isGeneralBetClosed()));
        $log.debug('UserProfileCtrl: shoudLoadGeneralBet = ' + res);
        return res;
    }

    if ($scope.shoudLoadGeneralBet()) {
        GeneralBetsManager.hasGeneralBet($scope.profileUser.Username).then(function (answer) {
            $log.debug('UserProfileCtrl: hasGeneralBet = ' + answer);
            if (answer === true) {
                GeneralBetsManager.getUserGeneralBet($scope.profileUser.Username).then(function (generalBet) {
                    $log.debug('UserProfileCtrl: got user general bet - ' + angular.toJson(generalBet));
                    $scope.generalBet = generalBet
                });
            }
            else {
                $scope.generalBet = {};
                if ($scope.isGeneralBetClosed()) {
                    $scope.noGeneralBetWasSubmitted = true;
                    return;
                }
                if ($scope.isLoggedUserProfile() && !$scope.isGeneralBetClosed()) {
                    return;
                }
                $scope.noGeneralBetWasSubmitted = true;
            }
        });
    }

    $scope.saveGeneralBet = function() {
        if (angular.isDefined($scope.generalBet.GeneralBetId))
        {
            $scope.generalBet.update().then(function() {
                Alert.success('General Bet was updated successfully');
            }, function () {
                Alert.error('Failed to update General Bet, please try again');
            });
        }
        else
        {
            GeneralBetsManager.addGeneralBet($scope.generalBet).then(function(data) {
                $log.log('UserProfileCtrl: General Bet ' + data.GeneralBetId + ' was added');
                $scope.generalBet = data;
                Alert.success('General Bet was added successfully');
            }, function () {
                Alert.error('Failed to add General Bet, please try again');
            });
        }
    };


}]);
