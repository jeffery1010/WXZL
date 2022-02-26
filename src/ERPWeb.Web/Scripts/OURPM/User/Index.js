$(function () {
    initUserDialog();
    initUserTable();
    initUserClick();
})
//验证输入-点击提交时
function checkUserClickSubmit(idList) {
    $(idList).each(function (index, element) {
        checkUserAttention($(this));
    });
}
//验证输入-更改提示信息
function checkUserAttention(element) {
    var id = $(element).attr('id');
    var value = $(element).textbox('getValue');
    var errorMsg = $(element).parents('tr').find('.errorMsg');
    if (id == 'dgUserName') {
        if (checkValLenString(value, 1, 64).code == 0) errorMsg.text('用户名在1-64个字符之间'); else errorMsg.text('');
    } else if (id == 'dgUserNo') {
        if (checkValLenString(value, 1, 64).code == 0) errorMsg.text('用户编号在1-64个字节之间'); else errorMsg.text('');
    } else if (id == 'dgTel') {
        if (checkValTel(value).code == 0) errorMsg.text('座机不符合规范'); else errorMsg.text('');
    } else if (id == 'dgEmail') {
        if (checkValEmail(value).code == 0) errorMsg.text('邮箱不符合规范'); else errorMsg.text('');
    } else if (id == 'dgAddress') {
        if (checkValLenString(value, 1, 1024).code == 0) errorMsg.text('地址在1-1024个字节之间'); else errorMsg.text('');
    } else if (id == 'dgIdCard') {
        if (checkValICord(value).code == 0) errorMsg.text('身份证验证不符合规范'); else errorMsg.text('');
    } else if (id == 'dgMobilePhone') {
        if (checkValPhone(value).code == 0) errorMsg.text('手机验证不符合规范'); else errorMsg.text('');
    } else if (id == 'dgPassword') {
        if (checkValLenString(value, 6, 16).code == 0) errorMsg.text('密码满足6-16个字节之间'); else errorMsg.text('');
    }
}


