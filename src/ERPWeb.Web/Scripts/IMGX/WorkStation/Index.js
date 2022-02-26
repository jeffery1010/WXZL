//窗体加载
$(function () {
    initWorkStationCombobox();
    $('#searchWorkStationStatus').combobox('setValue', '-1');
    $('#searchWorkStationModel').combobox('setValue', '-1');
    initWorkStationTable();
    initWorkStationDialog();
    initWorkStationClick();
    
});

//验证输入-点击提交时
function checkWorkStationClickSubmit(idList) {
    $(idList).each(function (index, element) {
        checkWorkStationAttention($(this));
    });
}
//验证输入-更改提示信息
function checkWorkStationAttention(element) {
    var id = $(element).attr('id');
    var value = $(element).textbox('getValue');
    var errorMsg = $(element).parents('tr').find('.errorMsg');
    if (id == 'dgWorkStationCode') {
        if (checkValLenString(value, 1, 64).code == 0) errorMsg.text('编号必须在1-64个字符之间'); else errorMsg.text('');
    } else if (id == 'dgWorkStationName') {
        if (checkValLenString(value, 1, 64).code == 0) errorMsg.text('名称必须在1-64个字符之间'); else errorMsg.text('');
    } else if (id == 'dgWorkStationMaxnum') {
        if (checkValLenString(value, 1, 1024).code == 0) errorMsg.text('备注必须满足1-1024个字节之间'); else errorMsg.text('');
    }
}
var comboboxJson = $.parseJSON("[]");
//初始化下拉框
function initWorkStationCombobox() {
    comboboxJson = ajaxSame('../../Model/GetModelCombobox', null, 'post', 'json');
    $('#searchWorkStationModel').combobox({
        data: comboboxJson,
        valueField: 'id',
        textField: 'name'
    });
    $('#dgWorkStationModel').combobox({
        data: comboboxJson,
        valueField: 'id',
        textField: 'name'
    });
}
//初始化提示框
function initWorkStationDialog() {
    $('#WorkStationDialog').dialog({
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
                checkWorkStationClickSubmit('#dgWorkStationCode,#dgWorkStationName');
                if (!IsSubmitDialog('#WorkStationDialog')) { return; }

                var id = $('#dgWorkStationId').textbox('getValue');
                var code = $('#dgWorkStationCode').textbox('getValue');
                var name = $('#dgWorkStationName').textbox('getValue');
                var status = $('#dgWorkStationStatus').combobox('getValue');
                var modelid = $('#dgWorkStationModel').combobox('getValue');
                var fsequence = $('#dgWorkStationSequence').textbox('getValue');

                var data = { Id: id, code: code, name: name, status: status, modelid: modelid, fsequence: fsequence };
                var url = $('#WorkStationDialog').panel('options').title == '新增' ? '../../WorkStation/InsertWorkStation' : '../../WorkStation/UpdateWorkStation';
                var result = dialogClickKeep(url, data, 'post', 'json');
                if (typeof (result) == 'string') { result = $.parseJSON(result); }
                if (result.Code == 200) {
                    clearDGWorkStation();
                    $('#WorkStationDialog').dialog({ closed: true, });
                    reloadWorkStationTable();//重新加载表格
                } else {
                    show(result.Message);
                }
            }
        }, {
            text: '取消',
            iconCls: 'icon-cancel',
            handler: function () {
                clearDGWorkStation();
                $('#WorkStationDialog').dialog({ closed: true, });
            }
        }],
    });
}

//初始化表格
function initWorkStationTable() {
    var code = $('#searchWorkStationCode').textbox('getValue');
    var name = $('#searchWorkStationName').textbox('getValue');
    var status = $('#searchWorkStationStatus').combobox('getValue');
    var modelid = $('#searchWorkStationModel').combobox('getValue');
    var fsequence = $('#searchWorkStationSequence').textbox('getValue');
    $('#WorkStationTable').datagrid({
        //分页
        fitColumns: true,
        singleSelect: true,
        url: '../../WorkStation/GetWorkStation',
        queryParams: { code: code, name: name, status: status, modelid: modelid, fsequence: fsequence },
        pagination: true,
        rownumbers: true,
        pagePosition: 'bottom',
        pageSize: 10,
        pageNumber: 1,
        pageList: [2, 10, 15, 20, 30, 50],
        toolbar: '#searchWorkStationToolBar',
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
            { title: '工位排序', width: 100, field: 'fsequence', },
            {
                title: '更新时间', width: 100, field: 'chtime', formatter: function (value, row) {
                    return toDateStr(value);
                }
            },
            {
                title: '状态', width: 100, field: 'status', formatter: function (value, row) {
                    return value==1?'激活':'失效';
                }},
        ]],
    });
}

//清楚dialog中的选项
function clearDGWorkStation() {
    $('#dgWorkStationId').textbox('setValue', '');
    $('#dgWorkStationCode').textbox('setValue', '');
    $('#dgWorkStationName').textbox('setValue', '');
    $('#dgWorkStationStatus').combobox('setValue', '1');
    $('#dgWorkStationSequence').textbox('setValue', '');
    $('.errorMsg').text('');
}


//初始化点击按钮、更改下拉框事件
function initWorkStationClick() {
    //点击Search按钮
    $('#btnWorkStationSearch').click(function () {
        reloadWorkStationTable();//重新加载表格
    });
    //点击清除
    $('#btnWorkStationClear').click(function () {
        $('#searchWorkStationCode').textbox('setValue', '');
        $('#searchWorkStationName').textbox('setValue', '');
        $('#searchWorkStationStatus').combobox('setValue', '-1');
        $('#searchWorkStationModel').combobox('setValue', '-1');
        $('#searchWorkStationSequence').textbox('setValue', '');
    });
    //点击新增
    $('#btnWorkStationInsert').click(function () {
        clearDGWorkStation();
        tableClickInsert('#WorkStationDialog', '新增');
    });
    //点击修改
    $('#btnWorkStationUpdate').click(function () {
        var rows = $('#WorkStationTable').datagrid('getSelections');
        if (rows.length <= 0) { show('请选择任意一行！'); return; }
        clearDGWorkStation();
        $('#WorkStationDialog').dialog({
            closed: false,
            title: '修改',
        });
        $('#dgWorkStationId').textbox('setValue', rows[0].id);
        $('#dgWorkStationCode').textbox('setValue', rows[0].code);
        $('#dgWorkStationName').textbox('setValue', rows[0].name);
        $('#dgWorkStationStatus').combobox('setValue', rows[0].status);
        $('#dgWorkStationModel').combobox('setValue', rows[0].modelid);
        $('#dgWorkStationSequence').textbox('setValue', rows[0].fsequence);
    });
    
}

//重新加载表格
function reloadWorkStationTable() {
    $('#WorkStationTable').datagrid('load', {
        code: $('#searchWorkStationCode').textbox('getValue'),
        name: $('#searchWorkStationName').textbox('getValue'),
        status: $('#searchWorkStationStatus').combobox('getValue'),
        modelid: $('#searchWorkStationModel').combobox('getValue'),
        fsequence: $('#searchWorkStationSequence').textbox('getValue'),
    });
}