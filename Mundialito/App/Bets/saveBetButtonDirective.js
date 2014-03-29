'use strict';

angular.module('mundialitoApp').directive('saveBetButton', ['$log', 'BetsService', 'Alert', function ($log, BetsService, Alert) {
    return {
        restrict: "A",
        scope: {
            bet : '='
        },
        link: function (scope, element) {
            element.bind("click", function () {
                if (scope.bet.BetId !== -1) {
                    BetsService.updateBetOnGame(scope.bet).success(function (data) {
                        $log.log('Bet ' + data.BetId + ' was updated');
                        Alert.new('success', 'Bet was updated successfully', 2000);
                    });
                } else {
                    BetsService.addBetOnGame(scope.bet).success(function (data) {
                        $log.log('Bet ' + data.BetId + ' was added');
                        Alert.new('success', 'Bet was added successfully', 2000);
                    });
                }

            });
        }
    };
}]);