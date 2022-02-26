//身份证验证
function checkValICord(value) {
    var myreg = /(^\d{15}$)|(^\d{18}$)|(^\d{17}(\d|X|x)$)/;  
    if (!myreg.test(value)) {
        return { code: 0, msg: 'Space' };
    } else {
        return { code: 1, msg: 'Success' };
    }
}
//验证INPUT val 非空
function checkValNotNull(value) {
    if (value == '' || value == null) {
        return { code:0, msg:'Null' };
    } else {
        return { code:1, msg:'Success' };
    }
}
//数字，长度
function checkValLenNumber(value, minNum, maxNum) {
    if (value.length < minNum || value.length > maxNum) {
        return { code: 0, msg:'Length' };
    } else if (isNaN(value)) {
        return { code: 0, msg:'Number' };
    } else {
        return { code:1, msg:'Success' };
    }
}
//字符串，长度
function checkValLenString(value, minNum, maxNum) {
    if (value.length < minNum || value.length > maxNum) {
        return { code: 0, msg:'Length' };
    } else {
        return { code: 1, msg:'Success' };
    }
}
//字符串，长度
function checkValLenSpaceString(value, minNum, maxNum) {
    if (value.length < minNum || value.length > maxNum) {
        return { code: 0, msg: 'Length' };
    } else {
        var myreg = /^\S*$/;
        if (!myreg.test(value)) {
            return { code: 0, msg: 'Space' };
        } else {
            return { code: 1, msg: 'Success' };
        }
    }
}
//不能输入空格
function checkValSpace(value) {
    var myreg = /^\S*$/;
    if (!myreg.test(value)) {
        return { code: 0, msg: 'Space' };
    } else {
        return { code: 1, msg: 'Success' };
    }
}
//邮箱验证
function checkValEmail(value) {
    var myreg = /^([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+\.[a-zA-Z]{2,3}$/;
    if (!myreg.test(value)) {
        return { code: 0, msg:'Email' };
    }
    else {
        return { code: 1, msg:'Success' };
    }
}
//手机号验证
function checkValPhone(value) {
    var myreg = /^[1][3,4,5,7,8,9][0-9]{9}$/;
    if (!myreg.test(value)) {
        return { code: 0, msg:'Phone' };
    } else {
        return { code:1, msg:'Success' };
    }
}
// 电话号码验证
function checkValTel(value) {
    var myreg = /^\d{2,5}-\d{2,5}-\d{7,8}$/;
    if (!myreg.test(value)) {
        return { code: 0, msg:'Tel' };
    } else {
        return { code:1, msg:'Success' };
    }
}
//MVC 网址验证
function checkValMVCUrl(value) {
    var myreg = /^\/\S+\/\S+$/;
    debugger;
    if (!myreg.test(value)) {
        return { code: 0, msg: 'MVCUrl' };
    } else {
        return { code: 1, msg: 'Success' };
    }
}
