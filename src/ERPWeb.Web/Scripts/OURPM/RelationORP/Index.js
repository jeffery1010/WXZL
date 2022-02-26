$(function () {
    loadRelationORPDropDownList();
    initRelationORPDialog();
    initRelationORPTable();
    initRelationORPClick();
})
//验证输入-点击提交时
function checkRelationORPClickSubmit(idList) {
    $(idList).each(function (index, element) {
        checkRelationORPAttention($(this));
    });
}
//验证输入-更改提示信息
function checkRelationORPAttention(element) {
    var id = $(element).attr('id');
    var value = $(element).textbox('getValue');
    var errorMsg = $(element).parents('tr').find('.errorMsg');
    if (id == 'dgRelationORPCode') {
        if (checkValLenString(value, 1, 64).code == 0) errorMsg.text('编号必须在1-64个字符之间'); else errorMsg.text('');
    } else if (id == 'dgRelationORPName') {
        if (checkValLenString(value, 1, 64).code == 0) errorMsg.text('名称必须在1-64个字符之间'); else errorMsg.text('');
    }
}

//初始化组织、角色下拉框
function loadRelationORPDropDownList() {
    loadDropDownList('/Organization/GetDropDownListOrganization', '#searchRelationORPOrganizationId');
    loadDropDownList('/Role/GetDropDownListRole', '#searchRelationORPRoleId');
    loadDropDownList('/Organization/GetDropDownListOrganization', '#dgRelationORPOrganizationId');
    loadDropDownList('/Role/GetDropDownListRole', '#dgRelationORPRoleId');
}
//初始化提示框
function initRelationORPDialog() {
    $('#RelationORPDialog').dialog({
        closed: true,
        closable: false,
        top:0,
        iconCls: 'icon-more',
        resizable: true,
        modal: true,
        buttons: [{
            text: '保存',
            handler: function () {
                //验证
                checkRelationORPClickSubmit('#dgRelationORPCode,#dgRelationORPName');
                if (!IsSubmitDialog('#RelationORPDialog')) { return; }

                var id = $('#dgRelationORPId').textbox('getValue');
                var code = $('#dgRelationORPCode').textbox('getValue');
                var name = $('#dgRelationORPName').textbox('getValue');
                var powerList = $('#dgRelationORPSelected').val();
                var organizationId = $('#dgRelationORPOrganizationId').combobox('getValue');
                var roleId = $('#dgRelationORPRoleId').combobox('getValue');
                var isVisible = $('#dgRelationORPVisible').switchbutton('options').checked ? 1 : 0;
                var isEnable = $('#dgRelationORPEnable').switchbutton('options').checked ? 1 : 0;
                var data = {
                    Id: id, Code: code, RelationName: name, PowerIdList: powerList,
                    RoleId: roleId, OrganizationId: organizationId,
                    IsVisible: isVisible, IsEnable: isEnable
                };
                var url = $('#RelationORPDialog').panel('options').title == '新增' ? '/RelationORP/Insert' : '/RelationORP/Update';
                var result=dialogClickKeep(url, data);
                if (result == 'YES') {
                    clearRelationORPForm();
                    $('#RelationORPDialog').dialog({ closed: true, });
                    reloadRelationORPTable();
                } else if (result == '') { } else show('请确保组织角色的唯一性');
            }
        }, {
            text: '取消',
            handler: function () {
                clearRelationORPForm();
                $('#RelationORPDialog').dialog({ closed: true, });
            }
        }],
    });
}
//清除dialog中的选项
function clearRelationORPForm() {
    $('#dgRelationORPId').textbox('setValue', '');
    $('#dgRelationORPCode').textbox('setValue', '');
    $('#dgRelationORPName').textbox('setValue', '');
    $('#dgRelationORPSelected').val('');

    $('#dgRelationORPOrganizationId').removeAttr("disabled");
    $('#dgRelationORPRoleId').removeAttr("disabled");
    $('#dgRelationORPOrganizationId').combobox('setValue', '-1');
    $('#dgRelationORPRoleId').combobox('setValue', '-1');

    $('#dgRelationORPVisible').switchbutton('uncheck');
    $('#dgRelationORPEnable').switchbutton('uncheck');

    $('#dbRelationORPTable').datagrid('unselectAll');
    $('.errorMsg').text('');
}

