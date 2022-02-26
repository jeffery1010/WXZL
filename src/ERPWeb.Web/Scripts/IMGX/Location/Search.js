//窗体加载
$(function () {
    initLocationTable();
});


//初始化表格
function initLocationTable() {
    var features = '';
    var name = '';
    $('.easyui-datagrid[title="SearchLocation"]').datagrid({
        //分页
        fitColumns: true,
        singleSelect: false,
        url: '../../Location/GetLocation',
        queryParams: { features: features, name: name },
        pagination: true,
        rownumbers: true,
        pagePosition: 'bottom',
        pageSize: 10,
        pageNumber: 1,
        pageList: [2, 10, 15, 20, 30, 50],
        toolbar: '.LocationToolBar',
        fitColumns: true,
        //数据格式
        columns: [[
            { title: '序号', width: 100, field: 'id', fixed: true,/*hidden: true*/ },
            { title: '特性', width: 100, field: 'features', },
            { title: '名称', width: 100, field: 'name', },
        ]],
    });
}


//重新加载表格
function reloadLocationTable(features, name) {
    $('.easyui-datagrid[title="SearchLocation"]').datagrid('load', {
        features: features,
        name: name,
    });
}

function clickSearch(obj) {
    var features = $(obj).siblings('.inputtext[title="features"]').first().val();
    var name = $(obj).siblings('.inputtext[title="name"]').first().val();
    reloadLocationTable(features, name);//重新加载表格
}

function clickSearchClear(obj) {
    $(obj).siblings('.inputtext[title="features"]').first().val('');
    $(obj).siblings('.inputtext[title="name"]').first().val('');
}


