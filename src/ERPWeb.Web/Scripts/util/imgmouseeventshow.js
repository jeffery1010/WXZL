
function imgMouseOver(this_img, outerdivid) { 
    var src = this_img.src;
    $('#' + outerdivid).find('img:first').attr('src', src);
    $('#' + outerdivid).css({ "display": "block" });
}
function imgMouseOut(this_img,outerdivid) {
    $('#' + outerdivid).css({ "display": "none" });
}
function imgMouseMove(this_img,outerdivid) {
    
    var bWidth = document.body.scrollWidth * 0.6;
    var arr = getWHPX(outerdivid);
    var x = 0;
    var width = 0;
    var height = 0;
    if (arr[0] > bWidth) {
        x = bWidth / arr[0];
        width = arr[0] * x;
        height = arr[1] * x;
    } else {
        x = arr[0] / bWidth;
        width = arr[0] / x;
        height = arr[1] / x;
    }
    $('#' + outerdivid).find('img:first').css({ "width": width + "px", "height": height + "px" });
}
