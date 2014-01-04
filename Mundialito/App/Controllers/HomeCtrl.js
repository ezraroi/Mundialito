angular.module('mundialitoApp')
.controller('HomeCtrl', ['$scope', 'security', function ($scope, Security) {
    Security.authenticate();
}]);