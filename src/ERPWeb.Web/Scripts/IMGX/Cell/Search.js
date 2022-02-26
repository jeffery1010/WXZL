//窗体加载
$(function () {
    initCellCombobox();
    initCellTable();
});


var comboboxJson = [{}];
//初始化下拉框
function initCellCombobox() {
    comboboxJson = ajaxSame('../../Model/GetModelCombobox', null, 'post', 'json');
    var isValidJson = [{ name: '--全部--', id: '-1' }, { name: '失效', id: '0' }, { name: '激活', id: '1' }];
    $('.easyui-combobox[title="isvalid"]').each(function () {
        loadDropDownListObj(this, isValidJson, 'id', 'name', '-1');
    });
    $('.easyui-combobox[title="model"]').each(function () {
        loadDropDownListObj(this, comboboxJson, 'id', 'name', '-1');
    });
}


//初始化表格
function initCellTable() {
    var code = '';
    var name = '';
    var modelid = -1;
    var isvalid = -1;
    $('.easyui-datagrid[title="SearchCell"]').datagrid({
        //分页
        fitColumns: true,
        singleSelect: true,
        url: '../../Cell/GetCell',
        queryParams: { code: code, name: name, isvalid: isvalid, modelid: modelid, },
        pagination: true,
        rownumbers: true,
        pagePosition: 'bottom',
        pageSize: 10,
        pageNumber: 1,
        pageList: [2, 10, 15, 20, 30, 50],
        toolbar: '.CellToolBar',
        fitColumns: true,
        //数据格式
        columns: [[
            { title: '序号', width: 50, field: 'id', fixed: true,/*hidden: true*/ },
            { title: '编号', width: 50, field: 'code', },
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
            {
                title: '更新时间', width: 180, field: 'chtime', formatter: function (value, row) {
                    return toDateStr(value);
                }
            },
            {
                title: '状态', width: 50, field: 'isvalid', formatter: function (value, row) {
                    return value == 1 ? '激活' : '失效';
                }
            },
        ]],
    });
}


//重新加载表格
function reloadCellTable(code,name,isvalid,modelid) {
    $('.easyui-datagrid[title="SearchCell"]').datagrid('load', {
        code: code,
        name: name,
        isvalid: isvalid,
        modelid: modelid,
    });
}

function clickSearch(obj) {
    
    
    var code = $(obj).siblings('.inputtext[title="code"]').first().val();
    var name = $(obj).siblings('.inputtext[title="name"]').first().val();
    var $isvalidDoc = $(obj).siblings('.easyui-combobox[title="isvalid"]').first();
    var $modelidDoc = $(obj).siblings('.easyui-combobox[title="model"]').first();
    var isvalid = $($isvalidDoc).combobox('getValue');
    var modelid = $($modelidDoc).combobox('getValue');
    reloadCellTable(code, name, isvalid, modelid);//重新加载表格
}

function clickSearchClear(obj) {
    $(obj).siblings('.inputtext[title="code"]').first().val('');
    $(obj).siblings('.inputtext[title="name"]').first().val('');
    var $isvalidDoc = $(obj).siblings('.easyui-combobox[title="isvalid"]').first();
    var $modelidDoc = $(obj).siblings('.easyui-combobox[title="isvalid"]').first();
    $($isvalidDoc).combobox('setValue', '-1');
    $($modelidDoc).combobox('setValue', '-1');
}


