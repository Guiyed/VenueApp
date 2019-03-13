'use strict'; // This is not neccesary but it makes the Javascript more reliable

// --- Everything here runs Before the document loads ---
$(document).ready(function () {
    console.log("ready!");
    var dt = endDate;
    console.log("Viewbag " + dt);
    var end = new Date(dt);
    console.log("End " + end);
    var _second = 1000;
    var _minute = _second * 60;
    var _hour = _minute * 60;
    var _day = _hour * 24;
    var timer;

function showRemaining() {
    var now = new Date();
    console.log("Now " + now);
    var distance = end - now;
    console.log(distance)
    if (distance <= 0) {
        document.getElementById('timer').setAttribute("class", "text-center text-danger");
        document.getElementById('countdown').innerHTML = "!!! Redirecting !!!";
        clearInterval(timer);
        window.location.href = '/User/Login'; //Redirect to Login page

        return;
    }

    var days = Math.floor(distance / _day);
    var hours = Math.floor((distance % _day) / _hour);
    var minutes = Math.floor((distance % _hour) / _minute);
    var seconds = Math.floor((distance % _minute) / _second);

    /*
     * document.getElementById('countdown').innerHTML = hours + 'hrs ';
    document.getElementById('countdown').innerHTML += minutes + 'mins ';
    document.getElementById('countdown').innerHTML += seconds + 'secs';
    */

    document.getElementById('countdown').innerHTML = seconds + 'secs';
}
    timer = setInterval(showRemaining, 999);   // 0.99 second interval

});
