'use strict'; // This is not neccesary but it makes the Javascript more reliable

// --- Everything here runs Before the document loads ---
$(document).ready(function () {
    console.log("Timer Redirect ready!");
    console.log("Viewbag endDate " + endDate);
    console.log("Viewbag nowDate " + nowDate);
    var end = new Date(endDate);
    //console.log("End Date" + end);
    var now = new Date(nowDate);
    //var now = new Date();
    //console.log("Now Date" + now);
    var _second = 1000;
    var _minute = _second * 60;
    var _hour = _minute * 60;
    var _day = _hour * 24;
    var timer;
    var milisecondsTicks = 1000;


    function showRemaining() {
        //now = new Date();
        var val = now.getTime();
        val += milisecondsTicks;
        now = new Date(val);
        var distance = end - now;
        console.log("Show Remaining " + distance)
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

    timer = setInterval(showRemaining, milisecondsTicks);   // 0.99 second interval
    //timer = setInterval(showRemaining, 999);   // 0.99 second interval
});
