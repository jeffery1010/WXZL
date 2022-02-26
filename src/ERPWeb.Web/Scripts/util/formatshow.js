
//修改日历框的显示格式
function dateformatter(date) {
    var year = date.getFullYear();
    var month = date.getMonth() + 1;
    var day = date.getDate();
    var hour = date.getHours();
    var min = date.getMinutes();
    var second = date.getSeconds();
    month = month < 10 ? '0' + month : month;
    day = day < 10 ? '0' + day : day;
    hour = hour < 10 ? '0' + hour : hour;
    min = min < 10 ? '0' + min : min;
    second = second < 10 ? '0' + second : second;
    var time = year + "-" + month + "-" + day + " " + hour + ":" + min + ":" + second;

    return time;
}
var time;
function dateparser(s) {
    var t;
    if (time && time != "") {
        t = time;
    }
    if (s.indexOf("(") > -1) {
        t = Date.parse(t);
    } else {
        t = Date.parse(s);
    }
    if (!isNaN(t)) {
        return new Date(t);
    } else {
        return new Date();
    }
}
