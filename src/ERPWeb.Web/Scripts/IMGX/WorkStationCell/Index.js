//窗体加载
$(function () {
    initWorkStationCellCombobox();
    initWorkStationCellTable();
    initWorkStationCellDialog();
    initWorkStationCellClick();
    
});

//验证输入-点击提交时
function checkWorkStationCellClickSubmit(idList) {
    $(idList).each(function (index, element) {
        checkWorkStationCellAttention($(this));
    });
}
//验证输入-更改提示信息
function checkWorkStationCellAttention(element) {
    var id = $(element).attr('id');
    var value = $(element).textbox('getValue');
    var errorMsg = $(element).parents('tr').find('.errorMsg');
    if (id == 'dgWorkStationCellWorkStationCell') {
        if (checkValLenString(value, 1, 64).code == 0) errorMsg.text('编号必须在1-64个字符之间'); else errorMsg.text('');
    } else if (id == 'dgWorkStationCellToPath') {
        if (checkValLenString(value, 1, 64).code == 0) errorMsg.text('名称必须在1-64个字符之间'); else errorMsg.text('');
    } else if (id == 'dgWorkStationCellMaxnum') {
        if (checkValLenString(value, 1, 1024).code == 0) errorMsg.text('备注必须满足1-1024个字节之间'); else errorMsg.text('');
    } 
}
var comboboxJson = [{}];
//初始化下拉框
function initWorkStationCellCombobox() {
    var isValidJson = [{ name: '--全部--', id: '-1' }, { name: '失效', id: '0' }, { name: '激活', id: '1' }];
    var isValidJsonOnly = [{ name: '失效', id: '0' }, { name: '激活', id: '1' }];
    loadDropDownListAll('#searchWorkStationCellIsValid', isValidJson, 'id', 'name', '-1');
    loadDropDownListAll('#dgWorkStationCellIsValid', isValidJsonOnly, 'id', 'name', '1');
    
}
//初始化提示框
function initWorkStationCellDialog() {
    $('#WorkStationCellDialog').dialog({
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
                if (!IsSubmitDialog('#WorkStationCellDialog')) { return; }

                var id = $('#dgWorkStationCellId').textbox('getValue');
                var wsid = $('#dgWorkStationCellWSId').val();
                var cellid = $('#dgWorkStationCellCellId').val();
                var isvalid = $('#dgWorkStationCellIsValid').combobox('getValue');

                var data = { Id: id, wsid: wsid, cellid: cellid, isvalid: isvalid };
                var url = $('#WorkStationCellDialog').panel('options').title == '新增' ? '../../WorkStationCell/InsertWorkStationCell' : '../../WorkStationCell/UpdateWorkStationCell';
                var result = dialogClickKeep(url, data, 'post', 'json');
                if (typeof (result) == 'string') { result = $.parseJSON(result); }
                if (result.Code == 200) {
                    clearDGWorkStationCell();
                    $('#WorkStationCellDialog').dialog({ closed: true, });
                    reloadWorkStationCellTable();//重新加载表格
                } else {
                    show(result.Message);
                }
            }
        }, {
            text: '取消',
            iconCls: 'icon-cancel',
            handler: function () {
                clearDGWorkStationCell();
                $('#WorkStationCellDialog').dialog({ closed: true, });
            }
        }],
    });

    $('#searchWorkStationCellWS').dialog({
        title:'选择工位',
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
                var rows = $('.easyui-datagrid[title="SearchWorkStation"]').datagrid('getSelections');
                if (rows.length != 1) { show('请选择【一行】！'); return; }
                var IsDialog = $('#WorkStationCellDialog').is(":visible");
                if (IsDialog) {
                    $('#dgWorkStationCellWSId').val(rows[0].id);
                    $('#dgWorkStationCellWSName').text(rows[0].name);
                } else {
                    $('#searchWorkStationCellWSId').val(rows[0].id);
                    $('#searchWorkStationCellWSName').text(rows[0].name);
                }
                $('#searchWorkStationCellWS').dialog({ closed: true, });
            }
        }, {
            text: '取消',
            iconCls: 'icon-cancel',
            handler: function () {
                $('#searchWorkStationCellWS').dialog({ closed: true, });
            }
        }],
    });
    $('#searchWorkStationCellCell').dialog({
        title: '选择先别',
        closed: true,//默认关闭状态
        closable: false,//不提供关闭页面
        top: 0,
        width: 530,
        iconCls: 'icon-more',
        resizable: false,//是否可改变窗口大小
        modal: true,//模式化窗口，打开其他地方不可点击
        buttons: [{
            text: '确定',
            iconCls: 'icon-ok',
            handler: function () {
                var rows = $('.easyui-datagrid[title="SearchCell"]').datagrid('getSelections');
                if (rows.length <= 0) { show('请选择任意一行！'); return; }
                var IsDialog = $('#WorkStationCellDialog').is(":visible");
                if (IsDialog) {
                    $('#dgWorkStationCellCellId').val(rows[0].id);
                    $('#dgWorkStationCellCellName').text(rows[0].name);
                } else {
                    $('#searchWorkStationCellCellId').val(rows[0].id);
                    $('#searchWorkStationCellCellName').text(rows[0].name);
                }
                $('#searchWorkStationCellCell').dialog({ closed: true, });
            }
        }, {
            text: '取消',
            iconCls: 'icon-cancel',
            handler: function () {
                $('#searchWorkStationCellCell').dialog({ closed: true, });
            }
        }],
    });
    
}

