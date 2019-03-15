'use strict';
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


//Tuve que poner este codigo en el HEADER para que funcionanra los mapas bien.
var map;

function loadMap() {
    if (!map) {
        var mapScriptUrl = 'https://www.bing.com/api/maps/mapcontrol?callback=GetMap2&key=Ar6GSgDklc17CZg1iXfmAutlA2Kru2EpLP0NFvJmllNtv3QX2VTgP3YBSY2AVVUu';
        var script = document.createElement("script");
        script.setAttribute('defer', '');
        script.setAttribute('async', '');
        script.setAttribute("type", "text/javascript");
        script.setAttribute("src", mapScriptUrl);
        document.body.appendChild(script);
    }
    console.log("Loaded MAP API");
}

function GetMap() {
    map = new Microsoft.Maps.Map('#myMap', {});
}

function GetMap2() {
    var loc = eventLocation;
    map = new Microsoft.Maps.Map(document.getElementById('myMap'), {
    //center: new Microsoft.Maps.Location(47.624527, -122.355255),
    //mapTypeId: Microsoft.Maps.MapTypeId.aerial,
    zoom: 50
    });

    Microsoft.Maps.loadModule('Microsoft.Maps.Search', function () {
        var searchManager = new Microsoft.Maps.Search.SearchManager(map);
        var requestOptions = {
            bounds: map.getBounds(),
            where: loc,
            callback: function (answer, userData) {
                map.setView({ bounds: answer.results[0].bestView });
                map.entities.push(new Microsoft.Maps.Pushpin(answer.results[0].location, { color: 'teal' }));
            }
        };
        searchManager.geocode(requestOptions);
    });


}