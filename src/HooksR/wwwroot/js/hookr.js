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

connection.on("uireceive", function (message) {
  console.log(message);
  var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
  var encodedMsg = user + " says " + msg;
  var li = document.createElement("li");
  li.textContent = encodedMsg;
  document.getElementById("messagesList").appendChild(li);
});

connection.start().then(function () {
}).catch(function (err) {
  return console.error(err.toString());
});
