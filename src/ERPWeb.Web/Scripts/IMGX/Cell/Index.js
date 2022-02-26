//窗体加载
$(function () {
    initCellCombobox();
    $('#searchCellIsValid').combobox('setValue', '-1');
    $('#searchCellModel').combobox('setValue', '-1');
    initCellTable();
    initCellDialog();
    initCellClick();
    
});

//验证输入-点击提交时
function checkCellClickSubmit(idList) {
    $(idList).each(function (index, element) {
        checkCellAttention($(this));
    });
}
//验证输入-更改提示信息
function checkCellAttention(element) {
    var id = $(element).attr('id');
    var value = $(element).textbox('getValue');
    var errorMsg = $(element).parents('tr').find('.errorMsg');
    if (id == 'dgCellCode') {
        if (checkValLenString(value, 1, 64).code == 0) errorMsg.text('编号必须在1-64个字符之间'); else errorMsg.text('');
    } else if (id == 'dgCellName') {
        if (checkValLenString(value, 1, 64).code == 0) errorMsg.text('名称必须在1-64个字符之间'); else errorMsg.text('');
    } else if (id == 'dgCellMaxnum') {
        if (checkValLenString(value, 1, 1024).code == 0) errorMsg.text('备注必须满足1-1024个字节之间'); else errorMsg.text('');
    }
}
var comboboxJson = $.parseJSON("");
//初始化下拉框
function initCellCombobox() {
    comboboxJson = ajaxSame('../../Model/GetModelCombobox', null, 'post', 'json');
    $('#searchCellModel').combobox({
        data: comboboxJson,
        valueField: 'id',
        textField: 'name'
    });
    $('#dgCellModel').combobox({
        data: comboboxJson,
        valueField: 'id',
        textField: 'name'
    });
}
//初始化提示框
function initCellDialog() {
    $('#CellDialog').dialog({
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
                checkCellClickSubmit('#dgCellCode,#dgCellName');
                if (!IsSubmitDialog('#CellDialog')) { return; }

                var id = $('#dgCellId').textbox('getValue');
                var code = $('#dgCellCode').textbox('getValue');
                var name = $('#dgCellName').textbox('getValue');
                var isvalid = $('#dgCellIsValid').combobox('getValue');
                var modelid = $('#dgCellModel').combobox('getValue');

                var data = { Id: id, code: code, name: name, isvalid: isvalid, modelid: modelid  };
                var url = $('#CellDialog').panel('options').title == '新增' ? '../../Cell/InsertCell' : '../../Cell/UpdateCell';
                var result = dialogClickKeep(url, data, 'post', 'json');
                if (typeof (result) == 'string') { result = $.parseJSON(result); }
                if (result.Code == 200) {
                    clearDGCell();
                    $('#CellDialog').dialog({ closed: true, });
                    reloadCellTable();//重新加载表格
                } else {
                    show(result.Message);
                }
            }
        }, {
            text: '取消',
            iconCls: 'icon-cancel',
            handler: function () {
                clearDGCell();
                $('#CellDialog').dialog({ closed: true, });
            }
        }],
    });
}

//初始化表格
function initCellTable() {
    var code = $('#searchCellCode').textbox('getValue');
    var name = $('#searchCellName').textbox('getValue');
    var isvalid = $('#searchCellIsValid').combobox('getValue');
    var modelid = $('#searchCellModel').combobox('getValue');
    $('#CellTable').datagrid({
        //分页
        fitColumns: true,
        singleSelect: true,
        url: '../../Cell/GetCell',
        queryParams: { code: code, name: name, isvalid: isvalid, modelid: modelid },
        pagination: true,
        rownumbers: true,
        pagePosition: 'bottom',
        pageSize: 10,
        pageNumber: 1,
        pageList: [2, 10, 15, 20, 30, 50],
        toolbar: '#searchCellToolBar',
        fitColumns: true,
        //数据格式
        columns: [[
            { title: '序号', width: 100, field: 'id', fixed: true,/*hidden: true*/ },
            { title: '编号', width: 100, field: 'code', },
            { title: '名称', width: 100, field: 'name', },
            {
                title: '机种', width: 100, field: 'modelid', formatter: function (value, row) {
                    for (var i in comboboxJson) {
                        if (value == comboboxJson[i].id) {
                            return comboboxJson[i].name;
                        }
                    }
                }
            },
            {
                title: '更新时间', width: 100, field: 'chtime', formatter: function (value, row) {
                    return toDateStr(value);
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
function clearDGCell() {
    $('#dgCellId').textbox('setValue', '');
    $('#dgCellCode').textbox('setValue', '');
    $('#dgCellName').textbox('setValue', '');
    $('#dgCellIsValid').combobox('setValue', '1');
    $('.errorMsg').text('');
}


//初始化点击按钮、更改下拉框事件
function initCellClick() {
    //点击Search按钮
    $('#btnCellSearch').click(function () {
        reloadCellTable();//重新加载表格
    });
    //点击清除
    $('#btnCellClear').click(function () {
        $('#searchCellCode').textbox('setValue', '');
        $('#searchCellName').textbox('setValue', '');
        $('#searchCellIsValid').combobox('setValue', '-1');
        $('#searchCellModel').combobox('setValue', '-1');
    });
    //点击新增
    $('#btnCellInsert').click(function () {
        clearDGCell();
        tableClickInsert('#CellDialog', '新增');
    });
    //点击修改
    $('#btnCellUpdate').click(function () {
        var rows = $('#CellTable').datagrid('getSelections');
        if (rows.length <= 0) { show('请选择任意一行！'); return; }
        clearDGCell();
        $('#CellDialog').dialog({
            closed: false,
            title: '修改',
        });
        $('#dgCellId').textbox('setValue', rows[0].id);
        $('#dgCellCode').textbox('setValue', rows[0].code);
        $('#dgCellName').textbox('setValue', rows[0].name);
        $('#dgCellIsValid').combobox('setValue', rows[0].isvalid);
        $('#dgCellModel').combobox('setValue', rows[0].modelid);
    });
    
}

//重新加载表格
function reloadCellTable() {
    $('#CellTable').datagrid('load', {
        code: $('#searchCellCode').textbox('getValue'),
        name: $('#searchCellName').textbox('getValue'),
        isvalid: $('#searchCellIsValid').combobox('getValue'),
        modelid: $('#searchCellModel').combobox('getValue'),
    });
}