$(function () {
    initOrganizationDialog();
    initOrganizationTable();
    initOrganizationClick();
})
//验证输入-点击提交时
function checkOrganizationClickSubmit(idList) {
    $(idList).each(function (index, element) {
        checkOrganizationAttention($(this));
    });
}
//验证输入-更改提示信息
function checkOrganizationAttention(element) {
    var id = $(element).attr('id');
    var value = $(element).textbox('getValue');
    var errorMsg = $(element).parents('tr').find('.errorMsg');
    if (id == 'dgCode') {
        if (checkValLenString(value, 1, 16).code == 0) errorMsg.text('编号必须在1-16个字符之间'); else errorMsg.text('');
    } else if (id == 'dgName') {
        if (checkValLenString(value, 1, 16).code == 0) errorMsg.text('名称必须在1-16个字符之间'); else errorMsg.text('');
        //} else if (id == 'dgRemark') {
        //    if (checkValLenString(value, 1, 1024).code == 0) errorMsg.text('备注必须满足1-1024个字节之间'); else errorMsg.text('');
        //} else if (id == 'dgOrganizationVersion') {
        //    if (checkValLenString(value, 1, 64).code == 0) errorMsg.text('版本号必须满足1-64个字节之间'); else errorMsg.text('');
        //} else if (id == 'dgOrganizationApplication') {
        //    if (checkValLenString(value, 1, 64).code == 0) errorMsg.text('系统编号必须满足1-1024个字节之间'); else errorMsg.text('');
    } else if (id == 'dgOrganizationParentCode') {
        var data = $('#dgOrganizationParentCode').combobox('getValue');
        if (data <= 0) errorMsg.text('必选项'); else errorMsg.text('');
    }
}

