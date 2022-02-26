$(function () {
    initPowerDialog();
    initPowerTable();
    initPowerClick();
})
//验证输入-点击提交时
function checkPowerClickSubmit(idList) {
    $(idList).each(function (index, element) {
        checkPowerAttention($(this));
    });
}
//验证输入-更改提示信息
function checkPowerAttention(element) {
    var id = $(element).attr('id');
    var value = $(element).textbox('getValue');
    var errorMsg = $(element).parents('tr').find('.errorMsg');
    if (id == 'dgPowerCode') {
        if (checkValLenString(value, 1, 64).code == 0) errorMsg.text('名称必须在1-64个字符之间'); else errorMsg.text('');
    } else if (id == 'dgPowerName') {
        if (checkValLenString(value, 1, 64).code == 0) errorMsg.text('名称必须在1-64个字符之间'); else errorMsg.text('');
    } else if (id == 'dgPowerRemark') {
        if (checkValLenString(value, 1, 1024).code == 0) errorMsg.text('备注必须满足1-1024个字节之间'); else errorMsg.text('');
    }
}

//初始化提示框
function initPowerDialog() {
    $('#PowerDialog').dialog({
        closed: true,//默认关闭状态
        closable: false,//不提供关闭页面
        top: 0,
        iconCls: 'icon-more',
        resizable: false,//是否可改变窗口大小
        modal: true,//模式化窗口，打开其他地方不可点击
        buttons: [{
            text: '保存',
            iconCls: 'icon-ok',
            handler: function () {
                //验证
                checkPowerClickSubmit('#dgPowerName,#dgPowerRemark');
                if (!IsSubmitDialog('#PowerDialog')) { return; }

                var id = $('#dgPowerId').textbox('getValue');
                var code = $('#dgPowerCode').textbox('getValue');
                var name = $('#dgPowerName').textbox('getValue');
                var remark = $('#dgPowerRemark').textbox('getValue');
                var isVisible = $('#dgPowerVisible').switchbutton('options').checked ? 1 : 0;
                var isEnable = $('#dgPowerEnable').switchbutton('options').checked ? 1 : 0;
                var data = { Id: id, PowerCode: code, PowerName: name, PowerRemark: remark, IsVisible: isVisible, IsEnable: isEnable };
                //不是新增就是修改了
                var url = $('#PowerDialog').panel('options').title == '新增' ? '/Power/Insert' : '/Power/Update';
                var result = dialogClickKeep(url, data);
                if (result == 'YES') {
                    clearDGPower();
                    $('#PowerDialog').dialog({ closed: true, });
                    reloadPowerTable();//重新加载表格
                } else if (result == '') { } else { alert(result); }
            }
        }, {
            text: '取消',
            iconCls: 'icon-cancel',
            handler: function () {
                clearDGPower();
                $('#PowerDialog').dialog({ closed: true, });
            }
        }],
    });
}

//初始化表格
function initPowerTable() {
    var code = $('#searchPowerCode').textbox('getValue');
    var name = $('#searchPowerName').textbox('getValue');
    var isVisible = $('#searchPowerVisible').combobox('getValue');
    var isEnable = $('#searchPowerEnable').combobox('getValue');
    $('#PowerTable').datagrid({
        //分页
        fitColumns: true,
        singleSelect: true,
        url: '/Power/GetPower',
        queryParams: { PowerCode: code, PowerName: name, IsVisible: isVisible, IsEnable: isEnable },
        pagination: true,
        rownumbers: true,
        pagePosition: 'bottom',
        pageSize: 50,
        pageNumber: 1,
        pageList: [20, 50, 100, 200],
        toolbar: '#searchPowerToolBar',
        //数据格式
        columns: [[
            { title: '序号', width: 100, field: 'Id', hidden: true },
            { title: '编号', width: 100, field: 'PowerCode', },
            { title: '名称', width: 100, field: 'PowerName', },
            { title: '备注', width: 100, field: 'PowerRemark', },
            {
                title: '创建时间', width: 100, field: 'CreateTime',
                formatter: function (value, row, index) {
                    // value /Date(1563950624813)/  .slice(6)  从第6位开始(下标起始0)
                    var time = new Date(parseInt(value.slice(6)));
                    return time.getFullYear() + '-' + time.getMonth() + '-' + time.getDate() +
                        ' ' + time.getHours() + ':' + time.getMinutes() + ':' + time.getSeconds();
                }
            },
            { title: '创建人', width: 100, field: 'UserName', },
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
//清楚dialog中的选项
function clearDGPower() {
    $('#dgPowerId').textbox('setValue', '');
    $('#dgPowerCode').textbox('setValue', '');
    $('#dgPowerName').textbox('setValue', '');
    $('#dgPowerRemark').textbox('setValue', '');
    $('#dgPowerVisible').switchbutton('uncheck');
    $('#dgPowerEnable').switchbutton('uncheck');
    $('.errorMsg').text('');
}


//初始化点击按钮、更改下拉框事件
function initPowerClick() {
    //点击Search按钮
    $('#btnPowerSearch').click(function () {
        reloadPowerTable();//重新加载表格
    });
    //点击清除
    $('#btnPowerClear').click(function () {
        $('#searchPowerCode').textbox('setValue', '');
        $('#searchPowerName').textbox('setValue', '');
        $('#searchPowerVisible').combobox('setValue', '-1');
        $('#searchPowerEnable').combobox('setValue', '-1');
    });
    //点击新增
    $('#btnPowerInsert').click(function () {
        clearDGPower();
        tableClickInsert('#PowerDialog', '新增');//Method.js
    });
    //点击修改
    $('#btnPowerUpdate').click(function () {
        clearDGPower();//清除Dialog数据
        var rows = $('#PowerTable').datagrid('getSelections');
        if (rows.length <= 0) { show('请选择任意一行！'); return; }
        var data = tableClickUpdateById(rows[0].Id, '/Power/Select', '#PowerDialog', '修改');
        $('#dgPowerId').textbox('setValue', data.Id);
        $('#dgPowerCode').textbox('setValue', data.PowerCode);
        $('#dgPowerName').textbox('setValue',data.PowerName);
        $('#dgPowerRemark').textbox('setValue',data.PowerRemark);
        $('#dgPowerVisible').switchbutton(data.IsVisible == 1 ? 'check' : 'uncheck');
        $('#dgPowerEnable').switchbutton(data.IsEnable == 1 ? 'check' : 'uncheck');
    });
    //点击删除
    $('#btnPowerDelete').click(function () {
        var rows = $('#PowerTable').datagrid('getSelections');
        if (rows.length <= 0) { show('请选择任意一行！'); return; }
        var result = tableClickDeleteById(rows[0].Id, '/Power/Delete');
        if (result == 'YES') {
            reloadPowerTable();//重新加载表格
        } else if (result == '') {
            //没确定删除
        } else {
            alert(result);
        }

    });
}

//重新加载表格
function reloadPowerTable() {
    $('#PowerTable').datagrid('load', {
        PowerCode: $('#searchPowerCode').textbox('getValue'),
        PowerName: $('#searchPowerName').textbox('getValue'),
        IsVisible: $('#searchPowerVisible').combobox('getValue'),
        IsEnable: $('#searchPowerEnable').combobox('getValue'),
    });
}