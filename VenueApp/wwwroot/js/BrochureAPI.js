'use strict';
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

$(document).ready(function () {
    console.log("ready!");
    loadMapScenario(eventLocation)

function loadMapScenario(loc) {
    var map = new Microsoft.Maps.Map(document.getElementById('myMap'), {
        center: new Microsoft.Maps.Location(47.624527, -122.355255),
        //mapTypeId: Microsoft.Maps.MapTypeId.aerial,
        zoom: 10
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

function notAvailable() {
    alert("Please Log in first to access all features");
}

});