//初始化表格
function initOrganizationTable() {
    var name = $('#searchName').textbox('getValue');
    var isVisible = $('#searchOrganizationVisible').combobox('getValue');
    var isEnable = $('#searchOrganizationEnable').combobox('getValue');
    var version = $('#searchOrganizationVersion').textbox('getValue');
    var application = $('#searchOrganizationApplication').textbox('getValue');
    $('#OrganizationTable').treegrid({
        //分页
        fitColumns: true,
        singleSelect: true,
        url: '/Organization/GetOrganization',
        queryParams: {
            Name: name,
            IsVisible: isVisible, IsEnable: isEnable,
            VersionCode: version, AppCode: application
        },
        idField: 'Id',
        treeField: 'Code',
        animate: true,
        pagination: true,
        rownumbers: true,
        pagePosition: 'bottom',
        pageSize: 50,
        pageNumber: 1,
        pageList: [20, 50, 100, 200],
        toolbar: '#searchOraganizationToolBar',
        //数据格式
        columns: [[
            { title: '序号', width: 100, field: 'Id', hidden: true },
            { title: '编号', width: 100, field: 'Code', },
            { title: '名称', width: 100, field: 'Name', },
            { title: '父编号', width: 100, field: 'ParentCode', },
            { title: '备注', width: 100, field: 'Remark', },
            {
                title: '创建时间', width: 100, field: 'CreateTime',
                formatter: function (value, row, index) {
                    // value /Date(1563950624813)/  .slice(6)  从第6位开始(下标起始0)
                    var time = new Date(parseInt(value.slice(6)));
                    return time.getFullYear() + '-' + time.getMonth() + '-' + time.getDate() +
                        ' ' + time.getHours() + ':' + time.getMinutes() + ':' + time.getSeconds();
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
            { title: '版本编号', width: 100, field: 'VersionCode', },
            { title: '系统编号', width: 100, field: 'AppCode', },
        ]],
    });
}
//清楚dialog中的选项
function clearDGOrganization() {
    $('#dgOrganizationId').textbox('setValue', '');
    $('#dgCode').textbox('setValue', '');
    $('#dgName').textbox('setValue', '');
    $('#dgRemark').textbox('setValue', '');
    $('#dgOrganizationParentCode').combobox('setValue', '-1');
    $('#dgOrganizationVisible').switchbutton('uncheck');
    $('#dgOrganizationEnable').switchbutton('uncheck');
    $('#dgOrganizationVersion').textbox('setValue', '');
    $('#dgOrganizationApplication').textbox('setValue', '');
    $('.errorMsg').text('');
}
//初始化提示框
function initOrganizationDialog() {
    //绑定列
    loadDropDownList('/Organization/GetDropDownListOrganization', '#dgOrganizationParentCode');
    $('#OrganizationDialog').dialog({
        closed: true,
        top: 0,
        iconCls: 'icon-more',
        resizable: true,
        modal: true,
        closable: false,
        buttons: [{
            text: '保存',
            handler: function () {
                //验证
                checkOrganizationClickSubmit('#dgCode,#dgName,#dgRemark,#dgOrganizationVersion,#dgOrganizationApplication,#dgOrganizationParentCode');
                if (!IsSubmitDialog('#OrganizationDialog')) { return; }

                var id = $('#dgOrganizationId').textbox('getValue');
                var code = $('#dgCode').textbox('getValue');
                var name = $('#dgName').textbox('getValue');
                var remark = $('#dgRemark').textbox('getValue');
                var parentCode = $('#dgOrganizationParentCode').combobox('getValue');
                var isVisible = $('#dgOrganizationVisible').switchbutton('options').checked ? 1 : 0;
                var isEnable = $('#dgOrganizationEnable').switchbutton('options').checked ? 1 : 0;
                var version = $('#dgOrganizationVersion').textbox('getValue');
                var application = $('#dgOrganizationApplication').textbox('getValue');
                var data = {
                    Id: id, Code: code, Name: name,
                    Remark: remark, ParentCode: parentCode,
                    IsVisible: isVisible, IsEnable: isEnable,
                    VersionCode: version, AppCode: application
                };
                //不是新增就是修改了
                var url = $('#OrganizationDialog').panel('options').title == '新增' ? '/Organization/Insert' : '/Organization/Update';

                var result = dialogClickKeep(url, data);
                if (result == 'YES') {
                    clearDGOrganization();
                    $('#OrganizationDialog').dialog({ closed: true, });
                    reloadOrganizationTable();//重新加载表格
                } else if (result == '') { }else alert(data);
            }
        }, {
            text: '取消',
            handler: function () {
                clearDGOrganization();
                $('#OrganizationDialog').dialog({ closed: true, });
            }
        }],
    });
}

//初始化点击按钮、更改下拉框事件
function initOrganizationClick() {
    //点击Search按钮
    $('#btnOrganizationSearch').click(function () {
        reloadOrganizationTable();//重新加载表格
    });
    //点击清除
    $('#btnOrganizationClear').click(function () {
        $('#searchName').textbox('setValue', '');
        $('#searchOrganizationVisible').combobox('setValue', '-1');
        $('#searchOrganizationEnable').combobox('setValue', '-1');
        $('#searchOrganizationVersion').textbox('setValue', '');
        $('#searchOrganizationApplication').textbox('setValue', '');
    });
    //点击新增
    $('#btnOrganizationInsert').click(function () {
        clearDGOrganization();
        $('#dgOrganizationVisible').switchbutton( 'check' );
        $('#dgOrganizationEnable').switchbutton('check');
        tableClickInsert('#OrganizationDialog', '新增');
    });
    //点击修改
    $('#btnOrganizationUpdate').click(function () {
        clearDGOrganization();
        var rows = $('#OrganizationTable').treegrid('getSelections');
        if (rows.length <= 0) { show('请选择任意一行！'); return; }
        var data = tableClickUpdateById(rows[0].Id, '/Organization/Select', '#OrganizationDialog', '修改');
        $('#dgOrganizationId').textbox('setValue', data.Id);
        $('#dgCode').textbox('setValue', data.Code);
        $('#dgName').textbox('setValue',data.Name);
        $('#dgRemark').textbox('setValue', data.Remark);
        $('#dgOrganizationParentCode').combobox('setValue', data.ParentCode);
        $('#dgOrganizationVisible').switchbutton(data.IsVisible == 1 ? 'check' : 'uncheck');
        $('#dgOrganizationEnable').switchbutton(data.IsEnable == 1 ? 'check' : 'uncheck');
        $('#dgOrganizationVersion').textbox('setValue',data.VersionCode);
        $('#dgOrganizationApplication').textbox('setValue',data.AppCode);
    });
    //点击删除
    $('#btnOrganizationDelete').click(function () {
        var rows = $('#OrganizationTable').treegrid('getSelections');
        if (rows.length <= 0) { show('请选择任意一行！'); return; }
        var result = tableClickDeleteById(rows[0].Id, '/Organization/Delete');
        if (result == 'YES') {
            clearDGOrganization();
            $('#OrganizationDialog').dialog({ closed: true, });
            reloadOrganizationTable();//重新加载表格
        } else if (result == 'Wait') { } else alert(result);
        
    });
}

//重新加载表格
function reloadOrganizationTable() {
    $('#OrganizationTable').treegrid('load', {
        Name: $('#searchName').textbox('getValue'),
        IsVisible: $('#searchOrganizationVisible').combobox('getValue'),
        IsEnable: $('#searchOrganizationEnable').combobox('getValue'),
        VersionCode: $('#searchOrganizationVersion').textbox('getValue'),
        AppCode: $('#searchOrganizationApplication').textbox('getValue'),
    });
}