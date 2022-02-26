//窗体加载
$(function () {
    initReadSettingCombobox();
    initReadSettingTable();
    initReadSettingDialog();
    initReadSettingClick();
    
});

//验证输入-点击提交时
function checkReadSettingClickSubmit(idList) {
    $(idList).each(function (index, element) {
        checkReadSettingAttention($(this));
    });
}
//验证输入-更改提示信息
function checkReadSettingAttention(element) {
    var id = $(element).attr('id');
    var value = $(element).textbox('getValue');
    var errorMsg = $(element).parents('tr').find('.errorMsg');
    if (id == 'dgReadSettingReadSetting') {
        if (checkValLenString(value, 1, 64).code == 0) errorMsg.text('编号必须在1-64个字符之间'); else errorMsg.text('');
    } else if (id == 'dgReadSettingToPath') {
        if (checkValLenString(value, 1, 64).code == 0) errorMsg.text('名称必须在1-64个字符之间'); else errorMsg.text('');
    } else if (id == 'dgReadSettingMaxnum') {
        if (checkValLenString(value, 1, 1024).code == 0) errorMsg.text('备注必须满足1-1024个字节之间'); else errorMsg.text('');
    } 
}
var comboboxJson = [{}];
//初始化下拉框
function initReadSettingCombobox() {
    var isValidJson = [{ name: '--全部--', id: '-1' }, { name: '失效', id: '0' }, { name: '激活', id: '1' }];
    var isValidJsonOnly = [{ name: '失效', id: '0' }, { name: '激活', id: '1' }];
    
}
//初始化提示框
function initReadSettingDialog() {
    $('#ReadSettingDialog').dialog({
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
                if (!IsSubmitDialog('#ReadSettingDialog')) { return; }

                var id = $('#dgReadSettingId').textbox('getValue');
                var consoleid = $('#dgReadSettingConsoleId').val();
                var xpathid = $('#dgReadSettingXPathId').val();

                var data = { Id: id, consoleid: consoleid, xpathid: xpathid};
                var url = $('#ReadSettingDialog').panel('options').title == '新增' ? '../../ReadSetting/InsertReadSetting' : '../../ReadSetting/UpdateReadSetting';
                var result = dialogClickKeep(url, data, 'post', 'json');
                if (typeof (result) == 'string') { result = $.parseJSON(result); }
                if (result.Code == 200) {
                    clearDGReadSetting();
                    $('#ReadSettingDialog').dialog({ closed: true, });
                    reloadReadSettingTable();//重新加载表格
                } else {
                    show(result.Message);
                }
            }
        }, {
            text: '取消',
            iconCls: 'icon-cancel',
            handler: function () {
                clearDGReadSetting();
                $('#ReadSettingDialog').dialog({ closed: true, });
            }
        }],
    });

    $('#searchReadSettingConsole').dialog({
        title:'选择控制台',
        closed: true,//默认关闭状态
        closable: false,//不提供关闭页面
        top: 0,
        width:870,
        iconCls: 'icon-more',
        resizable: false,//是否可改变窗口大小
        modal: true,//模式化窗口，打开其他地方不可点击
        buttons: [{
            text: '确定',
            iconCls: 'icon-ok',
            handler: function () {
                var rows = $('.easyui-datagrid[title="SearchConsole"]').datagrid('getSelections');
                if (rows.length <= 0) { show('请选择任意一行！'); return; }
                var IsDialog = $('#ReadSettingDialog').is(":visible");
                if (IsDialog) {
                    $('#dgReadSettingConsoleId').val(rows[0].id);
                    $('#dgReadSettingConsoleName').text(rows[0].name);
                } else {
                    $('#searchReadSettingConsoleId').val(rows[0].id);
                    $('#searchReadSettingConsoleName').text(rows[0].name);
                }
                $('#searchReadSettingConsole').dialog({ closed: true, });
            }
        }, {
            text: '取消',
            iconCls: 'icon-cancel',
            handler: function () {
                $('#searchReadSettingConsole').dialog({ closed: true, });
            }
        }],
    });
    $('#searchReadSettingXPath').dialog({
        title: '选择XPath',
        closed: true,//默认关闭状态
        closable: false,//不提供关闭页面
        top: 0,
        width: 930,
        iconCls: 'icon-more',
        resizable: false,//是否可改变窗口大小
        modal: true,//模式化窗口，打开其他地方不可点击
        buttons: [{
            text: '确定',
            iconCls: 'icon-ok',
            handler: function () {
                var rows = $('.easyui-datagrid[title="SearchXPath"]').datagrid('getSelections');
                if (rows.length <= 0) { show('请选择任意一行！'); return; }
                var IsDialog = $('#ReadSettingDialog').is(":visible");
                if (IsDialog) {
                    $('#dgReadSettingXPathId').val(rows[0].id);
                    $('#dgReadSettingXPathName').text(rows[0].name);
                } else {
                    $('#searchReadSettingXPathId').val(rows[0].id);
                    $('#searchReadSettingXPathName').text(rows[0].name);
                }
                $('#searchReadSettingXPath').dialog({ closed: true, });
            }
        }, {
            text: '取消',
            iconCls: 'icon-cancel',
            handler: function () {
                $('#searchReadSettingXPath').dialog({ closed: true, });
            }
        }],
    });
    
}

