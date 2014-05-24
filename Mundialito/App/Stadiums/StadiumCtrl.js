'use strict';
angular.module('mundialitoApp').controller('StadiumCtrl', ['$scope', '$log', 'StadiumsManager', 'stadium', 'Alert', function ($scope, $log, StadiumsManager, stadium, Alert) {
    $scope.stadium = stadium;
    $scope.showEditForm = false;

    $scope.updateStadium = function() {
        $scope.stadium.update().success(function() {
            Alert.new('success', 'Stadium was updated successfully', 2000);
        })
    };

    $scope.schema =  StadiumsManager.getStaidumSchema();
}]);