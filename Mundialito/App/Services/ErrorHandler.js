'use strict';
angular.module('mundialitoApp')
.factory('ErrorHandler', ['$rootScope', '$log' , 'Alert', function ($rootScope, $log, Alert) {
    var ErrorHandler = this;

    ErrorHandler.handle = function (data, status, headers, config) {
        $log.log(data);
        var message = [];
        if (data.Message) {
            message.push("<strong>" + data.Message + "</strong>");
        }
        if (data.ModelState) {
            angular.forEach(data.ModelState, function (errors, key) {
                message.push(errors);
            });
        }
        if (data.ExceptionMessage) {
            message.push(data.ExceptionMessage);
        }
        if (data.error_description) {
            message.push(data.error_description);
        }
        Alert.new('danger', message.join('<br/>'));
    }

    return ErrorHandler;
}])
.factory('myHttpInterceptor', ['ErrorHandler', '$q', function (ErrorHandler, $q) {
    return {
        response: function (response) {
            return response;
        },
        responseError: function (response) {
            ErrorHandler.handle(response.data, response.status, response.headers, response.config);

            // do something on error
            return $q.reject(response);
        }
    };
}]);