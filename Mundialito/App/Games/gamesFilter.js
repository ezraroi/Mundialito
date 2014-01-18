'use strict';

angular.module("mundialitoApp")
.filter('gamesFilter', function () {
    return function (arr, searchString) {

        if ((!searchString) || (searchString === "All")) {
            return arr;
        }

        var result = [];
        angular.forEach(arr, function (item) {

            if (item.IsOpen) {
                result.push(item);
            }

        });

        return result;
    };

})
.filter('pendingUpdateGamesFilter', function () {
    return function (arr) {
        var result = [];
        angular.forEach(arr, function (item) {

            if (item.IsPendingUpdate) {
                result.push(item);
            }
        });

        return result;
    };

});