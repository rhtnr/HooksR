var app = angular.module('homeapp', []);


app.filter('keysLength', [function() {
    return function(items) {
        return Object.keys(items).length;
    };
}]);