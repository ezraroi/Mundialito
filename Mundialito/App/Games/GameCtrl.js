'use strict';
angular.module('mundialitoApp').controller('GameCtrl', ['$scope', '$log', 'BetsManager', 'game', 'userBet','Alert', function ($scope, $log, BetsManager, game, userBet, Alert) {
    $scope.game = game;
    $scope.userBet = userBet;
    $scope.userBet.GameId = game.GameId;
    $scope.showEditForm = false;

    if (!$scope.game.IsOpen)
    {
        BetsManager.getGameBets($scope.game.GameId).then(function (data) {
            $log.debug("GameCtrl: get game bets" + angular.toJson(data));
            $scope.gameBets = data;
        });
    }

    $scope.updateGame = function() {
        $scope.game.update().success(function() {
            Alert.new('success', 'Game was updated successfully', 2000);
        })
    };

    $scope.updateBet = function() {
        if ($scope.userBet.BetId !== -1) {
            $scope.userBet.update().then(function() {
                Alert.new('success', 'Bet was updated successfully', 2000);
            });
        }
        else {
            BetsManager.addBet($scope.userBet).then(function (data) {
                $log.log('GameCtrl: Bet ' + data.BetId + ' was added');
                $scope.userBet = data;
                Alert.new('success', 'Bet was added successfully', 2000);
            });
        }
    };
}]);