//初始化表格
function initWorkStationCellTable() {
    var wsid = $('#searchWorkStationCellWSId').val();
    var cellid = $('#searchWorkStationCellCellId').val();
    var isvalid = $('#searchWorkStationCellIsValid').combobox('getValue');
    
    $('#WorkStationCellTable').datagrid({
        //分页
        fitColumns: true,
        singleSelect: true,
        url: '../../WorkStationCell/GetWorkStationCell',
        queryParams: { wsid: wsid, cellid: cellid, isvalid: isvalid },
        pagination: true,
        rownumbers: true,
        pagePosition: 'bottom',
        pageSize: 10,
        pageNumber: 1,
        pageList: [2, 10, 15, 20, 30, 50],
        toolbar: '#searchWorkStationCellToolBar',
        fitColumns: true,
        //数据格式
        columns: [[
            { title: '序号', width: 50, field: 'id', fixed: true,/*hidden: true*/ },
            { title: '工位序号', width: 50, field: 'wsid', },
            { title: '工位编号', width: 100, field: 'wscode', },
            { title: '工位名称', width: 100, field: 'wsname', },
            { title: '线别序号', width: 50, field: 'cellid', },
            { title: '线别编号', width: 100, field: 'cellcode', },
            { title: '线别名称', width: 100, field: 'cellname', },
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
function clearDGWorkStationCell() {
    $('#dgWorkStationCellWSId').val('');
    $('#dgWorkStationCellWSName').text('');
    $('#dgWorkStationCellCellId').val('');
    $('#dgWorkStationCellCellName').text('');
    $('#dgWorkStationCellIsValid').combobox('setValue', '1');
    $('.errorMsg').text('');
}

//初始化点击按钮、更改下拉框事件
function initWorkStationCellClick() {
    //点击Search按钮
    $('#btnWorkStationCellSearch').click(function () {
        reloadWorkStationCellTable();//重新加载表格
    });
    //点击清除
    $('#btnWorkStationCellClear').click(function () {
        $('#searchWorkStationCellWSId').val('');
        $('#searchWorkStationCellWSName').text('');
        $('#searchWorkStationCellCellId').val('');
        $('#searchWorkStationCellCellName').text('');
        $('#searchWorkStationCellIsValid').combobox('setValue', '-1');
    });
    //点击新增
    $('#btnWorkStationCellInsert').click(function () {
        clearDGWorkStationCell();
        tableClickInsert('#WorkStationCellDialog', '新增');
    });
    //点击修改
    $('#btnWorkStationCellUpdate').click(function () {
        var rows = $('#WorkStationCellTable').datagrid('getSelections');
        if (rows.length <= 0) { show('请选择任意一行！'); return; }
        clearDGWorkStationCell();
        $('#dgWorkStationCellId').textbox('setValue', rows[0].id);
        $('#dgWorkStationCellWSId').val(rows[0].wsid);
        $('#dgWorkStationCellWSName').text(rows[0].wsname);
        $('#dgWorkStationCellCellId').val(rows[0].cellid);
        $('#dgWorkStationCellCellName').text(rows[0].cellname);
        $('#dgWorkStationCellIsValid').combobox('setValue', rows[0].isvalid);
        
        $('#WorkStationCellDialog').dialog({
            closed: false,
            title: '修改',
        });
        
    });
}

//重新加载表格
function reloadWorkStationCellTable() {
    var wsid = $('#searchWorkStationCellWSId').val();
    var cellid = $('#searchWorkStationCellCellId').val();
    var isvalid = $('#searchWorkStationCellIsValid').combobox('getValue');
    
    $('#WorkStationCellTable').datagrid('load', {
        wsid: wsid, cellid: cellid, isvalid: isvalid
    });
}

function selWS() {
    $.ajax({
        url: '../../WorkStation/Search',
        data: null,
        type: 'post',
        dataType: 'html',
        async: false,//非异步
        success: function (data) {
            $('#searchWorkStationCellWS').empty();
            $('#searchWorkStationCellWS').append(data);
            $('#searchWorkStationCellWS').dialog({
                closed: false,//默认关闭状态
                closable: false,//不提供关闭页面
            });
        },
        error: function (data) {
            show(data);
        },
    });
}
function selCell() {
    $.ajax({
        url: '../../Cell/Search',
        data: null,
        type: 'post',
        dataType: 'html',
        async: false,//非异步
        success: function (data) {
            $('#searchWorkStationCellCell').empty();
            $('#searchWorkStationCellCell').append(data);
            $('#searchWorkStationCellCell').dialog({
                closed: false,//默认关闭状态
                closable: false,//不提供关闭页面
            });
        },
        error: function (data) {
            show(data);
        },
    });
}