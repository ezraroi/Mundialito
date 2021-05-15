'use strict';
angular.module('mundialitoApp').factory('Player', ['$http', '$log', function ($http, $log) {
    function Player(playerData) {
        if (playerData) {
            this.setData(playerData);
        }
        // Some other initializations related to stadium
    };

    Player.prototype = {
        setData: function (playerData) {
            angular.extend(this, playerData);
        }
    };
    return Player;
}]);
