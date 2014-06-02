'use strict';
angular.module('mundialitoApp').factory('Stadium', ['$http','$log', function($http,$log) {
    function Stadium(stadiumData) {
        if (stadiumData) {
            this.setData(stadiumData);
        }
        // Some other initializations related to stadium
    };

    Stadium.prototype = {
        setData: function(stadiumData) {
            angular.extend(this, stadiumData);
        },
        delete: function() {
            if (confirm('Are you sure you would like to delete stadium ' + this.Name)) {
                $log.debug('Stadium: Will delete stadium ' + this.StadiumId)
                return $http.delete("api/stadiums/" + this.StadiumId, { tracker: 'deleteStadium' });
            }
        },
        update: function() {
            $log.debug('Stadium: Will update stadium ' + this.StadiumId)
            var stadiumToUpdate = {};
            angular.copy(this,stadiumToUpdate);
            delete stadiumToUpdate.Games;
            return $http.put("api/stadiums/" + this.StadiumId, stadiumToUpdate, { tracker: 'editStadium' });
        },
        getUrl: function() {
            return '/stadiums/' + this.StadiumId;
        }
    };
    return Stadium;
}]);
