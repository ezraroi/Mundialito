'use strict';
angular.module('mundialitoApp').controller('StadiumsCtrl', ['$scope', '$log', 'StadiumsManager', 'stadiums', 'Alert', function ($scope, $log, StadiumsManager, stadiums, Alert) {
    $scope.stadiums = stadiums;
    $scope.showNewStadium = false;
    $scope.newStadium = null;

    $scope.addNewStadium = function () {
        $scope.newStadium = StadiumsManager.getEmptyStadiumObject();
    };

    $scope.saveNewStadium = function() {
        StadiumsManager.addStadium($scope.newStadium).then(function(data) {
            Alert.success('Stadium was added successfully');
            $scope.newStadium = null;
            $scope.stadiums.push(data);
        });
    };

    $scope.deleteStadium = function(stadium) {
        var scope = stadium;
        stadium.delete().success(function() {
            Alert.success('Stadium was deleted successfully');
            $scope.stadiums.splice($scope.stadiums.indexOf(scope),1);
        })
    };

    $scope.schema =  StadiumsManager.getStaidumSchema();
}]);