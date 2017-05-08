$(function () {
    $.connection.hub.url = "http://94.209.146.117:99/signalr";
    $.connection.hub.logging = true;

    var hub = $.connection.dashboardHub;

    //RECEIVE
    hub.client.clientConnected = function (client) {
        viewModel.usersOnline((viewModel.usersOnline() + 1));
        setMapClientStatus(client);
    };

    hub.client.clientDisconnected = function (client) {
        viewModel.usersOnline((viewModel.usersOnline() - 1));
        setMapClientStatus(client);
    };

    // Start the connection.
    $.connection.hub.start().done(function () {

        //SEND
        //hub.server.send();

    });
});
