$(function () {
    $.connection.hub.url = "http://94.209.146.117:99/signalr";
    $.connection.hub.logging = true;

    var hub = $.connection.clientControlHub;

    //RECEIVE
    hub.client.clientConnected = function (client) {
        
    };

    hub.client.clientDisconnected = function (client) {
        
    };

    // Start the connection.
    $.connection.hub.start().done(function () {

        //SEND
        //hub.server.send();

    });
});
