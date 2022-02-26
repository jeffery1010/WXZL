$(function () {
    initMenuDialog();
    initMenuTable();
    initMenuClick();
})

//验证输入-点击提交时
function checkMenuClickSubmit(idList) {
    $(idList).each(function (index, element) {
        checkMenuAttention($(this));
    });
}
//验证输入-更改提示信息
function checkMenuAttention(element) {
    var id = $(element).attr('id');
    var value = $(element).textbox('getValue');
    var errorMsg = $(element).parents('tr').find('.errorMsg');
    if (id == 'dgMenuCode') {
        if (checkValLenString(value, 1, 16).code == 0) errorMsg.text('编号必须在1-16个字符之间'); else errorMsg.text('');
    } else if (id == 'dgMenuName') {
        if (checkValLenSpaceString(value, 1, 8).code == 0) errorMsg.text('名字必须在1-8个非空格字符之间'); else errorMsg.text('');
    //} else if (id == 'dgMenuUrl') {
    //    if (checkValMVCUrl(value).code == 0) errorMsg.text('Url必须满足 /XXX/XXX 的形式'); else errorMsg.text('');
    } else if (id == 'dgMenuRemark') {
        if (checkValLenString(value, 1, 1024).code == 0) errorMsg.text('备注必须满足1-1024个字节之间'); else errorMsg.text('');
    } else if (id == 'dgMenuParentCode') {
        value = $(element).combobox('getValue');
        if (checkValLenString(value, 1, 16).code == 0) errorMsg.text('父级编号必须在1-16个字符之间'); else errorMsg.text('');
    }
}

