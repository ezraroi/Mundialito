'use strict';
angular.module('mundialitoApp').directive('mundialitoToggleText', [function () {
    function link(scope, element, attrs) {
        var state;
        scope.$watch(attrs.varieble, function (value) {
            state = value;
            updateText();
        });

        function updateText() {
            var text = state == true ? attrs.trueLabel : attrs.falseLabel;
            element.text(text);
        }
    }
    return {
        link: link
    };
}]);