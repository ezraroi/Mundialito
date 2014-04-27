﻿'use strict';
angular.module('mundialitoApp').controller('GameCtrl', ['$scope', '$rootScope', '$log', 'BetsService', 'game', 'userBet','Alert', function ($scope, $rootScope, $log, BetsService, game, userBet, Alert) {
    $scope.game = game;
    $scope.userBet = userBet;
    $scope.userBet.GameId = game.GameId;
    $scope.showEditForm = false;

    BetsService.getGameBets($scope.game.GameId).success(function (data, status) {
        $log.debug("GameCtrl: BetsService.getGameBets Success (" + status + "): " + angular.toJson(data));
        $scope.gameBets = data;
    });

    $scope.updateGame = function() {
        $scope.game.update().success(function() {
            Alert.new('success', 'Game was added successfully', 2000);
        })
    };

}]);