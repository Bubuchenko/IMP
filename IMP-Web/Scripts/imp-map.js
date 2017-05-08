var getMarkerStyle = function (Online) {
    if (Online === true) {
        return {
            fill: 'yellow',
            stroke: '#505050',
            "fill-opacity": 1,
            "stroke-width": 1,
            "stroke-opacity": 1,
            r: 3
        };
    }

    if (Online === false) {
        return {
            fill: 'red',
            stroke: '#505050',
            "fill-opacity": 1,
            "stroke-width": 1,
            "stroke-opacity": 1,
            r: 3
        };
    }
};

var connectPulseStyle = {
    fill: 'none',
    stroke: '#f2be35',
    "fill-opacity": 1,
    "stroke-width": 1,
    r: 10
}


var addClientToMap = function (client) {
    var markerStyle = getMarkerStyle(client.IsOnline);

    var Location = client.PersonalInformation.Location.split(',');

    map.addMarker(client.ClientId, { latLng: Location, name: client.Username, style: markerStyle });

    if (client.IsOnline === true) {
        $('circle[data-index="' + client.ClientId + '"]').addClass("pulsate");

        //Display a temporary marker
        map.addMarker(client.ClientId + "-OnlineMarker", { latLng: Location, name: client.Username, style: connectPulseStyle });
        $('circle[data-index="' + client.ClientId + '-OnlineMarker"]').addClass("pulse");

        //Remove temporary marker after a while
        setTimeout(function () {
            try {
                map.removeMarkers([client.ClientId + '-OnlineMarker']);
            } catch(ex) { }
        }, 10000)
    }
};

var addClientsToMap = function (clients) {
    for (var i = 0; i < clients.length; i++)
        addClientToMap(clients[i]);
};

var setMapClientStatus = function (client) {
    map.removeMarkers([client.ClientId]);
    addClientToMap(client);
};

var createMap = function (container, map) {
    return map = new jvm.Map({
        container: container,
        map: map,
        regionSelectable: false,
        zoomAnimate: false,
        regionStyle: {
            initial: {
                fill: '#234f61'
            }
        },
    });
};