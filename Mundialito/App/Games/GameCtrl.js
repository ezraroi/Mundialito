'use strict';
angular.module('mundialitoApp').controller('GameCtrl', ['$scope', '$log', 'GamesManager', 'BetsManager', 'game', 'userBet', 'Alert', function ($scope, $log, GamesManager, BetsManager, game, userBet, Alert) {
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
        $scope.game.update().success(function (data) {
            Alert.success('Game was updated successfully');
            GamesManager.setGame(data);
        }).catch(function (err) {
            Alert.error('Failed to update game, please try again');
            $log.error('Error updating game', err);
        });
    };

    $scope.updateBet = function() {
        if ($scope.userBet.BetId !== -1) {
            $scope.userBet.update().success(function (data) {
                Alert.success('Bet was updated successfully');
                BetsManager.setBet(data);
            }).error(function (err) {
                Alert.error('Failed to update bet, please try again');
                $log.error('Error updating bet', err);
            });
        }
        else {
            BetsManager.addBet($scope.userBet).then(function (data) {
                $log.log('GameCtrl: Bet ' + data.BetId + ' was added');
                $scope.userBet = data;
                Alert.success('Bet was added successfully');
            }, function (err) {
                Alert.error('Failed to add bet, please try again');
                $log.error('Error adding bet', err);
            });
        }
    };

    $scope.sort = function(column) {
        $log.debug('GameCtrl: sorting by ' + column);
        $scope.gameBets = _.sortBy($scope.gameBets, function (item) {
            switch (column)
            {
                case 'points': return item.Points;
                case 'cards': return item.CardsMark;
                case 'corners': return item.CornersMark;
                case 'user': return item.User.FirstName + item.User.LastName;
                case 'result': return item.HomeScore + '-' + item.AwayScore;
            }
        });
    };
}]);