//窗体加载
$(function () {
    initSMDialog();
    initSMTable();
    initSMClick();
});

//初始化提示框
function initSMDialog() {
    $('#SMDialog').dialog({
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

                var id = $('#dgSMId').textbox('getValue');
                var portlist = $('#dgSubmodelIdList').val();

                var data = { pcid: id, newsubids: portlist };
                var url = '../../Machine/PutMacSubRelation';
                var result = ajaxSame(url, data, 'post', 'json');//dialogClickKeep(url, data);

                if (typeof (result) == 'string') {
                    result = $.parseJSON(result);
                }
                if (result.code == '1') {
                    clearDGSM();
                    $('#SMDialog').dialog({ closed: true, });
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
                $('#SMDialog').dialog({ closed: true, });
            }
        }],
    });
}

//初始化表格
function initSMTable() {
    var name = $('#searchSMName').textbox('getValue');
    $('#SMTable').datagrid({
        //分页
        fitColumns: true,
        singleSelect: true,
        url: '../../Machine/GetMachineSubmodel',
        queryParams: { PCName: name },
        pagination: true,
        rownumbers: true,
        pagePosition: 'bottom',
        pageSize: 10,
        pageNumber: 1,
        pageList: [2, 10, 15, 20, 30, 50],
        toolbar: '#searchSMToolBar',
        fitColumns: true,
        //数据格式
        columns: [[
            { title: '序号', width: 100, field: 'Id', /*hidden: true*/ },
            { title: '计算机名', width: 100, field: 'PCName', },
            { title: '适应机种序号', width: 100, field: 'SubIds', hidden: true,},
            { title: '适应机种编码', width: 100, field: 'SubCodes', },
            { title: '适应机种数字', width: 100, field: 'SubInts', },
        ]],
    });
}
//加载所有Submodel
function loadSubmodelDgTable() {
    $('#dgSubmodelTable').datagrid({
        url: '../../Machine/GetMachine',
        idField: "Id",
        pagination: true,
        rownumbers: true,
        pagePosition: 'bottom',
        pageSize: 5,
        pageNumber: 1,
        pageList: [5, 10, 15, 20, 30, 50],
        singleSelect: false,
        columns: [[
            { checkbox: true, width: 50,field: 'checkbox', },
            { title: '序号', width: 50, field: 'Id', },
            { title: '编号', width: 100, field: 'Code', },
            { title: '简称', width: 100, field: 'Int', },
            { title: '全称', width: 200, field: 'Name', },
            { title: 'BKC', width: 100, field: 'BKC', },
        ]],
        onLoadSuccess: function () {
            var selectedList = $('#dgSubmodelIdList').val();
            var powerArray = selectedList.split(',');
            for (var i = 0; i < powerArray.length; i++) {
                if (powerArray[i] == '') continue;
                $(this).datagrid('selectRecord', powerArray[i]);
            }
        },
        onCheck: function (index, row) {
            var selectedList = $('#dgSubmodelIdList').val();
            var oldStr = ',' + row.Id;
            if (isContainsArr(selectedList, oldStr,',')) return;//当包含字串时，不再新增
            selectedList = selectedList + oldStr;
            $('#dgSubmodelIdList').val(selectedList);
        },
        onUncheck: function (index, row) {
            var selectedList = $('#dgSubmodelIdList').val();
            var oldStr = ',' + row.Id;
            //包含字串才替换
            if (isContainsArr(selectedList, oldStr, ',')) selectedList = arrStrRemoveStr(selectedList, oldStr, ',');
            $('#dgSubmodelIdList').val(selectedList);
        },
        onSelectAll: function (rows) {
            //用户选中所有的行
            var selectedList = $('#dgSubmodelIdList').val();
            var oldStr = '';
            for (var i in rows) {
                oldStr = ',' + rows[i].Id;
                if (isContainsArr(selectedList, oldStr,',')) continue;
                selectedList = selectedList + oldStr;
            }
            $('#dgSubmodelIdList').val(selectedList);
        },
        onUnselectAll: function (rows) {
            //用户取消选中所有的行
            console.info(rows);
            var selectedList = $('#dgSubmodelIdList').val();
            var oldStr = '';
            for (var i in rows) {
                oldStr = ',' + rows[i].Id;
                if (isContainsArr(selectedList, oldStr, ',')) selectedList = arrStrRemoveStr(selectedList, oldStr, ',');
            }
            $('#dgSubmodelIdList').val(selectedList);
        },
    });

}
//清楚dialog中的选项
function clearDGSM() {
    $('#dgSubmodelIdList').val('');
    $('#dgSubmodelTable').datagrid('clearSelections');
    $('.errorMsg').text('');
}


//初始化点击按钮、更改下拉框事件
function initSMClick() {
    //点击Search按钮
    $('#btnSMSearch').click(function () {
        reloadSMTable();//重新加载表格
    });
    //点击清除
    $('#btnSMClear').click(function () {
        $('#searchSMName').textbox('setValue', '');
    });
    //点击修改
    $('#btnSMUpdate').click(function () {
        var rows = $('#SMTable').datagrid('getSelections');
        if (rows.length <= 0) { show('请选择任意一行！'); return; }
        clearDGSM();
        var data = tableClickUpdateById(rows[0].Id, '../../Machine/SelectMachineSubOne', '#SMDialog', '修改');
        $('#dgSMId').textbox('setValue', data.Id);
        $('#dgSMName').textbox('setValue', data.PCName);
        $('#dgSubmodelIdList').val(data.SubIds);
        loadSubmodelDgTable();
    });
}

//重新加载表格
function reloadSMTable() {
    $('#SMTable').datagrid('load', {
        PCName: $('#searchSMName').textbox('getValue'),
    });
}