//窗体加载
$(function () {
    initSMDialog();
    initSMTable();
    initSMClick();
});

//初始化提示框
function initSMDialog() {
    $('#SMCDialog').dialog({
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
                
                var submodel = $('#dgSubmodel').combobox('getValue');
                var tbname = $('#dgTBName').textbox('getValue');
                

                var data = { submodel: submodel, tbname: tbname };
                console.info(data);
                var url = '../../Machine/PutMacLineSub';

                var result = dialogClickKeep(url, data);

                if (typeof (result) == 'string') {
                    result = $.parseJSON(result);
                }
                if (result.code == 1) {
                    clearDGSM();
                    $('#SMCDialog').dialog({ closed: true, });
                    reloadSMTable();//重新加载表格
                } else {
                    show(result.msg);
                }
            }
        }, {
            text: '取消',
            iconCls: 'icon-cancel',
            handler: function () {
                clearDGSM();
                $('#SMCDialog').dialog({ closed: true, });
            }
        }],
    });
}

//初始化表格
function initSMTable() {
    $('#SMCTable').datagrid({
        //分页
        fitColumns: true,
        singleSelect: true,
        url: '../../Machine/GetLineSubmodel',
        //queryParams: { PCName: name },
        pagination: true,
        rownumbers: true,
        pagePosition: 'bottom',
        pageSize: 10,
        pageNumber: 1,
        pageList: [2, 10, 15, 20, 30, 50],
        toolbar: '#searchSMCToolBar',
        fitColumns: true,
        //数据格式
        columns: [[
            { title: '线别', width: 100, field: 'Id', /*hidden: true*/ },
            { title: '小机种', width: 100, field: 'ModelID', },
            { title: '机种名称', width: 100, field: 'ModelName', },
            { title: '激活状态', width: 100, field: 'LatestFlag', },
            {
                title: '上次更改时间', width: 100, field: 'UpdateTime', formatter:
                    function (value, row, index) {
                        return DateTimeFormatter(value);
                    }
            },
            { title: '表名', width: 100, field: 'TBName', },
        ]],
    });
}


//清楚dialog中的选项
function clearDGSM() {
    $('#dgLine').textbox('setValue', '');
    $('#dgTBName').textbox('setValue', '');
    $('#dgSubmodel').combobox('select', '105');
    $('.errorMsg').text('');
}


//初始化点击按钮、更改下拉框事件
function initSMClick() {
    //点击Search按钮
    $('#btnSMSearch').click(function () {
        reloadSMTable();//重新加载表格
    });
    //点击修改
    $('#btnSMCUpdate').click(function () {
        var rows = $('#SMCTable').datagrid('getSelections');
        if (rows.length <= 0) { show('请选择任意一行！'); return; }
        clearDGSM();
        var data = tableClickUpdateById(rows[0].Id, '../../Machine/SelectLineSubOne', '#SMCDialog', '修改');
        $('#dgLine').textbox('setValue', data.Id);
        $('#dgSubmodel').combobox('select',data.ModelID);
        $('#dgTBName').textbox('setValue', data.TBName);
    });
}

//重新加载表格
function reloadSMTable() {
    $('#SMCTable').datagrid('load');
}


//格式化时间
function DateTimeFormatter(value) {
    if (value == undefined) {
        return "";
    }
    /*json格式时间转js时间格式*/
    value = value.substr(1, value.length - 2);
    var obj = eval('(' + "{Date: new " + value + "}" + ')');
    var dateValue = obj["Date"];
    if (dateValue.getFullYear() < 1900) {
        return "";
    }

    return dateValue.format("yyyy-MM-dd hh:mm:ss");
}