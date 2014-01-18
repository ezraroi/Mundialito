angular.module('mundialitoApp')
.controller('GamesCtrl', ['$scope', '$rootScope', '$filter', '$log', 'GamesService', 'TeamsService', 'StadiumsService', 'security', 'Alert', function ($scope, $rootScope, $filter, $log, GamesService, TeamsService, StadiumsService, Security, Alert) {
    Security.authenticate();
    
    var getGames = function () {
        GamesService.getGames().success(function (data, status, headers, config) {
            $scope.games = data;
            $scope.pendingUpdateGames = $filter('pendingUpdateGamesFilter')(data);
            $log.debug("GamesCtrl: GamesService.getGames (" + status + "): " + angular.toJson(data));
        });
    };

    var init = function () {
        getGames();

        TeamsService.getTeams().success(function (data, status, headers, config) {
            $log.debug("GamesCtrl: TeamsService.getTeams Success (" + status + "): " + angular.toJson(data));
            $scope.teams = data;
        });

        StadiumsService.getStadiums().success(function (data, status, headers, config) {
            $log.debug("GamesCtrl: StadiumsService.getStadiums Success (" + status + "): " + angular.toJson(data));
            $scope.stadiums = data;
        });
    };

    init();

    $scope.pendingUpdateGames = [];

    $scope.newGame = null;

    $scope.gamesFilter = "All";

    $scope.schema = GamesService.schema;

    $scope.addNewGame = function () {
        $log.debug('GamesCtrl: addNewGame cliked');
        $log.debug('GamesCtrl: refreshing selectpickers');
        $('.selectpicker').selectpicker('refresh');
        $scope.newGame = GamesService.getEmptyGameObject();
    };

    var refreshGamesBind = $rootScope.$on('refreshGames', function () {
        $log.debug("GamesCtrl: got 'refreshGames' event");
        getGames();
        $scope.newGame = null;
    });

    $scope.$on('$destroy', refreshGamesBind);
   
    
}]);