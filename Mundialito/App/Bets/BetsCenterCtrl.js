'use strict';
angular.module('mundialitoApp').controller('BetsCenterCtrl', ['$scope', '$log', '$timeout','Alert', 'BetsManager', 'games', function ($scope, $log, $timeout, Alert, BetsManager, games) {
    $scope.games = games;
    $scope.bets = {};


    var loadUserBets = function() {
        if (!angular.isDefined($scope.security.user) || ($scope.security.user == null))
        {
            $log.debug('BetsCenterCtrl: user info not loaded yet, will retry in 1 second');
            $timeout(loadUserBets,1000);
        }
        else {
            BetsManager.getUserBets($scope.security.user.userName).then(function (bets) {
                for(var i=0; i < bets.length; i++) {
                    $scope.bets[bets[i].Game.GameId] = bets[i];
                    $scope.bets[bets[i].Game.GameId].GameId = bets[i].Game.GameId
                }

                for(var j=0; j < games.length; j++) {
                    if (!angular.isDefined($scope.bets[games[j].GameId])) {
                        $log.debug('BetsCenterCtrl: game ' + games[j].GameId + ' has not bet')
                        $scope.bets[games[j].GameId] = { BetId : -1, GameId : games[j].GameId};
                    }
                    else {
                        $scope.bets[$scope.bets[games[j].GameId]] = bets[i];
                    }
                }
            });
        }
    };

    loadUserBets();

    $scope.updateBet = function(gameId) {
        if ($scope.bets[gameId].BetId !== -1) {
            $log.debug('BetsCenterCtrl: Will update bet');
            $scope.bets[gameId].update().then(function(data) {
                Alert.new('success', 'Bet was updated successfully', 2000);
                BetsManager.setBet(data);
            });
        }
        else {
            $log.debug('BetsCenterCtrl: Will create new bet');
            BetsManager.addBet($scope.bets[gameId]).then(function (data) {
                $log.log('BetsCenterCtrl: Bet ' + data.BetId + ' was added');
                $scope.bets[gameId] = data;
                Alert.new('success', 'Bet was added successfully', 2000);
            });
        }
    };
}]);