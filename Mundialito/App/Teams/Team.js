angular.module('mundialitoApp').factory('Team', ['$http','$log', function($http,$log) {
    function Team(teamData) {
        if (teamData) {
            this.setData(teamData);
        }
        // Some other initializations related to game
    };

    Team.prototype = {
        setData: function(teamData) {
            angular.extend(this, teamData);
        },
        delete: function() {
            if (confirm('Are you sure you would like to delete team ' + this.Name)) {
                $log.debug('Team: Will delete team ' + this.TeamId)
                return $http.delete("api/teams/" + this.TeamId, { tracker: 'deleteTeam'});
            }
        },
        update: function() {
            $log.debug('Team: Will update game ' + this.TeamId)
            return $http.put("api/teams/" + this.TeamId, this, { tracker: 'editTeam'});
        },
        getUrl: function() {
            return '/teams/' + this.TeamId;
        }
    };
    return Team;
}]);
