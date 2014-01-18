'use strict';

angular.module('mundialitoApp')
.directive('accessLevel', ['$log','security', function ($log,Security) {
    return {
        restrict: 'A',
        link: function ($scope, element, attrs) {
            var prevDisp = element.css('display')
                , userRole = ""
                , accessLevel;

            
            $scope.$watch(
              function () {
                  return Security.user;
              },

              function (newValue, oldValue) {
                  $scope.user = newValue;
                  if (($scope.user === undefined) || ($scope.user === null)) {
                      userRole = "Public"
                  } else if ($scope.user.roles) {
                      $log.debug('Security.user has been changed:' + $scope.user.userName);
                      userRole = $scope.user.roles;
                  } else {
                      userRole = "Public"
                  }
                  updateCSS();
              },
              true
            );
           
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