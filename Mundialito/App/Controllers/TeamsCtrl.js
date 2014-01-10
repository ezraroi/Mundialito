angular.module('mundialitoApp')
.controller('TeamsCtrl', ['$scope', '$http' ,'security', 'Alert', function ($scope, $http ,Security, Alert) {

    Security.authenticate();

    $http.get('api/teams').success(function (data) {
        $scope.teams = data;
    });

    var Team = function () {
        return {
            name: '',
            flag: '',
            logo: '',
            shortname: ''
        }
    }

    $scope.addTeam = function () {
        if (!$scope.addTeamForm.$valid) return;
        $scope.message = "Adding new team...";
        $http({ method: 'POST', url: 'api/teams', data: $scope.team }).success(function () {
            Alert.new('success', 'Added new team successfully', 2000);
            $scope.message = null;
            $http.get('api/teams').success(function (data) {
                $scope.teams = data;
            });
        });
    }
    $scope.team = new Team();

    $scope.schema = [
            { property: 'name', label: 'Name', type: 'text', attr: { required: true } },
            { property: 'flag', label: 'Flag', type: 'url', attr: { required: true } },
            { property: 'logo', label: 'Logo', type: 'url', attr: { required: true } },
            { property: 'shortname', type: 'text', attr: { ngMaxlength: 3, ngMinlength: 3, required: true } },
    ];
}]);