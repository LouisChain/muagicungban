function countDown(runTime, divid) {
    var tdiv = document.getElementById(divid)
        , totalSecond = runTime
        , to;

    this.TotalSecond = function (val) {
        totalSecond = val;
    };

    this.rewriteCounter = function () {
        totalSecond -= 1;
        var second = totalSecond;
        var days = Math.floor(second / 86400);
        second -= days * 86400;
        var hours = Math.floor(second / 3600);
        second -= hours * 3600;
        var minutes = Math.floor(second / 60);
        second -= minutes * 60;
        second = Math.ceil(second);
        if (totalSecond >= 0) {
            tdiv.innerHTML = days + "ngày " + leadingZero(hours) + ":" + leadingZero(minutes) + ":" + leadingZero(second);
        }
        else { clearInterval(to); }
    };
    this.rewriteCounter();
    to = setInterval(this.rewriteCounter, 1000);
}

function leadingZero(Time) {
    if (Time < 10) {
        return "0" + Time;
    }
    else {
        return "" + Time;
    }
}

function countDown1(runTime, divid) {
    var tdiv = document.getElementById(divid)
        , totalSecond = runTime
        , to;

    this.TotalSecond = function (val) {
        totalSecond = val;
    };

    this.rewriteCounter = function () {
        totalSecond -= 1;
        var second = totalSecond;
        var days = Math.floor(second / 86400);
        second -= days * 86400;
        var hours = Math.floor(second / 3600);
        second -= hours * 3600;
        var minutes = Math.floor(second / 60);
        second -= minutes * 60;
        second = Math.ceil(second);
        if (totalSecond >= 0) {
            tdiv.innerHTML = "<span>" + days + "</span> ngày <br><br><span>" + leadingZero(hours) + "</span>:<span>" + leadingZero(minutes) + "</span>:<span>" + leadingZero(second) + "</span>";
        }
        else { clearInterval(to); }
    };
    this.rewriteCounter();
    to = setInterval(this.rewriteCounter, 1000);
}
