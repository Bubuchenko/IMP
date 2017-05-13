$.connection.hub.url = "http://94.209.146.117:99/signalr";
$.connection.hub.logging = true;

var uploadFile;
var downloadFile;
var openItem;
var deleteItem;
var getDirectoryContent;

var hub = $.connection.fileManageHub;

//RECEIVE
hub.client.clientConnected = function (client) {
    if (client.ClientId == viewModel.Client().ClientId) {
        
    }
};

hub.client.clientDisconnected = function (client) {
    if (client.ClientId == viewModel.Client().ClientId) {
        alert("d/c'd");
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
    uploadFile = function (clientID, source) {
        hub.server.uploadFile(clientID, source);
    }

    downloadFile = function (clientID, destination) {
        hub.server.downloadFile(clientID, destination);
    }

    getDirectoryContent = function (clientID, path) {
        var promise = new Promise(function (resolve, reject) {
            var result = hub.server.getDirectoryContent(clientID, path);
            resolve(result);
        });

        return promise;
    };

    deleteItem = function (clientID, path) {
        var promise = new Promise(function (resolve, reject) {
            var result = hub.server.delete(clientID, path);
            resolve(result);
        });

        return promise;
    };

    openItem = function (clientID, path) {
        var promise = new Promise(function (resolve, reject) {
            var result = hub.server.open(clientID, path);
            resolve(result);
        });

        return promise;
    };


    browseDirectory(viewModel.CurrentDirectory(), "Folder");
});
