'use strict';

angular.module('mundialitoApp')
.factory('TeamsService', ['$rootScope', '$http' , function ($rootScope, $http) {
    var TeamsApi = {
        getTeams: function () {
            return $http.get("api/teams", { tracker: 'getTeams' });
        },
        addTeam: function (teamData) {
            return $http.post("api/teams", {  tracker: 'addTeam', data: teamData });
        },
        editTeam: function (teamData) {
            return $http.put("api/teams/" + teamData.TeamId, { tracker: 'editTeam', data: teamData });
        },
        deleteTeam: function (teamId) {
            return $http.delete("api/teams/" + teamId, { tracker: 'deleteTeam', method: 'DELETE' });
        },
        getTeam: function (teamId) {
            return $http.get("api/teams/" + teamId, { tracker: 'getTeam', method: 'GET' });
        },
        getTeamGames: function(teamId) {
            return $http.get("api/teams/" + teamId + "/games", { tracker: 'getTeamGames', method: 'GET' });
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