app.controller('homeController', function($scope) {
    $scope.Message = "Requests";
    $scope.requests = [];

    console.log("Starting connection");

    $scope.getKeys = function(obj) {
        return Object.keys(obj);
    };

    $scope.getValues = function(obj) {
        return Object.values(obj);
    };

    $scope.table_expand = function(table) {
        console.log(table);
        this.renderer.addClass(table, "table-requestsexpanded");
    };


});