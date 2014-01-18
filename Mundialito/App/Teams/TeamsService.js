'use strict';

angular.module('mundialitoApp')
.factory('TeamsService', ['$http' , function ($http) {
    var TeamsApi = {
        getTeams: function () {
            return $http.get("api/teams", { tracker: 'getTeams' });
        },
        addTeam: function (teamData) {
            return $http.post("api/teams", teamData, {  tracker: 'addTeam'});
        },
        editTeam: function (teamData) {
            return $http.put("api/teams/" + teamData.TeamId, teamData, { tracker: 'editTeam'});
        },
        deleteTeam: function (teamId) {
            return $http.delete("api/teams/" + teamId, { tracker: 'deleteTeam'});
        },
        getTeam: function (teamId) {
            return $http.get("api/teams/" + teamId, { tracker: 'getTeam'});
        },
        getTeamGames: function(teamId) {
            return $http.get("api/teams/" + teamId + "/games", { tracker: 'getTeamGames' });
        },
        getEmptyTeamObject: function() {
            return {
                Name: '',
                Flag: '',
                Logo: '',
                ShortName: ''
            }
        },
        schema : [
                { property: 'Name', label: 'Name', type: 'text', attr: { required: true } },
                { property: 'Flag', label: 'Flag', type: 'url', attr: { required: true } },
                { property: 'Logo', label: 'Logo', type: 'url', attr: { required: true } },
                { property: 'ShortName', label: 'Short Name', type: 'text', attr: { ngMaxlength: 3, ngMinlength: 3, required: true } },
        ]
    };
    return TeamsApi;
}]);