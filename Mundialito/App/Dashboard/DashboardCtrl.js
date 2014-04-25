angular.module('mundialitoApp')
.controller('DashboardCtrl', ['$scope', '$rootScope', '$log', 'security', 'Alert', function ($scope, $rootScope, $log, Security, Alert) {
    Security.authenticate();

}]);