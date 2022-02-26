//窗体加载
$(function () {
    initRTDialog();
    initRTTable();
    initRTClick();
});

//验证输入-点击提交时
function checkRTClickSubmit(idList) {
    $(idList).each(function (index, element) {
        checkRTAttention($(this));
    });
}
//验证输入-更改提示信息
function checkRTAttention(element) {
    var id = $(element).attr('id');
    var value = $(element).textbox('getValue');
    var errorMsg = $(element).parents('tr').find('.errorMsg');
    if (id == 'dgRTCode') {
        if (checkValLenString(value, 1, 64).code == 0) errorMsg.text('编号必须在1-64个字符之间'); else errorMsg.text('');
    } else if (id == 'dgRTName') {
        if (checkValLenString(value, 1, 64).code == 0) errorMsg.text('名称必须在1-64个字符之间'); else errorMsg.text('');
    } else if (id == 'dgRTMaxnum') {
        if (checkValLenString(value, 1, 1024).code == 0) errorMsg.text('备注必须满足1-1024个字节之间'); else errorMsg.text('');
    }
}


//初始化提示框
function initRTDialog() {
    $('#RTDialog').dialog({
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
                checkRTClickSubmit('#dgRTCode,#dgRTName,#dgRTMaxnum');
                if (!IsSubmitDialog('#RTDialog')) { return; }

                var id = $('#dgRTId').textbox('getValue');
                var code = $('#dgRTCode').textbox('getValue');
                var name = $('#dgRTName').textbox('getValue');
                var maxnum = $('#dgRTMaxnum').textbox('getValue');
                var inputtype = $('#dgRTInputtype').combobox('getValue');
                var portlist = $('#dgPortIdList').val();

                var data = { Id: id, code: code, name: name, maxnum: maxnum, inputtype: inputtype, portlist: portlist };
                var url = $('#RTDialog').panel('options').title == '新增' ? '../../Repair/InsertRepairType' : '../../Repair/UpdateRepairType';
                var result = dialogClickKeep(url, data);
                if (result == 'YES') {
                    clearDGRT();
                    $('#RTDialog').dialog({ closed: true, });
                    reloadRTTable();//重新加载表格
                } else {
                    show(result);
                }
            }
        }, {
            text: '取消',
            iconCls: 'icon-cancel',
            handler: function () {
                clearDGRT();
                $('#RTDialog').dialog({ closed: true, });
            }
        }],
    });
}

