'use strict';
angular.module('mundialitoApp').controller('UserProfileCtrl', ['$scope', '$log', '$routeParams', 'Alert','GeneralBetsManager', 'profileUser','userGameBets', 'teams', 'generalBetsAreOpen', function ($scope, $log, $routeParams, Alert, GeneralBetsManager, profileUser, userGameBets, teams, generalBetsAreOpen) {
    $scope.profileUser = profileUser;
    $scope.userGameBets = userGameBets;
    $scope.teams = teams;
    $scope.noGeneralBetWasSubmitted = false;
    $scope.generalBetsAreOpen = generalBetsAreOpen;

    $scope.isLoggedUserProfile = function() {
        return ($scope.security.user != null) && ($scope.security.user.userName === $scope.profileUser.Username);
    };

    $scope.isGeneralBetClosed = function() {
        return !$scope.generalBetsAreOpen;
    };

    $scope.isGeneralBetReadOnly = function() {
        return (!$scope.isLoggedUserProfile() || ($scope.isGeneralBetClosed()));
    }

    if (!$scope.isGeneralBetReadOnly()) {
        GeneralBetsManager.hasGeneralBet($scope.profileUser.Username).then(function (answer) {
            $log.debug('UserProfileCtrl: hasGeneralBet = ' + answer);
            if (answer === 'true') {
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
                Alert.new('success', 'General Bet was updated successfully', 2000);
            });
        }
        else
        {
            GeneralBetsManager.addGeneralBet($scope.generalBet).then(function(data) {
                $log.log('UserProfileCtrl: General Bet ' + data.GeneralBetId + ' was added');
                $scope.generalBet = data;
                Alert.new('success', 'General Bet was added successfully', 2000);
            });
        }
    };


}]);