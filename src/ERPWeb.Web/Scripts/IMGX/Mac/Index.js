//窗体加载
$(function () {
    initMacCombobox();
    loadWSTable(); 
    initMacTable();
    initMacDialog();
    initMacClick();
    
});

//加载所有Port
function loadWSTable() {
    var code = $('#dgSearchWorkStationCode').textbox('getValue');
    var modelid = $('#dgSearchWorkStationModel').combobox('getValue');
    var status = $('#dgSearchWorkStationIsValid').combobox('getValue');
    $('#dgWSTable').datagrid({
        url: '../../WorkStation/GetWorkStation',
        idField: "id",
        queryParams: { code: code, modelid: modelid,status:status },
        pagination: true,
        rownumbers: true,
        pagePosition: 'bottom',
        pageSize: 5,
        pageNumber: 1,
        pageList: [5, 10, 15, 20, 30, 50],
        singleSelect: true,
        columns: [[
            { checkbox: true, field: 'checkbox', },
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
                    return value == 1 ? '激活' : '失效';
                }
            },
        ]],
        onLoadSuccess: function () {
            var selectedId = $('#dgMacWSId').val();
            if (selectedId == "") return;
            $('#dgWSTable').datagrid('selectRecord', selectedId);
        },
        onCheck: function (index, row) {
            if (row.id <= 0) { return; }
            $('#dgMacWSId').textbox('setValue', row.id);
            $('#dgMacWSCode').textbox('setValue', row.code);
            
        },
    });

}

//验证输入-点击提交时
function checkMacClickSubmit(idList) {
    $(idList).each(function (index, element) {
        checkMacAttention($(this));
    });
}
//验证输入-更改提示信息
function checkMacAttention(element) {
    var id = $(element).attr('id');
    var value = $(element).textbox('getValue');
    var errorMsg = $(element).parents('tr').find('.errorMsg');
    if (id == 'dgMacIP4') {
        if (checkValLenString(value, 1, 64).code == 0) errorMsg.text('编号必须在1-64个字符之间'); else errorMsg.text('');
    } else if (id == 'dgMacName') {
        if (checkValLenString(value, 1, 64).code == 0) errorMsg.text('名称必须在1-64个字符之间'); else errorMsg.text('');
    } else if (id == 'dgMacMaxnum') {
        if (checkValLenString(value, 1, 1024).code == 0) errorMsg.text('备注必须满足1-1024个字节之间'); else errorMsg.text('');
    } 
}
var comboboxJson = [{}];
//初始化下拉框
function initMacCombobox() {
    comboboxJson = ajaxSame('../../Model/GetModelCombobox', null, 'post', 'json');
    var isValidJson = [{ name: '--全部--', id: '-1' }, { name: '失效', id: '0' }, { name: '激活', id: '1' }];
    var isValidJsonOnly = [{ name: '失效', id: '0' }, { name: '激活', id: '1' }];
    
    loadDropDownListAll('#dgMacModel', comboboxJson, 'id', 'name', '-1');
    loadDropDownListAll('#dgSearchWorkStationModel', comboboxJson, 'id', 'name', '-1');

    loadDropDownListAll('#searchMacIsValid', isValidJson, 'id', 'name', '-1');
    loadDropDownListAll('#dgSearchWorkStationIsValid', isValidJson, 'id', 'name', '-1');
    loadDropDownListAll('#dgSearchWorkStationIsValid', isValidJson, 'id', 'name', '-1');
    loadDropDownListAll('#dgMacIsValid', isValidJsonOnly, 'id', 'name', '1');
    
}
//初始化提示框
function initMacDialog() {
    $('#MacDialog').dialog({
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
                checkMacClickSubmit('#dgMacIP4,#dgMacName,#dgMacWSId');
                if (!IsSubmitDialog('#MacDialog')) { return; }

                var id = $('#dgMacId').textbox('getValue');
                var ip4 = $('#dgMacIP4').textbox('getValue');
                var name = $('#dgMacName').textbox('getValue');
                var isvalid = $('#dgMacIsValid').combobox('getValue');
                var wsid = $('#dgMacWSId').textbox('getValue');

                var data = { Id: id, ip4: ip4, name: name, isvalid: isvalid, wsid: wsid };
                var url = $('#MacDialog').panel('options').title == '新增' ? '../../Mac/InsertMac' : '../../Mac/UpdateMac';
                var result = dialogClickKeep(url, data, 'post', 'json');
                if (typeof (result) == 'string') { result = $.parseJSON(result); }
                if (result.Code == 200) {
                    clearDGMac();
                    $('#MacDialog').dialog({ closed: true, });
                    reloadMacTable();//重新加载表格
                } else {
                    show(result.Message);
                }
            }
        }, {
            text: '取消',
            iconCls: 'icon-cancel',
            handler: function () {
                clearDGMac();
                $('#MacDialog').dialog({ closed: true, });
            }
        }],
    });
}

