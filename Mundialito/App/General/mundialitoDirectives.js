'use strict';
angular.module('mundialitoApp')
.directive('mundialitoToggleText', ['$log', function ($log) {

    function link(scope, element, attrs) {

        var state;

        scope.$watch(attrs.varieble, function (value) {
            $log.debug("watch (attrs.varieble): new value is '" + value + "'");
            state = value;
            updateText();
        });

        function updateText() {
            var text = state == true ? attrs.trueLabel : attrs.falseLabel;
            $log.debug("Setting text to: " + text);
            element.text(text);
        }
    }

    return {
        link: link
    };
}]);