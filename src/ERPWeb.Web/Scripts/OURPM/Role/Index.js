$(function () {
    initRoleDialog();
    initRoleTable();
    initRoleClick();
})
//验证输入-点击提交时
function checkRoleClickSubmit(idList) {
    $(idList).each(function (index, element) {
        checkRoleAttention($(this));
    });
}
//验证输入-更改提示信息
function checkRoleAttention(element) {
    var id = $(element).attr('id');
    var value = $(element).textbox('getValue');
    var errorMsg = $(element).parents('tr').find('.errorMsg');
    if (id == 'dgRoleCode') {
        if (checkValLenString(value, 1, 64).code == 0) errorMsg.text('编号必须在1-64个字符之间'); else errorMsg.text('');
    } else if (id == 'dgRoleName') {
        if (checkValLenString(value, 1, 64).code == 0) errorMsg.text('名称必须在1-64个字符之间'); else errorMsg.text('');
    } else if (id == 'dgRoleRemark') {
        if (checkValLenString(value, 1, 1024).code == 0) errorMsg.text('备注必须满足1-1024个字节之间'); else errorMsg.text('');
    }
}


//初始化提示框
function initRoleDialog() {
    //绑定列
    var data = ajaxSame('/Organization/GetDropDownListOrganization', null, 'post', 'json');
    if (typeof data == 'string') data = $.parseJSON(data);
    loadDropDownListAll('#dgRoleOrganization', data, 'Key', 'Text', 0);
    $('#RoleDialog').dialog({
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
                checkRoleClickSubmit('#dgRoleCode,#dgRoleName,#dgRoleRemark');
                if (!IsSubmitDialog('#RoleDialog')) { return; }

                var id = $('#dgRoleId').textbox('getValue');
                var code = $('#dgRoleCode').textbox('getValue');
                var name = $('#dgRoleName').textbox('getValue');
                var remark = $('#dgRoleRemark').textbox('getValue');
                var isVisible = $('#dgRoleVisible').switchbutton('options').checked ? 1 : 0;
                var isEnable = $('#dgRoleEnable').switchbutton('options').checked ? 1 : 0;
                var organizationid = $('#dgRoleOrganization').combobox('getValue');
                var data = { Id: id, RoleCode: code, RoleName: name, RoleRemark: remark, IsVisible: isVisible, IsEnable: isEnable, OrganizationId: organizationid };
                var url = $('#RoleDialog').panel('options').title == '新增' ? '/Role/Insert' : '/Role/Update';
                var result = dialogClickKeep(url, data);
                if (result == 'YES') {
                    clearDGRole();
                    $('#RoleDialog').dialog({ closed: true, });
                    reloadRoleTable();//重新加载表格
                } else if (result == '') { } else alert(result);
            }
        }, {
            text: '取消',
            iconCls: 'icon-cancel',
            handler: function () {
                clearDGRole();
                $('#RoleDialog').dialog({ closed: true, });
            }
        }],
    });
}

//初始化表格
function initRoleTable() {
    var code = $('#searchRoleCode').textbox('getValue');
    var name = $('#searchRoleName').textbox('getValue');
    var isVisible = $('#searchRoleVisible').combobox('getValue');
    var isEnable = $('#searchRoleEnable').combobox('getValue');
    $('#RoleTable').datagrid({
        //分页
        fitColumns: true,
        singleSelect: true,
        url: '/Role/GetRole',
        queryParams: { RoleCode: code, RoleName: name, IsVisible: isVisible, IsEnable: isEnable },
        pagination: true,
        rownumbers: true,
        pagePosition: 'bottom',
        pageSize: 10,
        pageNumber: 1,
        pageList: [2, 10, 15, 20, 30, 50],
        toolbar: '#searchRoleToolBar',
        fitColumns: true,
        //数据格式
        columns: [[
            { title: '序号', width: 100, field: 'Id', hidden: false },
            { title: '编号', width: 100, field: 'RoleCode', },
            { title: '名称', width: 100, field: 'RoleName', },
            { title: '备注', width: 100, field: 'RoleRemark', },
            {
                title: '创建时间', width: 100, field: 'CreateTime', formatter: function (value, row, index) {
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
function clearDGRole() {
    $('#dgRoleId').textbox('setValue', '');
    $('#dgRoleCode').textbox('setValue', '');
    $('#dgRoleName').textbox('setValue', '');
    $('#dgRoleRemark').textbox('setValue', '');
    $('#dgRoleVisible').switchbutton('uncheck');
    $('#dgRoleEnable').switchbutton('uncheck');
    $('.errorMsg').text('');
}


//初始化点击按钮、更改下拉框事件
function initRoleClick() {
    //点击Search按钮
    $('#btnRoleSearch').click(function () {
        reloadRoleTable();//重新加载表格
    });
    //点击清除
    $('#btnRoleClear').click(function () {
        $('#searchRoleCode').textbox('setValue', '');
        $('#searchRoleName').textbox('setValue', '');
        $('#searchRoleVisible').combobox('setValue', '-1');
        $('#searchRoleEnable').combobox('setValue', '-1');
    });
    //点击新增
    $('#btnRoleInsert').click(function () {
        clearDGRole();
        tableClickInsert('#RoleDialog', '新增');
    });
    //点击修改
    $('#btnRoleUpdate').click(function () {
        var rows = $('#RoleTable').datagrid('getSelections');
        if (rows.length <= 0) { show('请选择任意一行！'); return; }
        clearDGRole();
        var data = tableClickUpdateById(rows[0].Id, '/Role/Select', '#RoleDialog', '修改');
         
        $('#dgRoleId').textbox('setValue', data.Id);
        $('#dgRoleCode').textbox('setValue', data.RoleCode);
        $('#dgRoleName').textbox('setValue', data.RoleName);
        $('#dgRoleRemark').textbox('setValue', data.RoleRemark);
        $('#dgRoleVisible').switchbutton(data.IsVisible == 1 ? 'check' : 'uncheck');
        $('#dgRoleEnable').switchbutton(data.IsEnable == 1 ? 'check' : 'uncheck');
    });
    //点击删除
    $('#btnRoleDelete').click(function () {
        var rows = $('#RoleTable').datagrid('getSelections');
        if (rows.length <= 0) { show('请选择任意一行！'); return; }
        var result = tableClickDeleteById(rows[0].Id, '/Role/Delete');
        if (result == 'YES') {
            clearDGRole();
            $('#RoleDialog').dialog({ closed: true, });
            reloadRoleTable();//重新加载表格
        } else if (result == '') { } else alert(result);
    });
}

//重新加载表格
function reloadRoleTable() {
    $('#RoleTable').datagrid('load', {
        RoleCode: $('#searchRoleCode').textbox('getValue'),
        RoleName: $('#searchRoleName').textbox('getValue'),
        IsVisible: $('#searchRoleVisible').combobox('getValue'),
        IsEnable: $('#searchRoleEnable').combobox('getValue'),
    });
}