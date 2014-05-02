angular.module('mundialitoApp').factory('MundialitoUtils', [ 'Constants', function (Constants) {

    var Utils = {
        shouldRefreshInstance : function(instance) {
            if (!angular.isDefined(instance.LoadTime) || !angular.isDate(instance.LoadTime)) {
                return false;
            }
            var now = new Date().getTime();
            return ((now - instance.LoadTime.getTime()) > Constants.REFRESH_TIME);
        }
    };

    return Utils;
}]);