//加载Dialog中的表格
function loadRelationORPDgTable() {
    $('#dbRelationORPTable').datagrid({
        url: '/Power/GetPowerNoSearch',
        idField: "Id",
        pagination: true,
        rownumbers: true,
        pagePosition: 'top',
        pageSize: 5,
        pageNumber: 1,
        pageList: [2, 5, 10, 15, 20, 30, 50],
        fitColumns: true,
        singleSelect: false,
        columns: [[
            { checkbox: true, field: 'CreateUserId', },
            { title: '权限编号', width: 100, field: 'Id', },
            { title: '权限名称', width: 100, field: 'PowerName', },
            { title: '权限备注', width: 100, field: 'PowerRemark', },
            {
                title: '创建时间', width: 100, field: 'CreateTime',
                formatter: function (value, row, index) {
                    // value /Date(1563950624813)/  .slice(6)  从第6位开始(下标起始0)
                    var time = new Date(parseInt(value.slice(6)));
                    return time.getFullYear() + '-' + time.getMonth() + '-' + time.getDate() +
                        ' ' + time.getHours() + ':' + time.getMinutes() + ':' + time.getSeconds();
                }
            },
        ]],
        onLoadSuccess: function () {
            //$(this).datagrid('selectRow', 1);//按行号选择
            //$(this).datagrid('selectRecord', 3);//按键选择
            var selectedList = $('#dgRelationORPSelected').val();
            var powerArray = selectedList.split(',');
            for (var i = 0; i < powerArray.length; i++) {
                if (powerArray[i] == '') continue;
                $(this).datagrid('selectRecord', powerArray[i]);
            }
        },
        onCheck: function (index, row) {
            //选中某行时   
            //index行号(0开始)  row当前选中的行   row.XXX行中的某一条属性
            var selectedList = $('#dgRelationORPSelected').val();
            var oldStr = ',' + row.Id;
            if (isContainsArr(selectedList, oldStr,',')) return;//当包含字串时，不再新增
                selectedList = selectedList + oldStr;
            $('#dgRelationORPSelected').val(selectedList);
        },
        onUncheck: function (index, row) {
            //取消选中时   
            //index行号(0开始)  row当前选中的行   row.XXX行中的某一条属性
            var selectedList = $('#dgRelationORPSelected').val();
            var oldStr = ',' + row.Id;
            //包含字串才替换
            if (isContainsArr(selectedList, oldStr, ',')) selectedList = arrStrRemoveStr(selectedList, oldStr, ',');
            $('#dgRelationORPSelected').val(selectedList);
        },
        onSelectAll: function (rows) {
            //用户选中所有的行
            var selectedList = $('#dgRelationORPSelected').val();
            var oldStr = '';
            for (var i in rows) {
                oldStr = ',' + rows[i].Id;
                if (isContainsArr(selectedList, oldStr,',')) continue;
                selectedList = selectedList + oldStr;
            }
            $('#dgRelationORPSelected').val(selectedList);
        },
        onUnselectAll: function (rows) {
            //用户取消选中所有的行
            console.info(rows);
            var selectedList = $('#dgRelationORPSelected').val();
            var oldStr = '';
            for (var i in rows) {
                oldStr = ',' + rows[i].Id;
                if (isContainsArr(selectedList, oldStr, ',')) selectedList = arrStrRemoveStr(selectedList, oldStr, ',');
            }
            $('#dgRelationORPSelected').val(selectedList);
        },
    });

}

