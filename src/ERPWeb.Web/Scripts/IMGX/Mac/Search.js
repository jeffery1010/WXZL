//窗体加载
$(function () {
    initMacCombobox();
    initMacTable();
});


var comboboxJson = [{}];
//初始化下拉框
function initMacCombobox() {
    var isValidJson = [{ name: '--全部--', id: '-1' }, { name: '失效', id: '0' }, { name: '激活', id: '1' }];
    $('.easyui-combobox[title="isvalid"]').each(function () {
        loadDropDownListObj(this, isValidJson, 'id', 'name', '-1');
    });
    
}


//初始化表格
function initMacTable() {
    var ip4 = '';
    var name = '';
    var isvalid = -1;
    $('.easyui-datagrid[title="SearchMac"]').datagrid({
        //分页
        fitColumns: true,
        singleSelect: false,
        url: '../../Mac/GetMac',
        queryParams: { ip4: ip4, name: name, isvalid: isvalid},
        pagination: true,
        rownumbers: true,
        pagePosition: 'bottom',
        pageSize: 10,
        pageNumber: 1,
        pageList: [2, 10, 15, 20, 30, 50],
        toolbar: '.macToolBar',
        fitColumns: true,
        //数据格式
        columns: [[
            { checkbox: true, field: 'checkbox', },
            { title: '序号', width: 50, field: 'id', fixed: true,/*hidden: true*/ },
            { title: 'IP4', width: 120, field: 'ip4', fixed: true, },
            { title: '名称', width: 90, field: 'name', fixed: true, },
            { title: '工位编号', width: 80, field: 'wscode', fixed: true, },
            { title: '工位名称', width: 200, field: 'wsname', fixed: true, },
            { title: '工位机种', width: 50, field: 'modelname', fixed: true, },
            {
                title: '更新时间', width: 180, field: 'chtime', fixed: true, formatter: function (value, row) {
                    return toDateStr(value);
                }
            },
            {
                title: '状态', width: 50, field: 'isvalid', fixed: true, formatter: function (value, row) {
                    return value==1?'激活':'失效';
                }},
        ]],
    });
}


//重新加载表格
function reloadMacTable(ip4,name,isvalid) {
    $('.easyui-datagrid[title="SearchMac"]').datagrid('load', {
        ip4:ip4,
        name: name,
        isvalid:isvalid,
    });
}

function clickSearch(obj) {
    
    
    var ip4 = $(obj).siblings('.inputtext[title="ip4"]').first().val();
    var name = $(obj).siblings('.inputtext[title="name"]').first().val();
    var $isvalidDoc = $(obj).siblings('.easyui-combobox[title="isvalid"]').first();
    var isvalid = $($isvalidDoc).combobox('getValue');
    reloadMacTable(ip4,name,isvalid);//重新加载表格
}

function clickSearchClear(obj) {
    $(obj).siblings('.inputtext[title="ip4"]').first().val('');
    $(obj).siblings('.inputtext[title="name"]').first().val('');
    var $isvalidDoc = $(obj).siblings('.easyui-combobox[title="isvalid"]').first();
    $($isvalidDoc).combobox('setValue', '-1');
}