//初始化表格
function initUserTable() {
    var name = $('#searchUserName').textbox('getValue');
    var code = $('#searchUserNo').textbox('getValue');
    var tel = $('#searchTel').textbox('getValue');
    var email = $('#searchEmail').textbox('getValue');
    var address = $('#searchAddress').textbox('getValue');
    var idCard = $('#searchIdCard').textbox('getValue');
    var mobilePhone = $('#searchMobilePhone').textbox('getValue');
    var isVisible = $('#searchUserVisible').combobox('getValue');
    var isEnable = $('#searchUserEnable').combobox('getValue');
    $('#UserTable').datagrid({
        url: '/User/GetUser',
        fitColumns: true,
        singleSelect: true,
        queryParams: {
            UserNo: code, UserName: name,
            Tel: tel, Email: email, Address: address,
            IdCard: idCard, MobilePhone: mobilePhone,
            IsVisible: isVisible, IsEnable: isEnable
        },
        //手动分页
        pagination: true,
        rownumbers: true,
        pagePosition: 'bottom',
        pageSize: 10,
        pageNumber: 1,
        pageList: [2, 10, 15, 20, 30, 50],
        columns: [[
            { title: '序号', width: 100, field: 'UserId', hidden: true },
            { title: '编号', width: 100, field: 'UserNo', },
            { title: '名称', width: 100, field: 'UserName', },
            { title: '座机', width: 100, field: 'Tel', },
            { title: '邮件', width: 100, field: 'Email', },
            { title: '地址', width: 100, field: 'Address', },

            { title: '身份证', width: 100, field: 'IdCard', },
            { title: '电话', width: 100, field: 'MobilePhone', },
            {
                title: '生日', width: 100, field: 'BirthDay',
                formatter: function (value, row, index) {
                    // value /Date(1563950624813)/  .slice(6)  从第6位开始(下标起始0)
                    var time = new Date(parseInt(value.slice(6)));
                    return time.getFullYear() + '-' + time.getMonth() + '-' + time.getDate();
                }
            },
            {
                title: '是否可见', width: 100, field: 'IsVisible',
                formatter: function (value, row, index) {
                    if (value == 0) return "不可见";
                    else if (value == 1) return "可见";
                }
            },
            {
                title: '是否激活', width: 100, field: 'IsEnable', formatter: function (value, row, index) {
                    if (value == 0) return "未激活";
                    else if (value == 1) return "已激活";
                }
            },
        ]],
        toolbar: '#searchUserToolBar',
    });
}
//清楚dialog中的选项
function clearUserForm() {
    $('#dgUserId').textbox('setValue','');
    $('#dgUserNo').textbox('setValue', '');
    $('#dgUserName').textbox('setValue', '');
    $('#dgTel').textbox('setValue', '');
    $('#dgEmail').textbox('setValue', '');
    $('#dgAddress').textbox('setValue', '');

    $('#dgIdCard').textbox('setValue', '');
    $('#dgMobilePhone').textbox('setValue', '');
    $('#dgBirthDay').datebox('setValue', '1970-01-01');

    $('#dgUserVisible').switchbutton('uncheck');
    $('#dgUserEnable').switchbutton('uncheck');

    $('#dgPassword').textbox('setValue', '');

    $('#dgRelationIdList').val('');
    $('.errorMsg').text('');
}
//初始化提示框
function initUserDialog() {
    $('#UserDialog').dialog({
        closed: true,
        closable: false,
        iconCls: 'icon-more',
        resizable: true,
        modal: true, 
        top:0,
        buttons: [{
            text: '保存',
            handler: function () {
                /*
                var name = $('#dgUserName').textbox('setValue','aa');
                var code = $('#dgUserNo').textbox('setValue','aaa');
                var tel = $('#dgTel').textbox('setValue','025-025-8888888');
                var email = $('#dgEmail').textbox('setValue', 'aa@qq.com');
                var address = $('#dgAddress').textbox('setValue', 'hubeiwuhan');
                var idCard = $('#dgIdCard').textbox('setValue', '421222199810102811');
                var mobilePhone = $('#dgMobilePhone').textbox('setValue', '13797804503');
                var password = $('#dgPassword').textbox('setValue','111111');
                */
                //验证
                checkUserClickSubmit('#dgUserName,#dgUserNo');
                if (!IsSubmitDialog('#UserDialog')) { return; }

                var id = $('#dgUserId').textbox('getValue');
                var name = $('#dgUserName').textbox('getValue');
                var code = $('#dgUserNo').textbox('getValue');
                var relationIdList = $('#dgRelationIdList').val();
                var tel = $('#dgTel').textbox('getValue');
                var email = $('#dgEmail').textbox('getValue');
                var address = $('#dgAddress').textbox('getValue');

                var idCard = $('#dgIdCard').textbox('getValue');
                var mobilePhone = $('#dgMobilePhone').textbox('getValue');
                var birthDay = $('#dgBirthDay').datebox('getValue');

                var isVisible = $('#dgUserVisible').switchbutton('options').checked ? 1 : 0;
                var isEnable = $('#dgUserEnable').switchbutton('options').checked ? 1 : 0;

                var password = $('#dgPassword').textbox('getValue');
                var data = {
                    UserId: id, UserName: name, UserNo: code,
                    RelationIdList: relationIdList, Tel: tel, Email: email, Address: address,
                    IdCard: idCard, MobilePhone: mobilePhone, BirthDay: birthDay,
                    IsVisible: isVisible, IsEnable: isEnable,
                    Password: password
                };
                var url = $('#UserDialog').panel('options').title == '新增' ? '/User/Insert' : '/User/Update';
                var result = dialogClickKeep(url, data);
                if (result == 'YES') {
                    clearUserForm();//清除选项
                    $('#UserDialog').dialog({ closed: true, });
                    reloadUserTable();
                } else if (result == '') { }else alert(data);
            }
        }, {
            text: '取消',
            handler: function () {
                clearUserForm();
                $('#UserDialog').dialog({ closed: true, });
            }
        }],
    });
}

