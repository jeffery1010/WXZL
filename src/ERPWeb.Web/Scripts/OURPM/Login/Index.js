$(function () {
    clickLogin();

    //$('#LoginUserNo').textbox('setValue', '5105980000');
    //$('#LoginPassword').textbox('setValue', '123456');
    //$('#LoginBtnLogin').click();
});

function clickLogin() {
    $('#LoginBtnLogin').click(function () {
        var UserNo = $('#LoginUserNo');
        var passWord = $('#LoginPassword');
        //校验
        if (!checkFunction(UserNo.textbox('getValue'), passWord.textbox('getValue'))) { return; } 

        var data = { UserNo: UserNo.textbox('getValue'), Password: passWord.textbox('getValue') };
        var result = ajaxSame('/Login/Login', data, 'post', 'text');
        
        if (result == 'Success') {
            window.location.href = "/Index/Index";
        } else if (result == 'NotFindUser') {
            show('该用户不存在！');
            UserNo.textbox('setValue', '')
            passWord.textbox('setValue', '')
        } else if (result == 'PasswordError') {
            passWord.textbox('setValue', '')
            show('密码错误！');
        }
    });
}

function checkFunction(UserNo, password) {
    //校验
    if (checkValLenString(UserNo, 4, 16).code == 0) {
        show('账号在4-16个字符');
        return false;
    }
    if (checkValSpace(UserNo).code == 0) {
        show('账号不能包含空格');
        return false;
    }
    if (checkValLenString(password, 6, 16).code == 0) {
        show('密码在6-16个字符');
        return false;
    }
    return true;
}
