angular.module('mundialitoApp')
    .controller('StadiumsCtrl', ['$scope', '$log', 'StadiumsManager', 'security', 'stadiums', 'Alert', function ($scope, $log, StadiumsManager, Security, stadiums, Alert) {
        Security.authenticate();
        $scope.stadiums = stadiums;
        $scope.showNewStadium = false;
        $scope.newStadium = null;

        $scope.addNewStadium = function () {
            $scope.newStadium = StadiumsManager.getEmptyStadiumObject();
        };

        $scope.saveNewStadium = function() {
            StadiumsManager.addStadium($scope.newStadium).then(function(data) {
                Alert.new('success', 'Stadium was added successfully', 2000);
                $scope.newStadium = null;
                $scope.stadiums.push(data);
            });
        };

        $scope.deleteStadium = function(stadium) {
            var scope = stadium;
            stadium.delete().success(function() {
                Alert.new('success', 'Stadium was deleted successfully', 2000);
                $scope.stadiums.splice($scope.stadiums.indexOf(scope),1);
            })
        };

        $scope.schema =  StadiumsManager.getStaidumSchema();
    }]);