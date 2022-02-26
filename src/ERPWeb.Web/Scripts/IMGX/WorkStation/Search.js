//窗体加载
$(function () {
    initWorkStationCombobox();
    var defaultModel = $('#tbxWorkStationModelDefault').val();
    if (defaultModel != '') {
        var $defaultModel = $('#tbxWorkStationModelDefault');
        $($defaultModel).siblings('.easyui-combobox[title="modelid"]').first().combobox('setValue', defaultModel);
    }
    initWorkStationTable();
    
});


var comboboxJson = [{}];
//初始化下拉框
function initWorkStationCombobox() {
    comboboxJson = ajaxSame('../../Model/GetModelCombobox', null, 'post', 'json');
    console.info(comboboxJson);
    var isValidJson = [{ name: '--全部--', id: '-1' }, { name: '失效', id: '0' }, { name: '激活', id: '1' }];
    $('.easyui-combobox[title="isvalid"]').each(function () {
        loadDropDownListObj(this, isValidJson, 'id', 'name', '-1');
    });
    $('.easyui-combobox[title="modelid"]').each(function () {
        loadDropDownListObj(this, comboboxJson, 'id', 'name', '-1');
    });
    
}


//初始化表格
function initWorkStationTable() {
    var $defaultModel = $('#tbxWorkStationModelDefault');
    var code = '';
    var name = '';
    var status = -1;
    var modelid = $($defaultModel).siblings('.easyui-combobox[title="modelid"]').first().combobox('getValue');
    $('.easyui-datagrid[title="SearchWorkStation"]').datagrid({
        //分页
        fitColumns: true,
        singleSelect: false,//允许选择多行，在调用的地方管控
        url: '../../WorkStation/GetWorkStation',
        queryParams: { code: code, name: name, status: status,modelid:modelid },
        pagination: true,
        rownumbers: true,
        pagePosition: 'bottom',
        pageSize: 10,
        pageNumber: 1,
        pageList: [2, 10, 15, 20, 30, 50],
        toolbar: '.WorkStationToolBar',
        fitColumns: true,
        //数据格式
        columns: [[
            { checkbox: true, field: 'checkbox', },
            { title: '序号', width: 100, field: 'id', fixed: true,/*hidden: true*/ },
            { title: '编号', width: 100, field: 'code', },
            { title: '名称', width: 100, field: 'name', },
            {
                title: '机种', width: 100, field: 'modelid', formatter: function (value, row) {
                    if (typeof (comboboxJson[0].id) != 'number') {
                        comboboxJson = ajaxSame('../../Model/GetModelCombobox', null, 'post', 'json');
                    }
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
    });
}


//重新加载表格
function reloadWorkStationTable(code, name, status, modelid) {
    $('.easyui-datagrid[title="SearchWorkStation"]').datagrid('load', {
        code: code,
        name: name,
        status: status,
        modelid: modelid,
    });
}

function clickSearch(obj) {
    var code = $(obj).siblings('.inputtext[title="code"]').first().val();
    var name = $(obj).siblings('.inputtext[title="name"]').first().val();
    var $isvalidDoc = $(obj).siblings('.easyui-combobox[title="isvalid"]').first();
    var $modelidDoc = $(obj).siblings('.easyui-combobox[title="modelid"]').first();
    var status = $($isvalidDoc).combobox('getValue');
    var modelid = $($modelidDoc).combobox('getValue');
    reloadWorkStationTable(code, name, status,modelid);//重新加载表格
}

function clickSearchClear(obj) {
    $(obj).siblings('.inputtext[title="code"]').first().val('');
    $(obj).siblings('.inputtext[title="name"]').first().val('');
    var $isvalidDoc = $(obj).siblings('.easyui-combobox[title="isvalid"]').first();
    var $modelidDoc = $(obj).siblings('.easyui-combobox[title="modelid"]').first();
    $($isvalidDoc).combobox('setValue', '-1');
    $($modelidDoc).combobox('setValue', '-1');
}


