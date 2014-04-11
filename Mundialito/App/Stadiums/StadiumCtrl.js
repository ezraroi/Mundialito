angular.module('mundialitoApp')
    .controller('StadiumCtrl', ['$scope', '$log', 'StadiumsManager', 'security', 'stadium', 'Alert', function ($scope, $log, StadiumsManager, Security, stadium, Alert) {
        Security.authenticate();
        $scope.stadium = stadium;
        $scope.showEditForm = false;

        $scope.updateStadium = function() {
            $scope.stadium.update().success(function() {
                Alert.new('success', 'Stadium was updated successfully', 2000);
            })
        };

        $scope.schema =  StadiumsManager.getStaidumSchema();
    }]);