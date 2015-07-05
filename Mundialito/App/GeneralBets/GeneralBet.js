'use strict';
angular.module('mundialitoApp').factory('GeneralBet', ['$http','$log', function($http,$log) {
    function GeneralBet(betData) {
        if (betData) {
            this.setData(betData);
        }
        // Some other initializations related to general bet
    };

    GeneralBet.prototype = {
        setData: function(betData) {
            angular.extend(this, betData);
        },
        update: function() {
            $log.debug('General Bet: Will update general bet ' + this.GeneralBetId);
            return $http.put('api/generalbets/' + this.GeneralBetId, this, { tracker: 'updateGeneralBet' });
        },
        resolve: function() {
            $log.debug('General Bet: Will resolve general bet ' + this.GeneralBetId);
            var data = {
                TeamIsRight: this.TeamIsRight || false,
                PlayerIsRight: this.PlayerIsRight || false
            };
            return $http.put('api/generalbets/' + this.GeneralBetId + '/resolve', data, { tracker: 'resolveGeneralBet' });
        }
    };
    return GeneralBet;
}]);
