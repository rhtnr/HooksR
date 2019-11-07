app.controller('homeController', function ($scope) {
  $scope.Message = "Requests";
  $scope.userhash = null;
  $scope.requests = [];

  console.log("Starting connection");

  $scope.init = function (userhash) {
    console.log(userhash);
    $scope.userhash = userhash;
  };



  $scope.getKeys = function (obj) {
    return Object.keys(obj);
  };

  $scope.getValues = function (obj) {
    return Object.values(obj);
  };

  $scope.table_expand = function (table) {
    console.log(table);
    this.renderer.addClass(table, "table-requestsexpanded");
  };


});