//初始化表格
function initRelationORPTable() {
    var code = $('#searchRelationORPCode').textbox('getValue');
    var name = $('#searchRelationORPName').textbox('getValue');
    var organizationId = $('#searchRelationORPOrganizationId').combobox('getValue');
    var roleId = $('#searchRelationORPRoleId').combobox('getValue');
    var isVisible = $('#searchRelationORPVisible').combobox('getValue');
    var isEnable = $('#searchRelationORPEnable').combobox('getValue');

    $('#RelationORPTable').datagrid({
        //分页
        fitColumns: true,
        singleSelect: true,
        url: '/RelationORP/GetRelationORP',
        queryParams: {
            Code: code, RelationName: name, OrganizationId: organizationId, RoleId: roleId,
            IsVisible: isVisible, IsEnable: isEnable
        },
        pagination: true,
        rownumbers: true,
        pagePosition: 'bottom',
        pageSize: 10,
        pageNumber: 1,
        pageList: [2, 10, 15, 20, 30, 50],
        columns: [[
            { title: '序号', width: 100, field: 'Id', hidden: true },
            { title: '关系编号', width: 100, field: 'Code', },
            { title: '关系名称', width: 100, field: 'RelationName', },
            { title: '组织编号', width: 100, field: 'OrganizationId', hidden: true },
            { title: '组织名称', width: 100, field: 'Name', },
            { title: '角色编号', width: 100, field: 'RoleId', hidden: true },
            { title: '角色名称', width: 100, field: 'RoleName', },
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
        toolbar: '#searchRelationORPToolBar',
        
    });

}
//初始化点击按钮、更改下拉框事件
function initRelationORPClick() {
    //点击Search按钮
    $('#btnRelationORPSearch').click(function () {
        reloadRelationORPTable();
    });
    //点击清除
    $('#btnRelationORPClear').click(function () {
        $('#searchRelationORPCode').textbox('setValue', '');
        $('#searchRelationORPName').textbox('setValue', '');
        $('#searchRelationORPRoleId').combobox('setValue','-1');
        $('#searchRelationORPOrganizationId').combobox('setValue','-1');
    });
    //点击新增
    $('#btnRelationORPInsert').click(function () {
        tableClickInsert('#RelationORPDialog', '新增');
        clearRelationORPForm();
        loadRelationORPDgTable();//加载表格数据
    });
    //点击修改
    $('#btnRelationORPUpdate').click(function () {
        var rows = $('#RelationORPTable').datagrid('getSelections');
        if (rows.length <= 0) { show('请选择任意一行！'); return; }
        clearRelationORPForm();
        var data = tableClickUpdateById($('#RelationORPTable').datagrid('getSelections')[0].Id, '/RelationORP/Select', '#RelationORPDialog', '修改');
        $('#dgRelationORPId').textbox('setValue', data.Id);
        $('#dgRelationORPCode').textbox('setValue', data.Code);
        $('#dgRelationORPName').textbox('setValue', data.RelationName);
        $('#dgRelationORPSelected').val(data.PowerIdList);

        $('#dgRelationORPOrganizationId').combobox('setValue',data.OrganizationId).attr("disabled", "disabled");
        $('#dgRelationORPRoleId').combobox('setValue',data.RoleId).attr("disabled", "disabled");

        $('#dgRelationORPVisible').switchbutton(data.IsVisible == 1 ? 'check' : 'uncheck');
        $('#dgRelationORPEnable').switchbutton(data.IsEnable == 1 ? 'check' : 'uncheck');
        loadRelationORPDgTable();//加载表格数据
    });
    //点击删除
    $('#btnRelationORPDelete').click(function () {
        var rows = $('#RelationORPTable').datagrid('getSelections');
        if (rows.length <= 0) { show('请选择任意一行！'); return; }
        var result = tableClickDeleteById(rows[0].Id, '/RelationORP/Delete');
        if (result == 'YES') {
            clearRelationORPForm();
            $('#RelationORPDialog').dialog({ closed: true, });
            reloadRelationORPTable();
        } else if (result == '') { } else alert(result);
    });
}

//重新加载表格
function reloadRelationORPTable() {
    $('#RelationORPTable').datagrid('load', {
        Code: $('#searchRelationORPCode').textbox('getValue'),
        RelationName: $('#searchRelationORPName').textbox('getValue'),
        OrganizationId: $('#searchRelationORPOrganizationId').combobox('getValue'),
        RoleId: $('#searchRelationORPRoleId').combobox('getValue'),
        IsVisible: $('#searchRelationORPVisible').combobox('getValue'),
        IsEnable: $('#searchRelationORPEnable').combobox('getValue'),
    });
}