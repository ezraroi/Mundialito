'use strict';

angular.module('mundialitoApp').directive('saveBetButton', ['$log', 'BetsService', 'Alert', function ($log, BetsService, Alert) {
    return {
        restrict: "A",
        scope: {
            bet : '='
        },
        link: function (scope, element) {
            element.bind("click", function () {
                BetsService.addBetOnGame(scope.newGame).success(function (data) {
                    $log.log('Bet ' + data.BetId + ' was added');
                    Alert.new('success', 'Bet was added successfully', 2000);
                });
            });
        }
    };
}]);