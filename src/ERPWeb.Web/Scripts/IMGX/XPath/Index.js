//窗体加载
$(function () {
    initXPathCombobox();
    initXPathTable();
    initXPathDialog();
    initXPathClick();
    
});

//验证输入-点击提交时
function checkXPathClickSubmit(idList) {
    $(idList).each(function (index, element) {
        checkXPathAttention($(this));
    });
}
//验证输入-更改提示信息
function checkXPathAttention(element) {
    var id = $(element).attr('id');
    var value = $(element).textbox('getValue');
    var errorMsg = $(element).parents('tr').find('.errorMsg');
    if (id == 'dgXPathXPath') {
        if (checkValLenString(value, 1, 64).code == 0) errorMsg.text('编号必须在1-64个字符之间'); else errorMsg.text('');
    } else if (id == 'dgXPathToPath') {
        if (checkValLenString(value, 1, 64).code == 0) errorMsg.text('名称必须在1-64个字符之间'); else errorMsg.text('');
    } else if (id == 'dgXPathMaxnum') {
        if (checkValLenString(value, 1, 1024).code == 0) errorMsg.text('备注必须满足1-1024个字节之间'); else errorMsg.text('');
    } 
}
var comboboxJson = [{}];
//初始化下拉框
function initXPathCombobox() {
    comboboxJson = ajaxSame('../../Model/GetModelCombobox', null, 'post', 'json');
    var isValidJson = [{ name: '--全部--', id: '-1' }, { name: '失效', id: '0' }, { name: '激活', id: '1' }];
    var isValidJsonOnly = [{ name: '失效', id: '0' }, { name: '激活', id: '1' }];
    loadDropDownListAll('#dgXPathModel', comboboxJson, 'id', 'name', '-1');
    loadDropDownListAll('#searchXPathIsValid', isValidJson, 'id', 'name', '-1');
    loadDropDownListAll('#dgXPathIsValid', isValidJsonOnly, 'id', 'name', '1');
    
}
//初始化提示框
function initXPathDialog() {
    $('#XPathDialog').dialog({
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
                if (!IsSubmitDialog('#XPathDialog')) { return; }

                var id = $('#dgXPathId').textbox('getValue');
                var macid = $('#dgXPathMacId').val();
                var xpath = $('#dgXPathXPath').textbox('getValue');
                var topath = $('#dgXPathToPath').textbox('getValue');
                var extend = $('#dgXPathExtend').textbox('getValue');
                var rate = $('#dgXPathRate').textbox('getValue');
                var userno = $('#dgXPathUserNo').textbox('getValue');
                var isvalid = $('#dgXPathIsValid').combobox('getValue');

                var data = { Id: id, xpath: xpath, topath: topath, extend: extend, userno: userno, macid: macid, rate: rate, isvalid: isvalid };
                var url = $('#XPathDialog').panel('options').title == '新增' ? '../../XPath/InsertXPath' : '../../XPath/UpdateXPath';
                var result = dialogClickKeep(url, data, 'post', 'json');
                if (typeof (result) == 'string') { result = $.parseJSON(result); }
                if (result.Code == 200) {
                    clearDGXPath();
                    $('#XPathDialog').dialog({ closed: true, });
                    reloadXPathTable();//重新加载表格
                } else {
                    show(result.Message);
                }
            }
        }, {
            text: '取消',
            iconCls: 'icon-cancel',
            handler: function () {
                clearDGXPath();
                $('#XPathDialog').dialog({ closed: true, });
            }
        }],
    });

    $('#searchXPathMac').dialog({
        title:'选择机台',
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
                var rows = $('.easyui-datagrid[title="SearchMac"]').datagrid('getSelections');
                debugger;
                if (rows.length != 1) { show('请选择一行！'); return; }
                var IsDialog = $('#XPathDialog').is(":visible");
                if (IsDialog) {
                    $('#dgXPathMacId').val(rows[0].id);
                    $('#dgXPathMacName').text(rows[0].name);
                } else {
                    $('#searchXPathMacId').val(rows[0].id);
                    $('#searchXPathMacName').text(rows[0].name);
                }
                $('#searchXPathMac').dialog({ closed: true, });
            }
        }, {
            text: '取消',
            iconCls: 'icon-cancel',
            handler: function () {
                $('#searchXPathMac').dialog({ closed: true, });
            }
        }],
    });
    
}

