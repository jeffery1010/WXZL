//窗体加载
$(function () {
    $('#searchModelIsValid').combobox('setValue', '-1');
    initModelTable();
    initModelDialog();
    initModelClick();
    
});

//验证输入-点击提交时
function checkModelClickSubmit(idList) {
    $(idList).each(function (index, element) {
        checkModelAttention($(this));
    });
}
//验证输入-更改提示信息
function checkModelAttention(element) {
    var id = $(element).attr('id');
    var value = $(element).textbox('getValue');
    var errorMsg = $(element).parents('tr').find('.errorMsg');
    if (id == 'dgModelCode') {
        if (checkValLenString(value, 1, 64).code == 0) errorMsg.text('编号必须在1-64个字符之间'); else errorMsg.text('');
    } else if (id == 'dgModelName') {
        if (checkValLenString(value, 1, 64).code == 0) errorMsg.text('名称必须在1-64个字符之间'); else errorMsg.text('');
    } else if (id == 'dgModelMaxnum') {
        if (checkValLenString(value, 1, 1024).code == 0) errorMsg.text('备注必须满足1-1024个字节之间'); else errorMsg.text('');
    }
}


//初始化提示框
function initModelDialog() {
    $('#ModelDialog').dialog({
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
                checkModelClickSubmit('#dgModelCode,#dgModelName');
                if (!IsSubmitDialog('#ModelDialog')) { return; }

                var id = $('#dgModelId').textbox('getValue');
                var code = $('#dgModelCode').textbox('getValue');
                var name = $('#dgModelName').textbox('getValue');
                var isvalid = $('#dgModelIsValid').combobox('getValue');

                var data = { Id: id, code: code, name: name, isvalid: isvalid };
                var url = $('#ModelDialog').panel('options').title == '新增' ? '../../Model/InsertModel' : '../../Model/UpdateModel';
                var result = dialogClickKeep(url, data, 'post', 'json');
                if (typeof (result) == 'string') { result = $.parseJSON(result); }
                if (result.Code == 200) {
                    clearDGModel();
                    $('#ModelDialog').dialog({ closed: true, });
                    reloadModelTable();//重新加载表格
                } else {
                    show(result.Message);
                }
            }
        }, {
            text: '取消',
            iconCls: 'icon-cancel',
            handler: function () {
                clearDGModel();
                $('#ModelDialog').dialog({ closed: true, });
            }
        }],
    });
}

//初始化表格
function initModelTable() {
    var code = $('#searchModelCode').textbox('getValue');
    var name = $('#searchModelName').textbox('getValue');
    var isvalid = $('#searchModelIsValid').combobox('getValue');
    $('#ModelTable').datagrid({
        //分页
        fitColumns: true,
        singleSelect: true,
        url: '../../Model/GetModel',
        queryParams: { code: code, name: name, isvalid: isvalid },
        pagination: true,
        rownumbers: true,
        pagePosition: 'bottom',
        pageSize: 10,
        pageNumber: 1,
        pageList: [2, 10, 15, 20, 30, 50],
        toolbar: '#searchModelToolBar',
        fitColumns: true,
        //数据格式
        columns: [[
            { title: '序号', width: 100, field: 'id', fixed: true,/*hidden: true*/ },
            { title: '编号', width: 100, field: 'code', },
            { title: '名称', width: 100, field: 'name', },
            {
                title: '状态', width: 100, field: 'isvalid', formatter: function (value, row) {
                    return value==1?'激活':'失效';
                }},
        ]],
    });
}

//清楚dialog中的选项
function clearDGModel() {
    $('#dgModelId').textbox('setValue', '');
    $('#dgModelCode').textbox('setValue', '');
    $('#dgModelName').textbox('setValue', '');
    $('#dgModelIsValid').combobox('setValue', '1');
    $('.errorMsg').text('');
}


//初始化点击按钮、更改下拉框事件
function initModelClick() {
    //点击Search按钮
    $('#btnModelSearch').click(function () {
        reloadModelTable();//重新加载表格
    });
    //点击清除
    $('#btnModelClear').click(function () {
        $('#searchModelCode').textbox('setValue', '');
        $('#searchModelName').textbox('setValue', '');
        $('#searchModelIsValid').combobox('setValue', '-1');
    });
    //点击新增
    $('#btnModelInsert').click(function () {
        clearDGModel();
        tableClickInsert('#ModelDialog', '新增');
    });
    //点击修改
    $('#btnModelUpdate').click(function () {
        var rows = $('#ModelTable').datagrid('getSelections');
        if (rows.length <= 0) { show('请选择任意一行！'); return; }
        clearDGModel();
        $('#ModelDialog').dialog({
            closed: false,
            title: '修改',
        });
        $('#dgModelId').textbox('setValue', rows[0].id);
        $('#dgModelCode').textbox('setValue', rows[0].code);
        $('#dgModelName').textbox('setValue', rows[0].name);
        $('#dgModelIsValid').combobox('setValue', rows[0].isvalid);
    });
    
}

//重新加载表格
function reloadModelTable() {
    $('#ModelTable').datagrid('load', {
        code: $('#searchModelCode').textbox('getValue'),
        name: $('#searchModelName').textbox('getValue'),
        isvalid: $('#searchModelIsValid').combobox('getValue'),
    });
}