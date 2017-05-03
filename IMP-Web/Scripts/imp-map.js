var getMarkerStyle = function (status) {
    if (status === 'Online') {
        return {
            fill: 'yellow',
            stroke: '#505050',
            "fill-opacity": 1,
            "stroke-width": 1,
            "stroke-opacity": 1,
            r: 3
        }
    }

    if (status === 'Offline') {
        return {
            fill: 'red',
            stroke: '#505050',
            "fill-opacity": 1,
            "stroke-width": 1,
            "stroke-opacity": 1,
            r: 3
        }
    }
}


var addClientToMap = function (map, client) {
    var markerStyle = getMarkerStyle(client.Status);
    map.addMarker(client.ID, { latLng: client.Location, name: client.Name, style: markerStyle });

    if (client.Status === 'Online')
        $('circle[data-index="' + client.ID + '"]').addClass("pulsate");
}

var addClientsToMap = function (map, clients) {
    for (var i = 0; i < clients.length; i++)
        addClientToMap(map, clients[i]);
}


var createMap = function (container, map) {
    return map = new jvm.Map({
        container: container,
        map: map,
        regionSelectable: false,
        zoomAnimate: true,
        regionStyle: {
            initial: {
                fill: '#234f61',
            },
        },
        series: {
            regions: {
                legend: {
                    title: 'War map'
                },
            },
        },
        markerStyle: {
            hover: {
                stroke: 'black',
                "stroke-width": 2,
                r: 10
            },
        },
    });
}