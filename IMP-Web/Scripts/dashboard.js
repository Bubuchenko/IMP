var applyFilter = function () {
    var status = $("#onlinestatus-filter").val();
    var antivirusName = $("#antivirus-filter").val();
    var systemtype = $("#systemtype-filter").val();

    if (status == null)
        status = "";
    if (antivirusName == null)
        antivirusName = "";
    if (systemtype == null)
        systemtype = "";


$.get('/IMP-Api/Client/FindBy1',
    {
        antivirusstatus: antivirusName,
        status: status,
        systemtype: systemtype
    },
    function (data) {
        map.removeAllMarkers();
        console.log(JSON.parse(data));
        addClientsToMap(JSON.parse(data));
    });
};