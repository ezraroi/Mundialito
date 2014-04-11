angular.module('mundialitoApp').factory('TeamsManager', ['$http', '$q', 'Team','$log', function($http,$q,Team,$log) {
    var teamsManager = {
        _pool: {},
        _retrieveInstance: function(teamId, teamData) {
            var instance = this._pool[teamId];

            if (instance) {
                $log.debug('TeamsManager: updating existing instance of team ' + teamId);
                instance.setData(teamData);
            } else {
                $log.debug('TeamsManager: saving new instance of team ' + teamId);
                instance = new Team(teamData);
                this._pool[teamId] = instance;
            }
            return instance;
        },
        _search: function(teamId) {
            $log.debug('TeamsManager: will fetch team ' + teamId + ' from local pool');
            return this._pool[teamId];
        },
        _load: function(teamId, deferred) {
            var scope = this;
            $log.debug('TeamsManager: will fetch team ' + teamId + ' from server');
            $http.get("api/teams/" + teamId, { tracker: 'getTeam'})
                .success(function(teamData) {
                    var team = scope._retrieveInstance(teamData.TeamId, teamData);
                    deferred.resolve(team);
                })
                .error(function() {
                    deferred.reject();
                });
        },

        /* Public Methods */

        getTeamSchema: function() {
            return [
                { property: 'Name', label: 'Name', type: 'text', attr: { required: true } },
                { property: 'Flag', label: 'Flag', type: 'url', attr: { required: true } },
                { property: 'Logo', label: 'Logo', type: 'url', attr: { required: true } },
                { property: 'ShortName', label: 'Short Name', type: 'text', attr: { ngMaxlength: 3, ngMinlength: 3, required: true } },
            ];
        },

        /* Use this function in order to get a new empty team data object */
        getEmptyTeamObject: function() {
            return {
                Name: '',
                Flag: '',
                Logo: '',
                ShortName: ''
            }
        },

        /* Use this function in order to add a new team */
        addTeam: function(teamData) {
            var deferred = $q.defer();
            var scope = this;
            $log.debug('TeamsManager: will add new team - ' + angular.toJson(teamData));
            $http.post("api/teams", teamData, {  tracker: 'addTeam'})
                .success(function(data) {
                    var team = scope._retrieveInstance(data.TeamId, data);
                    deferred.resolve(team);
                })
                .error(function() {
                    deferred.reject();
                });
            return deferred.promise;
        },

        /* Use this function in order to get a team instance by it's id */
        getTeam: function(teamId,fresh) {
            var deferred = $q.defer();
            var team = undefined;
            if ((!angular.isDefined(fresh) || (!fresh))) {
                team = this._search(teamId);
            }
            if (team) {
                deferred.resolve(team);
            } else {
                this._load(teamId, deferred);
            }
            return deferred.promise;
        },

        /* Use this function in order to get instances of all the teams */
        loadAllTeams: function() {
            var deferred = $q.defer();
            var scope = this;
            $log.debug('TeamsManager: will fetch all teams from server');
            $http.get("api/teams", { tracker: 'getTeams' })
                .success(function(teamsArray) {
                    var teams = [];
                    teamsArray.forEach(function(teamData) {
                        var team = scope._retrieveInstance(teamData.TeamId, teamData);
                        teams.push(team);
                    });

                    deferred.resolve(teams);
                })
                .error(function() {
                    deferred.reject();
                });
            return deferred.promise;
        },

        /*  This function is useful when we got somehow the team data and we wish to store it or update the pool and get a team instance in return */
        setTeam: function(teamData) {
            $log.debug('TeamsManager: will set team ' + teamData.TeamId + ' to -' + angular.toJson(teamData));
            var scope = this;
            var team = this._search(teamData.TeamId);
            if (team) {
                team.setData(teamData);
            } else {
                team = scope._retrieveInstance(teamData);
            }
            return team;
        }

    };
    return teamsManager;
}]);
