'use strict';

angular.module("mundialitoApp")
.filter('gamesFilter', function () {

    // All filters must return a function. The first parameter
    // is the data that is to be filtered, and the second is an
    // argument that may be passed with a colon (searchFor:searchString)

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

});