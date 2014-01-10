'use strict';

angular.module('mundialitoApp')
.directive('accessLevel', ['security', function (Security) {
    return {
        restrict: 'A',
        link: function ($scope, element, attrs) {
            var prevDisp = element.css('display')
                , userRole = ""
                , accessLevel;

            $scope.user = Security.user;
            $scope.$watch('user', function (user) {
                if ((user === undefined) || (user === null)) {
                    userRole = "Public"
                } else if (user.roles) {
                    userRole = user.roles;
                } else {
                    userRole = "Public"
                }
                updateCSS();
            }, true);

            attrs.$observe('accessLevel', function (al) {
                if (al) accessLevel = al;
                updateCSS();
            });

            function updateCSS() {
                if (userRole && accessLevel) {
                    if (userRole === accessLevel)
                        element.css('display', prevDisp);
                    else
                        element.css('display', 'none');
                }
            }
        }
    };
}]);

angular.module('mundialitoApp').directive('activeNav', ['$location', function ($location) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            var nestedA = element.find('a')[0];
            var path = nestedA.href;

            scope.location = $location;
            scope.$watch('location.absUrl()', function (newPath) {
                if (path === newPath) {
                    element.addClass('active');
                } else {
                    element.removeClass('active');
                }
            });
        }

    };

}]);