//初始化表格
function initXPathTable() {
    var xpath = $('#searchXPathXPath').textbox('getValue');
    var topath = $('#searchXPathToPath').textbox('getValue');
    var extend = $('#searchXPathExtend').textbox('getValue');
    var userno = $('#searchXPathUserNo').textbox('getValue');
    var macid = $('#searchXPathMacId').val();
    var ratemin = $('#searchXPathRateMin').textbox('getValue');
    var ratemax = $('#searchXPathRateMax').textbox('getValue');
    var isvalid = $('#searchXPathIsValid').combobox('getValue');

    if (ratemin == '') ratemin = 1;
    if (ratemax == '') ratemax = 99999;
    if (parseInt(ratemin) > parseInt(ratemax)) {
        show('频率异常，请重新输入'); return;
    }

    $('#XPathTable').datagrid({
        //分页
        fitColumns: true,
        singleSelect: true,
        url: '../../XPath/GetXPath',
        queryParams: { xpath: xpath, topath: topath, extend: extend, userno: userno, macid: macid, ratemin: ratemin, ratemax: ratemax, isvalid: isvalid },
        pagination: true,
        rownumbers: true,
        pagePosition: 'bottom',
        pageSize: 10,
        pageNumber: 1,
        pageList: [2, 10, 15, 20, 30, 50],
        toolbar: '#searchXPathToolBar',
        fitColumns: true,
        //数据格式
        columns: [[
            { title: '序号', width: 50, field: 'id', fixed: true,/*hidden: true*/ },
            { title: '机台序号', width: 50, field: 'macid', },
            { title: '机台名称', width: 100, field: 'macname', },
            { title: '机台IP4', width: 100, field: 'macip4', },
            { title: 'XPath', width: 100, field: 'xpath', },
            { title: 'ToPath', width: 100, field: 'topath', },
            { title: '拓展', width: 100, field: 'extend', },
            { title: '频率', width: 100, field: 'rate', },
            { title: '工号', width: 100, field: 'userno', },
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
function clearDGXPath() {
    $('#dgXPathId').textbox('setValue', '');
    $('#dgXPathMacId').val('');
    $('#dgXPathMacName').text('');
    $('#dgXPathXPath').textbox('setValue', '');
    $('#dgXPathToPath').textbox('setValue', '');
    $('#dgXPathExtend').textbox('setValue', '');
    $('#dgXPathRate').textbox('setValue', '');
    $('#dgXPathUserNo').textbox('setValue', '');
    $('#dgXPathIsValid').combobox('setValue','1');
    $('.errorMsg').text('');
}


//初始化点击按钮、更改下拉框事件
function initXPathClick() {
    //点击Search按钮
    $('#btnXPathSearch').click(function () {
        reloadXPathTable();//重新加载表格
    });
    //点击清除
    $('#btnXPathClear').click(function () {
        $('#searchXPathXPath').textbox('setValue','');
        $('#searchXPathToPath').textbox('setValue', '');
        $('#searchXPathExtend').textbox('setValue', '');
        $('#searchXPathUserNo').textbox('setValue', '');
        $('#searchXPathMacId').val('');
        $('#searchXPathMacName').text('');
        $('#searchXPathRateMin').textbox('setValue', '');
        $('#searchXPathRateMax').textbox('setValue', '');
        $('#searchXPathIsValid').combobox('setValue', '-1');
    });
    //点击新增
    $('#btnXPathInsert').click(function () {
        clearDGXPath();
        tableClickInsert('#XPathDialog', '新增');
    });
    //点击修改
    $('#btnXPathUpdate').click(function () {
        var rows = $('#XPathTable').datagrid('getSelections');
        if (rows.length <= 0) { show('请选择任意一行！'); return; }
        clearDGXPath();
        $('#dgXPathId').textbox('setValue', rows[0].id);
        $('#dgXPathMacId').val(rows[0].macid);
        $('#dgXPathMacName').text(rows[0].macname);
        $('#dgXPathXPath').textbox('setValue', rows[0].xpath);
        $('#dgXPathToPath').textbox('setValue', rows[0].topath);
        $('#dgXPathExtend').textbox('setValue', rows[0].extend);
        $('#dgXPathRate').textbox('setValue', rows[0].rate);
        $('#dgXPathUserNo').textbox('setValue', rows[0].userno);
        $('#dgXPathIsValid').combobox('setValue', rows[0].isvalid);
        
        $('#XPathDialog').dialog({
            closed: false,
            title: '修改',
        });
        
    });
    
}


//重新加载表格
function reloadXPathTable() {
    var xpath = $('#searchXPathXPath').textbox('getValue');
    var topath = $('#searchXPathToPath').textbox('getValue');
    var extend = $('#searchXPathExtend').textbox('getValue');
    var userno = $('#searchXPathUserNo').textbox('getValue');
    var macid = $('#searchXPathMacId').val();
    var ratemin = $('#searchXPathRateMin').textbox('getValue');
    var ratemax = $('#searchXPathRateMax').textbox('getValue');
    var isvalid = $('#searchXPathIsValid').combobox('getValue');

    if (ratemin == '') ratemin = 1;
    if (ratemax == '') ratemax = 99999;
    if (parseInt(ratemin) > parseInt(ratemax)) {
        show('频率异常，请重新输入'); return;
    }
    $('#XPathTable').datagrid('load', {
        xpath: xpath, topath: topath, extend: extend, userno: userno, macid: macid, ratemin: ratemin, ratemax: ratemax, isvalid: isvalid
    });
}


function selMac() {
    $.ajax({
        url: '../../Mac/Search',
        data: null,
        type: 'post',
        dataType: 'html',
        async: false,//非异步
        success: function (data) {
            $('#searchXPathMac').empty();
            $('#searchXPathMac').append(data);
            $('#searchXPathMac').dialog({
                closed: false,//默认关闭状态
                closable: false,//不提供关闭页面
            });
        },
        error: function (data) {
            show(data);
        },
    });
}