//初始化表格
function initMacTable() {
    var ip4 = $('#searchMacIP4').textbox('getValue');
    var name = $('#searchMacName').textbox('getValue');
    var isvalid = $('#searchMacIsValid').combobox('getValue');
    $('#MacTable').datagrid({
        //分页
        fitColumns: true,
        singleSelect: true,
        url: '../../Mac/GetMac',
        queryParams: { ip4: ip4, name: name, isvalid: isvalid },
        pagination: true,
        rownumbers: true,
        pagePosition: 'bottom',
        pageSize: 10,
        pageNumber: 1,
        pageList: [2, 10, 15, 20, 30, 50],
        toolbar: '#searchMacToolBar',
        fitColumns: true,
        //数据格式
        columns: [[
            { title: '序号', width: 100, field: 'id', fixed: true,/*hidden: true*/ },
            { title: 'IP4', width: 100, field: 'ip4', },
            { title: '名称', width: 100, field: 'name', },
            { title: '工位编号', width: 100, field: 'wscode', },
            { title: '工位名称', width: 100, field: 'wsname', },
            { title: '工位机种', width: 100, field: 'modelname', },
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
function clearDGMac() {
    $('#dgMacId').textbox('setValue', '');
    $('#dgMacIP4').textbox('setValue', '');
    $('#dgMacName').textbox('setValue', '');
    $('#dgMacIsValid').combobox('setValue', '1');

    $('#dgWSTable').datagrid('clearChecked');
    $('#dgWSTable').datagrid('clearSelections');

    $('#dgMacWSId').textbox('setValue','');
    $('#dgMacWSCode').textbox('setValue','');
    $('.errorMsg').text('');
}


//初始化点击按钮、更改下拉框事件
function initMacClick() {
    //点击Search按钮
    $('#btnMacSearch').click(function () {
        reloadMacTable();//重新加载表格
    });
    //点击清除
    $('#btnMacClear').click(function () {
        $('#searchMacIP4').textbox('setValue', '');
        $('#searchMacName').textbox('setValue', '');
        $('#searchMacIsValid').combobox('setValue', '-1');
    });
    //点击新增
    $('#btnMacInsert').click(function () {
        clearDGMac();
        tableClickInsert('#MacDialog', '新增');
    });
    //点击修改
    $('#btnMacUpdate').click(function () {
        var rows = $('#MacTable').datagrid('getSelections');
        if (rows.length <= 0) { show('请选择任意一行！'); return; }
        clearDGMac();
        $('#dgMacId').textbox('setValue', rows[0].id);
        $('#dgMacIP4').textbox('setValue', rows[0].ip4);
        $('#dgMacName').textbox('setValue', rows[0].name);
        $('#dgMacIsValid').combobox('setValue', rows[0].isvalid);

        $('#dgMacWSId').textbox('setValue', rows[0].wsid);
        $('#dgMacWSCode').textbox('setValue', rows[0].wscode);
        reloadDGWorkStationTable();
        $('#MacDialog').dialog({
            closed: false,
            title: '修改',
        });
        
    });

    //点击Search按钮
    $('#btndgWorkStationSearch').click(function () {
        this.reloadDGWorkStationTable();
    });
    //点击清除
    $('#btndgWorkStationClear').click(function () {
        $('#dgSearchWorkStationCode').textbox('setValue', '');
        $('#dgSearchWorkStationModel').combobox('setValue', '-1');
        $('#dgSearchWorkStationIsValid').combobox('setValue', '-1');
    });
}

//重新加载表格
function reloadMacTable() {
    $('#MacTable').datagrid('load', {
        ip4: $('#searchMacIP4').textbox('getValue'),
        name: $('#searchMacName').textbox('getValue'),
        isvalid: $('#searchMacIsValid').combobox('getValue'),
    });
}
function reloadDGWorkStationTable() {
    $('#dgWSTable').datagrid('load', {
        code: $('#dgSearchWorkStationCode').textbox('getValue'),
        modelid: $('#dgSearchWorkStationModel').combobox('getValue'),
        status: $('#dgSearchWorkStationIsValid').combobox('getValue'),
    });
}