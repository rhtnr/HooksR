

"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/hookhub").build();



/*
$.connection.hub.disconnected(function() {
    setTimeout(function() {
        console.log("Reconnecting to hub...");
       $.connection.hub.start();
   }, 3000); // Restart connection after 3 seconds.
});

*/



connection.on("uireceive", function (uipushrequest) {
  console.log(uipushrequest);
  change(uipushrequest);
});

connection.start().then(function () {
}).catch(function (err) {
  return console.error(err.toString());
});

function change(uipushrequest) {
  var scope = angular.element($("#controller")).scope();
  scope.$apply(function () {
    scope.Message = 'Superhero';
    scope.requests.unshift(uipushrequest);
  });
}
