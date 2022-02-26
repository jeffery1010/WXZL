//窗体加载
$(function () {
    initXPathCombobox();
    initXPathTable();
});


var comboboxJson = [{}];
//初始化下拉框
function initXPathCombobox() {
    var isValidJson = [{ name: '--全部--', id: '-1' }, { name: '失效', id: '0' }, { name: '激活', id: '1' }];
    $('.easyui-combobox[title="isvalid"]').each(function () {
        loadDropDownListObj(this, isValidJson, 'id', 'name', '-1');
    });
    
}


//初始化表格
function initXPathTable() {
    var extend = '';
    var ratemin = 1;
    var ratemax = 99999;
    var isvalid = -1;
    $('.easyui-datagrid[title="SearchXPath"]').datagrid({
        //分页
        fitColumns: true,
        singleSelect: true,
        url: '../../XPath/GetXPath',
        queryParams: { extend: extend, ratemin: ratemin, ratemax: ratemax, isvalid: isvalid },
        pagination: true,
        rownumbers: true,
        pagePosition: 'bottom',
        pageSize: 10,
        pageNumber: 1,
        pageList: [2, 10, 15, 20, 30, 50],
        toolbar: '.XPathToolBar',
        fitColumns: true,
        //数据格式
        columns: [[
            { title: '序号', width: 50, field: 'id', fixed: true,/*hidden: true*/ },
            { title: '机台序号', width: 50, field: 'macid', },
            { title: '机台名称', width: 100, field: 'macname', },
            { title: '机台IP4', width: 100, field: 'macip4', },
            { title: 'XPath', width: 100, field: 'xpath', },
            { title: 'ToPath', width: 100, field: 'topath', },
            { title: '拓展', width: 50, field: 'extend', },
            { title: '频率', width: 50, field: 'rate', },
            { title: '工号', width: 100, field: 'userno', },
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
function reloadXPathTable(extend, ratemin, ratemax,isvalid) {
    $('.easyui-datagrid[title="SearchXPath"]').datagrid('load', {
        extend: extend,
        ratemin: ratemin,
        ratemax: ratemax,
        isvalid:isvalid,
    });
}

function clickSearch(obj) {
    
    
    var extend = $(obj).siblings('.inputtext[title="extend"]').first().val();
    var ratemin = $(obj).siblings('.inputtext[title="ratemin"]').first().val();
    var ratemax = $(obj).siblings('.inputtext[title="ratemax"]').first().val();
    var $isvalidDoc = $(obj).siblings('.easyui-combobox[title="isvalid"]').first();
    var isvalid = $($isvalidDoc).combobox('getValue');

    if (ratemin == '') ratemin = 1;
    if (ratemax == '') ratemax = 99999;
    if (parseInt(ratemin) > parseInt(ratemax)) {
        show('频率异常，请重新输入'); return;
    }

    reloadXPathTable(extend, ratemin, ratemax,isvalid);//重新加载表格
}

function clickSearchClear(obj) {
    $(obj).siblings('.inputtext[title="extend"]').first().val('');
    $(obj).siblings('.inputtext[title="ratemin"]').first().val('');
    $(obj).siblings('.inputtext[title="ratemax"]').first().val('');
    var $isvalidDoc = $(obj).siblings('.easyui-combobox[title="isvalid"]').first();
    $($isvalidDoc).combobox('setValue', '-1');
}


