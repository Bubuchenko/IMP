$.connection.hub.url = "http://94.209.146.117:99/signalr";
$.connection.hub.logging = true;

var uploadFile;
var downloadFile;
var openItem;
var renameItem;
var moveItem;
var deleteItem;
var getDirectoryContent;
var createFolder;
var createFile;

var hub = $.connection.fileManageHub;

//RECEIVE
hub.client.clientConnected = function (client) {
    if (client.ClientId == viewModel.Client().ClientId) {
        enablePage();
    }
};

hub.client.clientDisconnected = function (client) {
    if (client.ClientId == viewModel.Client().ClientId) {
        disablePage();
    }
}

hub.client.newFileUpload = function (fileStatus) {
    
};


hub.client.updateFileProgress = function (fileStatus) {
    var fileTransfer = ko.utils.arrayFirst(viewModel.FileTransfers(), function (fileTransfer) {
        return fileTransfer.FileTransferID() == fileStatus.FileTransferID;
    });


    if (fileTransfer != null)
        fileTransfer.FriendlyProgress(fileStatus.FriendlyProgress);
    else
        addFileTransfer(fileStatus);
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

    renameItem = function (clientID, path, newName) {
        var promise = new Promise(function (resolve, reject) {
            var result = hub.server.rename(clientID, path, newName);
            resolve(result);
        });

        return promise;
    };


    moveItem = function (clientID, path, newPath) {
        var promise = new Promise(function (resolve, reject) {
            var result = hub.server.move(clientID, path, newPath);
            resolve(result);
        });

        return promise;
    };

    createFolder = function (clientID, path, name) {
        var promise = new Promise(function (resolve, reject) {
            var result = hub.server.createFolder(clientID, path, name);
            resolve(result);
        });

        return promise;
    };

    createFile = function (clientID, path, name) {
        var promise = new Promise(function (resolve, reject) {
            var result = hub.server.createFile(clientID, path, name);
            resolve(result);
        });

        return promise;
    };


    //Load the directly once we're connected
    browseDirectory(viewModel.CurrentDirectory(), "Folder");
});