//初始化表格
function initReadSettingTable() {
    var consoleid = 0;
    var xpathid = 0;
    
    $('#ReadSettingTable').datagrid({
        //分页
        fitColumns: true,
        singleSelect: true,
        url: '../../ReadSetting/GetReadSetting',
        queryParams: { consoleid: consoleid, xpathid: xpathid },
        pagination: true,
        rownumbers: true,
        pagePosition: 'bottom',
        pageSize: 10,
        pageNumber: 1,
        pageList: [2, 10, 15, 20, 30, 50],
        toolbar: '#searchReadSettingToolBar',
        fitColumns: true,
        //数据格式
        columns: [[
            { title: '序号', width: 50, field: 'id', fixed: true,/*hidden: true*/ },
            { title: '控制台Id', width: 50, field: 'consoleid', },
            { title: '控制台名', width: 50, field: 'consolename', },
            { title: '控制台IP', width: 50, field: 'consoleip4', },
            { title: 'XPathId', width: 50, field: 'xpathid', },
            { title: 'XPath', width: 120, field: 'xpathxpath', },
            { title: 'ToPath', width: 120, field: 'xpathtopath', },
            { title: '拓展', width: 80, field: 'xpathextend', },
            { title: '频率', width: 50, field: 'xpathrate', },
            {
                title: '更新时间', width: 180, field: 'chtime', formatter: function (value, row) {
                    return toDateStr(value);
                }
            },
        ]],
    });
}

//清楚dialog中的选项
function clearDGReadSetting() {
    $('#dgReadSettingId').textbox('setValue', '');
    $('#dgReadSettingConsoleId').val('');
    $('#dgReadSettingConsoleName').text('');
    $('#dgReadSettingXPathId').val('');
    $('#dgReadSettingXPathName').text('');
    $('.errorMsg').text('');
}

//初始化点击按钮、更改下拉框事件
function initReadSettingClick() {
    //点击Search按钮
    $('#btnReadSettingSearch').click(function () {
        reloadReadSettingTable();//重新加载表格
    });
    //点击清除
    $('#btnReadSettingClear').click(function () {
        $('#searchReadSettingConsoleId').val('');
        $('#searchReadSettingConsoleName').text('');
        $('#searchReadSettingXPathId').val('');
        $('#searchReadSettingXPathName').text('');
    });
    //点击新增
    $('#btnReadSettingInsert').click(function () {
        clearDGReadSetting();
        tableClickInsert('#ReadSettingDialog', '新增');
    });
    //点击修改
    $('#btnReadSettingUpdate').click(function () {
        var rows = $('#ReadSettingTable').datagrid('getSelections');
        if (rows.length <= 0) { show('请选择任意一行！'); return; }
        clearDGReadSetting();
        $('#dgReadSettingId').textbox('setValue', rows[0].id);
        $('#dgReadSettingConsoleId').val(rows[0].consoleid);
        $('#dgReadSettingConsoleName').text(rows[0].consolename);
        $('#dgReadSettingXPathId').val(rows[0].xpathid);
        $('#dgReadSettingXPathName').text(rows[0].xpathextend + '-' + rows[0].xpathrate);
        
        $('#ReadSettingDialog').dialog({
            closed: false,
            title: '修改',
        });
        
    });
}

//重新加载表格
function reloadReadSettingTable() {
    var consoleid = $('#searchReadSettingConsoleId').val();
    var xpathid = $('#searchReadSettingXPathId').val();
    
    $('#ReadSettingTable').datagrid('load', {
        consoleid: consoleid, xpathid: xpathid
    });
}

function selConsole() {
    $.ajax({
        url: '../../Console/Search',
        data: null,
        type: 'post',
        dataType: 'html',
        async: false,//非异步
        success: function (data) {
            $('#searchReadSettingConsole').empty();
            $('#searchReadSettingConsole').append(data);
            $('#searchReadSettingConsole').dialog({
                closed: false,//默认关闭状态
                closable: false,//不提供关闭页面
            });
        },
        error: function (data) {
            show(data);
        },
    });
}
function selXPath() {
    $.ajax({
        url: '../../XPath/Search',
        data: null,
        type: 'post',
        dataType: 'html',
        async: false,//非异步
        success: function (data) {
            $('#searchReadSettingXPath').empty();
            $('#searchReadSettingXPath').append(data);
            $('#searchReadSettingXPath').dialog({
                closed: false,//默认关闭状态
                closable: false,//不提供关闭页面
            });
        },
        error: function (data) {
            show(data);
        },
    });
}