//初始化点击按钮、更改下拉框事件
function initUserClick() {
    //点击Search按钮
    $('#btnUserSearch').click(function () {
        reloadUserTable();
    });
    //点击清除
    $('#btnUserClear').click(function () {
        $('#searchUserNo').textbox('setValue','');
        $('#searchUserName').textbox('setValue', '');
        $('#searchTel').textbox('setValue', '');
        $('#searchEmail').textbox('setValue', '');

        $('#searchAddress').textbox('setValue', '');
        $('#searchIdCard').textbox('setValue', '');
        $('#searchMobilePhone').textbox('setValue', '');

        $('#searchUserVisible').combobox('setValue', '-1');
        $('#searchUserEnable').combobox('setValue', '-1');
    });
    //点击新增;
    $('#btnUserInsert').click(function () {
        tableClickInsert('#UserDialog', '新增');
        $('#dgUserEnable').val(0);
        clearUserForm();
        //loadUserDgTable();//加载dgtable
        
    });
    //点击修改
    $('#btnUserUpdate').click(function () {
        var rows = $('#UserTable').datagrid('getSelections');
        if (rows.length <= 0) { show('请选择任意一行！'); return; }
        clearUserForm();
        var data = tableClickUpdateById(rows[0].UserId, '/User/Select', '#UserDialog', '修改');
        $('#dgUserId').textbox('setValue',data.UserId);
        $('#dgUserNo').textbox('setValue',data.UserNo);
        $('#dgUserName').textbox('setValue',data.UserName);
        $('#dgRelationIdList').val(data.RelationIdList);
        $('#dgTel').textbox('setValue',data.Tel);
        $('#dgEmail').textbox('setValue',data.Email);
        $('#dgAddress').textbox('setValue',data.Address);
        $('#dgIdCard').textbox('setValue',data.IdCard);
        $('#dgMobilePhone').textbox('setValue',data.MobilePhone);

        var birthDaySpan = new Date(parseInt(data.BirthDay.slice(6)));
        var birthDay = birthDaySpan.getFullYear() + '-' + birthDaySpan.getMonth() + '-' + birthDaySpan.getDate();
        $('#dgBirthDay').datebox('setValue', birthDay);
        $('#dgUserVisible').switchbutton(data.IsVisible == 1 ? 'check' : 'uncheck');
        $('#dgUserEnable').switchbutton(data.IsEnable == 1 ? 'check' : 'uncheck');
        $('#dgPassword').textbox('setValue','000000');
        //loadUserDgTable();//加载dgtable
    });
    //点击删除
    $('#btnUserDelete').click(function () {
        var rows = $('#UserTable').datagrid('getSelections');
        if (rows.length <= 0) { show('请选择任意一行！'); return; }
        var result = tableClickDeleteById(rows[0].UserId, '/User/Delete');
        if (result == 'YES') {
            clearUserForm();
            $('#UserDialog').dialog({ closed: true, });
            reloadUserTable();
        } else if (result == '') { }else alert(result);
    });
}

//重新加载表格
function reloadUserTable() {
    $('#UserTable').datagrid('load', {
        UserNo: $('#searchUserNo').textbox('getValue'),
        UserName: $('#searchUserName').textbox('getValue'),
        Tel: $('#searchTel').textbox('getValue'),
        Email: $('#searchEmail').textbox('getValue'),
        Address: $('#searchAddress').textbox('getValue'),
        IdCard: $('#searchIdCard').textbox('getValue'),
        MobilePhone: $('#searchMobilePhone').textbox('getValue'),
        IsVisible: $('#searchUserVisible').combobox('getValue'),
        IsEnable: $('#searchUserEnable').combobox('getValue'),
    });
}
