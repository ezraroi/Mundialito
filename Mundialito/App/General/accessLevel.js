'use strict';
angular.module('mundialitoApp').directive('accessLevel', ['$log','security', function ($log,Security) {
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

              function (newValue) {
                  $scope.user = newValue;
                  if (($scope.user === undefined) || ($scope.user === null)) {
                      userRole = "Public"
                  } else if ($scope.user.roles) {
                      //$log.debug('Security.user has been changed:' + $scope.user.userName);
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