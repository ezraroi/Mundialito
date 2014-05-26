'use strict';
angular.module('mundialitoApp').factory('Game', ['$http','$log', function($http,$log) {
    function Game(gameData) {
        if (gameData) {
            this.setData(gameData);
        }
        // Some other initializations related to game
    };

    Game.prototype = {
        setData: function(gameData) {
            angular.extend(this, gameData);
        },
        delete: function() {
            if (confirm('Are you sure you would like to delete game ' + this.GameId)) {
                $log.debug('Game: Will delete game ' + this.GameId)
                return $http.delete("api/games/" + this.GameId, { tracker: 'deleteGame' });
            }
        },
        update: function() {
            $log.debug('Game: Will update game ' + this.GameId)
            return $http.put("api/games/" + this.GameId, this, { tracker: 'editGame' });
        },
        getUrl: function() {
            return '/games/' + this.GameId;
        }
    };
    return Game;
}]);
