'use strict';
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


$(document).ready(function () {
   
    var page = 0;

    function getEvents(page) {
        $("#events-panel").show();
        $("#attraction-panel").hide();

        if (page < 0) {
            page = 0;
            return;
        }
        if (page > 0) {
            if (page > getEvents.json.page.totalPages - 1) {
                page = 0;
            }
        }

        $.ajax({
            type: "GET",
            url: "https://app.ticketmaster.com/discovery/v2/events.json?countryCode=US&city=Miami&apikey=rjvAOXYLhx1XPB30QYsgr5QVhQVO3U4b&size=5&page=" + page,
            async: true,
            dataType: "json",
            success: function (json) {
                getEvents.json = json;
                showEvents(json);
            },
            error: function (xhr, status, err) {
                console.log(err);
            }
        });
    }

    function showEvents(json) {
        var items = $("#events .list-group-item");
        items.hide();
        var events = json._embedded.events;
        var item = items.first();
        for (var i = 0; i < events.length; i++) {
            item.children('.list-group-item-heading').text(events[i].name);
            item.children('.list-group-item-text').text(events[i].dates.start.localDate);
            try {
                item.children(".venue").text(events[i]._embedded.venues[0].name + " in " + events[i]._embedded.venues[0].city.name);
            } catch (err) {
                console.log(err);
            }
            item.show();
            item.off("click");
            item.click(events[i], function (eventObject) {
                console.log(eventObject.data);
                try {
                    getAttraction(eventObject.data._embedded.attractions[0].id);
                } catch (err) {
                    console.log(err);
                }

                try {
                    getVenue(eventObject.data._embedded.venues[0].id);
                } catch (err) {
                    console.log(err);
                }

            });
            item = item.next();
        }
    }

    $("#prev").click(function () {
        getEvents(--page);
    });

    $("#next").click(function () {
        getEvents(++page);
    });

    function getAttraction(id) {
        $.ajax({
            type: "GET",
            url: "https://app.ticketmaster.com/discovery/v2/attractions/" + id + ".json?apikey=rjvAOXYLhx1XPB30QYsgr5QVhQVO3U4b",
            async: true,
            dataType: "json",
            success: function (json) {
                showAttraction(json);
            },
            error: function (xhr, status, err) {
                console.log(err);
            }
        });
    }

    function getVenue(id) {
        $.ajax({
            type: "GET",
            url: "https://app.ticketmaster.com/discovery/v2/venues/" + id + ".json?apikey=rjvAOXYLhx1XPB30QYsgr5QVhQVO3U4b",
            async: true,
            dataType: "json",
            success: function (json) {
                loadMapScenario(json, json.location.latitude, json.location.longitude);
            },
            error: function (xhr, status, err) {
                console.log(err);
            }
        });
    }



    function showAttraction(json) {
        $("#events-panel").hide();
        $("#attraction-panel").show();

        $("#myButton").click((e) => {
            //$("#attraction-panel").click(function () {
            getEvents(page);
        });

        $("#attraction .list-group-item-heading").first().text(json.name);
        $("#attraction img").first().attr('src', json.images[0].url);
        $("#classification").text(json.classifications[0].segment.name + " - " + json.classifications[0].genre.name + " - " + json.classifications[0].subGenre.name);
        //let lat = 26.133435;
        //let long = -80.3150427;
        //loadMapScenario(lat, long);        
    }



    function loadMapScenario(json, lat, long) {
        var map = new Microsoft.Maps.Map(document.getElementById('myMap'), {
            center: new Microsoft.Maps.Location(lat, long),
            //mapTypeId: Microsoft.Maps.MapTypeId.aerial,
            zoom: 15
        });

        var pushpin = new Microsoft.Maps.Pushpin(map.getCenter(), { color: 'teal' });
        map.entities.push(pushpin);

        $("#venueName").text(json.name + " - " + json.city.name + ", " + json.state.stateCode);
    }

    getEvents(page);

    //src = 'https://www.bing.com/api/maps/mapcontrol?key=Ar6GSgDklc17CZg1iXfmAutlA2Kru2EpLP0NFvJmllNtv3QX2VTgP3YBSY2AVVUu'

});