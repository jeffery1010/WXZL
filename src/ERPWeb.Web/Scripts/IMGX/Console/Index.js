//窗体加载
$(function () {
    initConsoleCombobox();
    $('#searchConsoleIsValid').combobox('setValue', '-1');
    $('#searchConsoleModel').combobox('setValue', '-1');
    initConsoleTable();
    initConsoleDialog();
    initConsoleClick();
    
});

//验证输入-点击提交时
function checkConsoleClickSubmit(idList) {
    $(idList).each(function (index, element) {
        checkConsoleAttention($(this));
    });
}
//验证输入-更改提示信息
function checkConsoleAttention(element) {
    var id = $(element).attr('id');
    var value = $(element).textbox('getValue');
    var errorMsg = $(element).parents('tr').find('.errorMsg');
    if (id == 'dgConsoleIP4') {
        if (checkValLenString(value, 1, 64).code == 0) errorMsg.text('编号必须在1-64个字符之间'); else errorMsg.text('');
    } else if (id == 'dgConsoleName') {
        if (checkValLenString(value, 1, 64).code == 0) errorMsg.text('名称必须在1-64个字符之间'); else errorMsg.text('');
    } else if (id == 'dgConsoleMaxnum') {
        if (checkValLenString(value, 1, 1024).code == 0) errorMsg.text('备注必须满足1-1024个字节之间'); else errorMsg.text('');
    }
}
var comboboxJson = $.parseJSON("");
//初始化下拉框
function initConsoleCombobox() {
    comboboxJson = ajaxSame('../../Model/GetModelCombobox', null, 'post', 'json');
    $('#searchConsoleModel').combobox({
        data: comboboxJson,
        valueField: 'id',
        textField: 'name'
    });
    $('#dgConsoleModel').combobox({
        data: comboboxJson,
        valueField: 'id',
        textField: 'name'
    });
}
//初始化提示框
function initConsoleDialog() {
    $('#ConsoleDialog').dialog({
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
                checkConsoleClickSubmit('#dgConsoleIP4,#dgConsoleName');
                if (!IsSubmitDialog('#ConsoleDialog')) { return; }

                var id = $('#dgConsoleId').textbox('getValue');
                var ip4 = $('#dgConsoleIP4').textbox('getValue');
                var name = $('#dgConsoleName').textbox('getValue');
                var isvalid = $('#dgConsoleIsValid').combobox('getValue');
                var modelid = $('#dgConsoleModel').combobox('getValue');

                var data = { Id: id, ip4: ip4, name: name, isvalid: isvalid, modelid: modelid  };
                var url = $('#ConsoleDialog').panel('options').title == '新增' ? '../../Console/InsertConsole' : '../../Console/UpdateConsole';
                var result = dialogClickKeep(url, data, 'post', 'json');
                if (typeof (result) == 'string') { result = $.parseJSON(result); }
                if (result.Code == 200) {
                    clearDGConsole();
                    $('#ConsoleDialog').dialog({ closed: true, });
                    reloadConsoleTable();//重新加载表格
                } else {
                    show(result.Message);
                }
            }
        }, {
            text: '取消',
            iconCls: 'icon-cancel',
            handler: function () {
                clearDGConsole();
                $('#ConsoleDialog').dialog({ closed: true, });
            }
        }],
    });
}

//初始化表格
function initConsoleTable() {
    var ip4 = $('#searchConsoleIP4').textbox('getValue');
    var name = $('#searchConsoleName').textbox('getValue');
    var isvalid = $('#searchConsoleIsValid').combobox('getValue');
    var modelid = $('#searchConsoleModel').combobox('getValue');
    $('#ConsoleTable').datagrid({
        //分页
        fitColumns: true,
        singleSelect: true,
        url: '../../Console/GetConsole',
        queryParams: { ip4: ip4, name: name, isvalid: isvalid, modelid: modelid },
        pagination: true,
        rownumbers: true,
        pagePosition: 'bottom',
        pageSize: 10,
        pageNumber: 1,
        pageList: [2, 10, 15, 20, 30, 50],
        toolbar: '#searchConsoleToolBar',
        fitColumns: true,
        //数据格式
        columns: [[
            { title: '序号', width: 100, field: 'id', fixed: true,/*hidden: true*/ },
            { title: 'IP4', width: 100, field: 'ip4', },
            { title: '名称', width: 100, field: 'name', },
            {
                title: '机种', width: 100, field: 'modelid', formatter: function (value, row) {
                    if (typeof (comboboxJson[0].id) != 'number') comboboxJson = ajaxSame('../../Model/GetModelCombobox', null, 'post', 'json');
                    for (var i in comboboxJson) {
                        if (value == comboboxJson[i].id) {
                            return comboboxJson[i].name;
                        }
                    }
                }
            },
            {
                title: '状态', width: 100, field: 'isvalid', formatter: function (value, row) {
                    return value==1?'激活':'失效';
                }},
        ]],
    });
}

//清楚dialog中的选项
function clearDGConsole() {
    $('#dgConsoleId').textbox('setValue', '');
    $('#dgConsoleIP4').textbox('setValue', '');
    $('#dgConsoleName').textbox('setValue', '');
    $('#dgConsoleIsValid').combobox('setValue', '1');
    $('.errorMsg').text('');
}


//初始化点击按钮、更改下拉框事件
function initConsoleClick() {
    //点击Search按钮
    $('#btnConsoleSearch').click(function () {
        reloadConsoleTable();//重新加载表格
    });
    //点击清除
    $('#btnConsoleClear').click(function () {
        $('#searchConsoleIP4').textbox('setValue', '');
        $('#searchConsoleName').textbox('setValue', '');
        $('#searchConsoleIsValid').combobox('setValue', '-1');
        $('#searchConsoleModel').combobox('setValue', '-1');
    });
    //点击新增
    $('#btnConsoleInsert').click(function () {
        clearDGConsole();
        tableClickInsert('#ConsoleDialog', '新增');
    });
    //点击修改
    $('#btnConsoleUpdate').click(function () {
        var rows = $('#ConsoleTable').datagrid('getSelections');
        if (rows.length <= 0) { show('请选择任意一行！'); return; }
        clearDGConsole();
        $('#ConsoleDialog').dialog({
            closed: false,
            title: '修改',
        });
        $('#dgConsoleId').textbox('setValue', rows[0].id);
        $('#dgConsoleIP4').textbox('setValue', rows[0].ip4);
        $('#dgConsoleName').textbox('setValue', rows[0].name);
        $('#dgConsoleIsValid').combobox('setValue', rows[0].isvalid);
        $('#dgConsoleModel').combobox('setValue', rows[0].modelid);
    });
    
}

//重新加载表格
function reloadConsoleTable() {
    $('#ConsoleTable').datagrid('load', {
        ip4: $('#searchConsoleIP4').textbox('getValue'),
        name: $('#searchConsoleName').textbox('getValue'),
        isvalid: $('#searchConsoleIsValid').combobox('getValue'),
        modelid: $('#searchConsoleModel').combobox('getValue'),
    });
}