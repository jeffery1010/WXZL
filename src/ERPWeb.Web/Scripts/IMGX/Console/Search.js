//窗体加载
$(function () {
    initConsoleCombobox();
    initConsoleTable();
});


var comboboxJson = [{}];
//初始化下拉框
function initConsoleCombobox() {
    comboboxJson = ajaxSame('../../Model/GetModelCombobox', null, 'post', 'json');
    var isValidJson = [{ name: '--全部--', id: '-1' }, { name: '失效', id: '0' }, { name: '激活', id: '1' }];
    $('.easyui-combobox[title="isvalid"]').each(function () {
        loadDropDownListObj(this, isValidJson, 'id', 'name', '-1');
    });
    $('.easyui-combobox[title="modelid"]').each(function () {
        loadDropDownListObj(this, comboboxJson, 'id', 'name', '-1');
    });
    
}


//初始化表格
function initConsoleTable() {
    var ip4 = '';
    var name = '';
    var modelid = -1;
    var isvalid = -1;
    $('.easyui-datagrid[title="SearchConsole"]').datagrid({
        //分页
        fitColumns: true,
        singleSelect: true,
        url: '../../Console/GetConsole',
        queryParams: { ip4: ip4, name: name,modelid:modelid, isvalid: isvalid },
        pagination: true,
        rownumbers: true,
        pagePosition: 'bottom',
        pageSize: 10,
        pageNumber: 1,
        pageList: [2, 10, 15, 20, 30, 50],
        toolbar: '.ConsoleToolBar',
        fitColumns: true,
        //数据格式
        columns: [[
            { title: '序号', width: 100, field: 'id', fixed: true,/*hidden: true*/ },
            { title: 'IP4', width: 100, field: 'ip4', },
            { title: '名称', width: 100, field: 'name', },
            {
                title: '机种', width: 100, field: 'modelid', formatter: function (value, row) {
                    if (typeof (comboboxJson[0].id) != 'number') comboboxJson = ajaxSame('../../Model/GetModelCombobox', null, 'post', 'json');
                    for (var i in comboboxJson) {
                        if (value == comboboxJson[i].id) {
                            return comboboxJson[i].name;
                        }
                    }
                }
            },
            {
                title: '状态', width: 100, field: 'isvalid', formatter: function (value, row) {
                    return value == 1 ? '激活' : '失效';
                }
            },
        ]],
    });
}


//重新加载表格
function reloadConsoleTable(ip4,name,modelid,isvalid) {
    $('.easyui-datagrid[title="SearchConsole"]').datagrid('load', {
        ip4: ip4,
        name: name,
        modelid: modelid,
        isvalid: isvalid,
    });
}

function clickSearch(obj) {
    
    
    var ip4 = $(obj).siblings('.inputtext[title="ip4"]').first().val();
    var name = $(obj).siblings('.inputtext[title="name"]').first().val();
    var $modelidDoc = $(obj).siblings('.easyui-combobox[title="modelid"]').first();
    var $isvalidDoc = $(obj).siblings('.easyui-combobox[title="isvalid"]').first();
    var modelid = $($modelidDoc).combobox('getValue');
    var isvalid = $($isvalidDoc).combobox('getValue');
    reloadConsoleTable(ip4,name,modelid,isvalid);//重新加载表格
}

function clickSearchClear(obj) {
    $(obj).siblings('.inputtext[title="ip4"]').first().val('');
    $(obj).siblings('.inputtext[title="name"]').first().val('');
    var $modelidDoc = $(obj).siblings('.easyui-combobox[title="modelid"]').first();
    var $isvalidDoc = $(obj).siblings('.easyui-combobox[title="isvalid"]').first();
    $($modelidDoc).combobox('setValue', '-1');
    $($isvalidDoc).combobox('setValue', '-1');
}


