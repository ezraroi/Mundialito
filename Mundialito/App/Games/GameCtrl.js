'use strict';
angular.module('mundialitoApp').controller('GameCtrl', ['$scope', '$log', 'GamesManager', 'BetsManager', 'game', 'userBet','Alert', function ($scope, $log, GamesManager, BetsManager, game, userBet, Alert) {
    $scope.game = game;
    $scope.userBet = userBet;
    $scope.userBet.GameId = game.GameId;
    $scope.showEditForm = false;

    if (!$scope.game.IsOpen)
    {
        BetsManager.getGameBets($scope.game.GameId).then(function (data) {
            $log.debug("GameCtrl: get game bets" + angular.toJson(data));
            $scope.gameBets = data;

            var chart1 = {};
            chart1.type = "PieChart";
            chart1.options = {
                displayExactValues: true,
                is3D: true,
                backgroundColor: { fill:'transparent' },
                chartArea: {left:10,top:20,bottom:0,height:"100%"},
                title: 'Bets Distribution'
            };
            var mark1 = _.filter(data, function(bet) { return bet.HomeScore > bet.AwayScore}).length;
            var markX = _.filter(data, function(bet) { return bet.HomeScore === bet.AwayScore}).length;
            var mark2 = _.filter(data, function(bet) { return bet.HomeScore < bet.AwayScore}).length;
            chart1.data = [
                ['Game Mark', 'Number Of Users'],
                ['1', mark1],
                ['X', markX],
                ['2', mark2]
            ];
            $scope.chart = chart1;
        });
    }

    $scope.updateGame = function() {
        if  ((angular.isDefined(game.Stadium.Games)) && (game.Stadium.Games != null)) {
            delete game.Stadium.Games;
        }
        $scope.game.update().success(function(data) {
            Alert.new('success', 'Game was updated successfully', 2000);
            GamesManager.setGame(data);
        })
    };

    $scope.updateBet = function() {
        if ($scope.userBet.BetId !== -1) {
            $scope.userBet.update().success(function(data) {
                Alert.new('success', 'Bet was updated successfully', 2000);
                BetsManager.setBet(data);
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