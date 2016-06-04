'use strict';
angular.module('mundialitoApp').factory('Alert', ['toaster', '$log', '$rootScope', function (toaster, $log, $rootScope) {
    var service = {
        success: success,
        error: error,
        note: note
    };

    return service;


     function success(message) {
        toaster.pop('success', 'Success', message);
    };

     function error(message, title) {
        toaster.pop('error', title || 'Error', message);
    }

    function note(message) {
        toaster.pop('note', 'Info', message);
    }
}]);