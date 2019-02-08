'use strict'; // This is not neccesary but it makes the Javascript more reliable

// --- Everything here runs Before the document loads ---


    var myVar = setInterval(myTimer, 1000);

    function myTimer() {
        var d = new Date();
        document.getElementById("demo").innerHTML = d.toLocaleTimeString();
    }

