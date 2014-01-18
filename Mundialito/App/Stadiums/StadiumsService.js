'use strict';

angular.module('mundialitoApp')
.factory('StadiumsService', ['$http', function ($http) {
    var StadiumsApi = {
        getStadiums: function () {
            return $http.get("api/stadiums", { tracker: 'getStadiums' });
        },
        getEmptyStadiumObject: function () {
            return {
                HomeTeam: '',
                AwayTeam: ''
            };
        }
    };
    return StadiumsApi;
}]);