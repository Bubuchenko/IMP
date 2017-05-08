    $.connection.hub.url = "http://94.209.146.117:99/signalr";
    $.connection.hub.logging = true;

    var hub = $.connection.cmdHub;
    var sendCommand;

    //RECEIVE
    hub.client.clientConnected = function (client) {
        if (client.ClientId == viewModel.Client().ClientId) {
            viewModel.Client(client);
        }
    };

    hub.client.clientDisconnected = function (client) {
        if (client.ClientId == viewModel.Client().ClientId) {
            viewModel.Client(client);
        }
    };

    // Start the connection.
    $.connection.hub.start().done(function () {
        //SEND
        sendCommand = function (command, term) {
            hub.server.cmdCommand(viewModel.Client().ClientId, command).done(function (result) {
                term.resume();
                term.echo(result, true);
            })
        }
    });