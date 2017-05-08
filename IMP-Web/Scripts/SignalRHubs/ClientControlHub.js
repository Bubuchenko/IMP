    $.connection.hub.url = "http://94.209.146.117:99/signalr";
    $.connection.hub.logging = true;

    var test;

    var hub = $.connection.clientControlHub;

    //RECEIVE
    hub.client.clientConnected = function (client) {
        if (client.ClientId == viewModel.Client().ClientId) {
            setMapClientStatus(client);
        }
    };

    hub.client.clientDisconnected = function (client) {
        if (client.ClientId == viewModel.Client().ClientId) {
            setMapClientStatus(client);
        }
    };

    // Start the connection.
    $.connection.hub.start().done(function () {
        //SEND
        //hub.server.send();
         test = function () {
             hub.server.test("BUBUCHENKO-S-1-5-21-3710005488-3752961230-307625830").done(function (result) {
                 
             })
        }
    });
