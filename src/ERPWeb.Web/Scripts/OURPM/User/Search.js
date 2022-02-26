//窗体加载
$(function () {
    initUserTable();
});

//初始化表格
function initUserTable() {
    var code = '';
    var name = '';
    var tel = '';
    var email = '';
    var address = '';
    var idCard = '';
    var mobilePhone = '';
    var isVisible = -1;
    var isEnable = -1;
    $('.easyui-datagrid[title="SearchPartialUser"]').datagrid({
        //分页
        fitColumns: true,
        singleSelect: true,
        url: '/User/GetUser',
        queryParams: {
            UserNo: code, UserName: name,
            Tel: tel, Email: email, Address: address,
            IdCard: idCard, MobilePhone: mobilePhone,
            IsVisible: isVisible, IsEnable: isEnable},
        pagination: true,
        rownumbers: true,
        pagePosition: 'bottom',
        pageSize: 10,
        pageNumber: 1,
        pageList: [2, 10, 15, 20, 30, 50],
        toolbar: '.userToolBar',
        fitColumns: true,
        //数据格式
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
    });
}


//重新加载表格
function reloadUserTable(code, name, tel, email, address, idCard, mobilePhone, isvalid,isenable) {
    $('.easyui-datagrid[title="SearchPartialUser"]').datagrid('load', {
        UserNo: code, UserName: name,
        Tel: tel, Email: email, Address: address,
        IdCard: idCard, MobilePhone: mobilePhone,
        IsVisible: isvalid, IsEnable: isenable
    });
}

function clickSearch(obj) {


    var code = $(obj).siblings('.inputtext[title="code"]').first().val();
    var name = $(obj).siblings('.inputtext[title="name"]').first().val();

    var tel = $(obj).siblings('.inputtext[title="tel"]').first().val();
    var email = $(obj).siblings('.inputtext[title="email"]').first().val();
    var address = $(obj).siblings('.inputtext[title="address"]').first().val();
    var idCard = $(obj).siblings('.inputtext[title="idCard"]').first().val();
    var mobilePhone = $(obj).siblings('.inputtext[title="mobilePhone"]').first().val();

    var isvalid = $(obj).siblings('.easyui-combobox[title="isvalid"]').first().val();
    var isenable = $(obj).siblings('.easyui-combobox[title="isenable"]').first().val();
    reloadUserTable(code, name, tel, email, address, idCard, mobilePhone, isvalid, isenable);//重新加载表格
}

function clickSearchClear(obj) {
    $(obj).siblings('.inputtext[title="code"]').first().val('');
    $(obj).siblings('.inputtext[title="name"]').first().val('');

    $(obj).siblings('.inputtext[title="tel"]').first().val('');
    $(obj).siblings('.inputtext[title="email"]').first().val('');
    $(obj).siblings('.inputtext[title="address"]').first().val('');
    $(obj).siblings('.inputtext[title="idCard"]').first().val('');
    $(obj).siblings('.inputtext[title="mobilePhone"]').first().val('');

    $(obj).siblings('.easyui-combobox[title="isvalid"]').first().val(-1);

    $(obj).siblings('.easyui-combobox[title="isenable"]').first().val(-1);
}


