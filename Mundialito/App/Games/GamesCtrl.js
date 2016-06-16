'use strict';
angular.module('mundialitoApp').controller('GamesCtrl', ['$scope','$log','GamesManager','games','teams', 'StadiumsManager' ,'Alert',function ($scope,$log, GamesManager, games, teams, StadiumsManager, Alert) {
    $scope.newGame = null;
    $scope.gamesFilter = "All";
    $scope.games = games;
    $scope.teams = teams;
    

    StadiumsManager.loadAllStadiums().then(function (res) {
        $scope.stadiums = res;
    });

    $scope.addNewGame = function () {
        $('.selectpicker').selectpicker('refresh');
        $scope.newGame = GamesManager.getEmptyGameObject();
    };

    $scope.saveNewGame = function() {
        GamesManager.addGame($scope.newGame).then(function(data) {
            Alert.success('Game was added successfully');
            $scope.newGame = GamesManager.getEmptyGameObject();
            $scope.games.push(data);
        });
    };

    $scope.isPendingUpdate = function() {
        return function( item ) {
            return item.IsPendingUpdate;
        };
    };

    $scope.updateGame = function(game) {
        if  ((angular.isDefined(game.Stadium.Games)) && (game.Stadium.Games != null)) {
            delete game.Stadium.Games;
        }
        game.update().success(function (data) {
            Alert.success('Game was updated successfully');
            GamesManager.setGame(data);
        });
    };
}]);