//初始化表格
function initRTTable() {
    var code = $('#searchRTCode').textbox('getValue');
    var name = $('#searchRTName').textbox('getValue');
    var inputtype = $('#searchRTInputtype').textbox('getValue');
    $('#RTTable').datagrid({
        //分页
        fitColumns: true,
        singleSelect: true,
        url: '../../Repair/GetRepairType',
        queryParams: { code: code, name: name, inputtype: inputtype },
        pagination: true,
        rownumbers: true,
        pagePosition: 'bottom',
        pageSize: 10,
        pageNumber: 1,
        pageList: [2, 10, 15, 20, 30, 50],
        toolbar: '#searchRTToolBar',
        fitColumns: true,
        //数据格式
        columns: [[
            { title: '序号', width: 100, field: 'id', hidden: true },
            { title: '编号', width: 100, field: 'code', },
            { title: '名称', width: 100, field: 'name', },
            { title: '限额', width: 100, field: 'maxnum', },
            { title: '投入方式', width: 100, field: 'inputtype', },
            { title: '集合', width: 100, field: 'portlist', hidden: true},
            { title: '部品组成', width: 200, field: 'portnamelist'},
        ]],
    });
}
//加载所有Port
function loadPortDgTable() {
    $('#dgPortTable').datagrid({
        url: '../../Repair/GetRepairPort',
        idField: "unitno",
        pagination: true,
        rownumbers: true,
        pagePosition: 'bottom',
        pageSize: 5,
        pageNumber: 1,
        pageList: [5, 10, 15, 20, 30, 50],
        singleSelect: false,
        columns: [[
            { checkbox: true, field: 'checkbox', },
            { title: '部品番号', width: 100, field: 'unitno', },
            { title: '部品名称', width: 100, field: 'name', },
            { title: '适应机种', width: 200, field: 'machinecodes', },
        ]],
        onLoadSuccess: function () {
            var selectedList = $('#dgPortIdList').val();
            var powerArray = selectedList.split(',');
            for (var i = 0; i < powerArray.length; i++) {
                if (powerArray[i] == '') continue;
                $(this).datagrid('selectRecord', powerArray[i]);
            }
        },
        onCheck: function (index, row) {
            var selectedList = $('#dgPortIdList').val();
            var oldStr = ',' + row.unitno;
            if (isContainsArr(selectedList, oldStr,',')) return;//当包含字串时，不再新增
            selectedList = selectedList + oldStr;
            $('#dgPortIdList').val(selectedList);
        },
        onUncheck: function (index, row) {
            var selectedList = $('#dgPortIdList').val();
            var oldStr = ',' + row.unitno;
            //包含字串才替换
            if (isContainsArr(selectedList, oldStr, ',')) selectedList = arrStrRemoveStr(selectedList, oldStr, ',');
            $('#dgPortIdList').val(selectedList);
        },
        onSelectAll: function (rows) {
            //用户选中所有的行
            var selectedList = $('#dgPortIdList').val();
            var oldStr = '';
            for (var i in rows) {
                oldStr = ',' + rows[i].unitno;
                if (isContainsArr(selectedList, oldStr,',')) continue;
                selectedList = selectedList + oldStr;
            }
            $('#dgPortIdList').val(selectedList);
        },
        onUnselectAll: function (rows) {
            //用户取消选中所有的行
            console.info(rows);
            var selectedList = $('#dgPortIdList').val();
            var oldStr = '';
            for (var i in rows) {
                oldStr = ',' + rows[i].unitno;
                if (isContainsArr(selectedList, oldStr, ',')) selectedList = arrStrRemoveStr(selectedList, oldStr, ',');
            }
            $('#dgPortIdList').val(selectedList);
        },
    });

}
//清楚dialog中的选项
function clearDGRT() {
    $('#dgRTId').textbox('setValue', '');
    $('#dgRTCode').textbox('setValue', '');
    $('#dgRTName').textbox('setValue', '');
    $('#dgRTMaxnum').textbox('setValue', '');
    $('#dgRTInputtype').combobox('setValue', '');
    $('#dgPortIdList').val('');
    $('#dgPortTable').datagrid('clearSelections');
    $('.errorMsg').text('');
}


//初始化点击按钮、更改下拉框事件
function initRTClick() {
    //点击Search按钮
    $('#btnRTSearch').click(function () {
        reloadRTTable();//重新加载表格
    });
    //点击清除
    $('#btnRTClear').click(function () {
        $('#searchRTCode').textbox('setValue', '');
        $('#searchRTName').textbox('setValue', '');
        $('#searchRTInputtype').combobox('setValue', '');
    });
    //点击新增
    $('#btnRTInsert').click(function () {
        clearDGRT();
        tableClickInsert('#RTDialog', '新增');
        loadPortDgTable();
    });
    //点击修改
    $('#btnRTUpdate').click(function () {
        var rows = $('#RTTable').datagrid('getSelections');
        if (rows.length <= 0) { show('请选择任意一行！'); return; }
        clearDGRT();
        var data = tableClickUpdateById(rows[0].Id, '../../Repair/SelectPortOne', '#RTDialog', '修改');
        $('#dgRTId').textbox('setValue', data.Id);
        $('#dgRTCode').textbox('setValue', data.code);
        $('#dgRTName').textbox('setValue', data.name);
        $('#dgRTMaxnum').textbox('setValue', data.maxnum);
        $('#dgRTInputtype').combobox('setValue', data.inputtype);
        $('#dgPortIdList').val(data.portlist);
        loadPortDgTable();
    });
    //点击删除
    $('#btnRTDelete').click(function () {
        var rows = $('#RTTable').datagrid('getSelections');
        if (rows.length <= 0) { show('请选择任意一行！'); return; }
        var result = tableClickDeleteById(rows[0].Id, '../../Repair/DeleteRepairType');
        if (result == 'YES') {
            clearDGRT();
            $('#RTDialog').dialog({ closed: true, });
            reloadRTTable();//重新加载表格
        } else {
            show(result);
        }
    });
}

//重新加载表格
function reloadRTTable() {
    $('#RTTable').datagrid('load', {
        code: $('#searchRTCode').textbox('getValue'),
        name: $('#searchRTName').textbox('getValue'),
        inputtype: $('#searchRTInputtype').combobox('getValue'),
    });
}