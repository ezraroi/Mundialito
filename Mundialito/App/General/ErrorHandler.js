'use strict';
angular.module('mundialitoApp').factory('ErrorHandler', ['$rootScope', '$log' , 'Alert', '$location','Constants', function ($rootScope, $log, Alert, $location, Constants) {
    var ErrorHandler = this;

    ErrorHandler.handle = function (data, status) {
        $log.log(data);
        if (status === 401) {
            $location.path(Constants.LOGIN_PATH);
            return;
        }
        var message = [];
        if (data.Message) {
            message.push("<strong>" + data.Message + "</strong>");
        }
        if (data.ModelState) {
            angular.forEach(data.ModelState, function (errors) {
                message.push(errors);
            });
        }
        if (data.ExceptionMessage) {
            message.push(data.ExceptionMessage);
        }
        if (data.error_description) {
            message.push(data.error_description);
        }
        if (message === '') {
            message.push("<strong>General Error</strong>")
            message.push("Looks like the server is down, please try again in few minutes")
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