﻿    $.connection.hub.url = "http://94.209.146.117:99/signalr";
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
    }

    hub.client.newFileUpload = function (message) {
        alert(message);
    };


    hub.client.fileProgessUpdate = function (filename, progress) {
        console.log(filename + " is at " + progress + "%");
        
    };

    // Start the connection.
    $.connection.hub.start().done(function () {
        //SEND
        //hub.server.send();
         test = function () {
             hub.server.uploadFile("BUBUCHENKO-S-1-5-21-3710005488-3752961230-307625830", "abc");
        }
    });
