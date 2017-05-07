var applyFilter = function () {

    $.get('/IMP-Api/Client/FindByAntivirusStatusSystemTypeCreationDateOS',
        {
            antivirusstatus: viewModel.antivirusStatus(),
            antivirusname: viewModel.antivirusName(),
            status: viewModel.onlineStatus(),
            systemtype: viewModel.systemType(),
            creationdate: viewModel.creationDate(),
            operatingsystem: viewModel.operatingSystem()
        },
        function (data) {
            map.removeAllMarkers();
            console.log(JSON.parse(data));
            addClientsToMap(JSON.parse(data));
        });
};


var viewModel = {
    totalUsers: ko.observable(),
    usersOnline: ko.observable(),

    antivirusName: ko.observable(),
    antivirusStatus: ko.observable(),
    onlineStatus: ko.observable(),
    computerType: ko.observable(),
    operatingSystem: ko.observable(),
    creationDate: ko.observable(),
    systemType: ko.observable()
};