//初始化表格
function initMenuTable() {
    var code = $('#searchMenuCode').textbox('getValue');
    var name = $('#searchMenuName').textbox('getValue');
    var url = $('#searchMenuUrl').textbox('getValue');
    var isVisible = $('#searchMenuVisible').combobox('getValue');
    var isEnable = $('#searchMenuEnable').combobox('getValue');
    $('#MenuTable').treegrid({
        //分页
        fitColumns: true,
        singleSelect: true,
        url: '/Menu/GetMenu',
        queryParams: {
            Name: name, Code: code, XPath: url,
            IsVisible: isVisible, IsEnable: isEnable,
        },
        idField: 'Id',
        treeField: 'Name',
        animate: true,
        pagination: true,
        rownumbers: true,
        pagePosition: 'bottom',
        pageSize: 50,
        pageNumber: 1,
        pageList: [20,50,100,200],
        toolbar: '#searchMenuToolBar',
        //数据格式
        columns: [[
            { title: '序号', width: 100, field: 'Id', hidden: true },
            { title: '名称', width: 100, field: 'Name', },
            { title: '编号', width: 100, field: 'Code', },
            { title: 'Url', width: 100, field: 'XPath', },
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
            { title: '父级编号', width: 100, field: 'ParentCode', },
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
function clearDGMenu() {
    $('#dgMenuId').textbox('setValue','');
    $('#dgMenuCode').textbox('setValue','');
    $('#dgMenuName').textbox('setValue','');
    $('#dgMenuUrl').textbox('setValue','');
    $('#dgMenuRemark').textbox('setValue', '');
    $('#dgMenuParentCode').combobox('setValue', '-1');
    $('#dgMenuVisible').switchbutton('uncheck');
    $('#dgMenuEnable').switchbutton('uncheck');
    $('.errorMsg').text('');
}
//初始化提示框
function initMenuDialog() {
    //绑定列
    loadDropDownList('/Menu/GetDropDownListMenu', '#dgMenuParentCode');
    $('#MenuDialog').dialog({
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
                checkMenuClickSubmit('#dgMenuCode,#dgMenuName,#dgMenuUrl,#dgMenuRemark,#dgMenuParentCode');
                if (!IsSubmitDialog('#MenuDialog')) { return; }

                var id = $('#dgMenuId').textbox('getValue');
                var code = $('#dgMenuCode').textbox('getValue');
                var name = $('#dgMenuName').textbox('getValue');
                var menuUrl = $('#dgMenuUrl').textbox('getValue');
                var remark = $('#dgMenuRemark').textbox('getValue');
                var parentCode = $('#dgMenuParentCode').combobox('getValue');
                var isVisible = $('#dgMenuVisible').switchbutton('options').checked ? 1 : 0;
                var isEnable = $('#dgMenuEnable').switchbutton('options').checked ? 1 : 0;
                var data = {
                    Id: id, Code: code, Name: name,
                    XPath: menuUrl, Remark: remark, ParentCode: parentCode,
                    IsVisible: isVisible, IsEnable: isEnable,
                };
                var url = $('#MenuDialog').panel('options').title == '新增' ? '/Menu/Insert' : '/Menu/Update';
                var result = dialogClickKeep(url, data);
                if (result == 'YES') {
                    clearDGMenu();
                    $('#MenuDialog').dialog({ closed: true, });
                    reloadMenuTable();//重新加载表格
                } else if (result == '') { } else alert(data);
            }
        }, {
            text: '取消',
            handler: function () {
                clearDGMenu();
                $('#MenuDialog').dialog({ closed: true, });
            }
        }],
    });
}

//初始化点击按钮、更改下拉框事件
function initMenuClick() {
    //点击Search按钮
    $('#btnMenuSearch').click(function () {
        reloadMenuTable();//重新加载表格
    });
    //点击清除
    $('#btnMenuClear').click(function () {
        $('#searchMenuCode').textbox('setValue','');
        $('#searchMenuName').textbox('setValue', '');
        $('#searchMenuUrl').textbox('setValue', '');
        $('#searchMenuVisible').combobox('setValue', '-1');
        $('#searchMenuEnable').combobox('setValue', '-1');
    });
    //点击新增
    $('#btnMenuInsert').click(function () {
        clearDGMenu();
        tableClickInsert('#MenuDialog', '新增');
    });
    //点击修改
    $('#btnMenuUpdate').click(function () {
        debugger;
        clearDGMenu();
        var rows = $('#MenuTable').treegrid('getSelections');
        if (rows.length <= 0) { show('请选择任意一行！'); return; }
        var data = tableClickUpdateById(rows[0].Id, '/Menu/Select', '#MenuDialog', '修改');
        $('#MenuDialog').dialog({
            closed: false,
            title: 'update',
        });
        $('#dgMenuId').textbox('setValue',data.Id);
        $('#dgMenuCode').textbox('setValue',data.Code);
        $('#dgMenuName').textbox('setValue',data.Name);
        $('#dgMenuUrl').textbox('setValue',data.XPath);
        $('#dgMenuRemark').textbox('setValue',data.Remark);
        $('#dgMenuParentCode').combobox('setValue', data.ParentCode);
        $('#dgMenuVisible').switchbutton(data.IsVisible == 1 ? 'check' : 'uncheck');
        $('#dgMenuEnable').switchbutton(data.IsEnable == 1 ? 'check' : 'uncheck');

    });
    //点击删除
    $('#btnMenuDelete').click(function () {
        var rows = $('#MenuTable').treegrid('getSelections');
        if (rows.length <= 0) { show('请选择任意一行！'); return; }

        var data = ajaxSame('/Menu/Delete', { Id: rows[0].Id }, 'post', 'json');
        if (typeof data == 'string') data = $.parseJSON(data);
        if (data.Code== 200) {
            clearDGMenu();
            $('#MenuDialog').dialog({ closed: true, });
            reloadMenuTable();//重新加载表格
        } else {
            show(data.Message);
        }
        
    });
}

//重新加载表格
function reloadMenuTable() {
    $('#MenuTable').treegrid('load', {
        Code: $('#searchMenuCode').textbox('getValue'),
        Name: $('#searchMenuName').textbox('getValue'),
        XPath: $('#searchMenuUrl').textbox('getValue'),
        IsVisible: $('#searchMenuVisible').combobox('getValue'),
        IsEnable: $('#searchMenuEnable').combobox('getValue'),
    });
}