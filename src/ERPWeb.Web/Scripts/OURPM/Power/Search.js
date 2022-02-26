//窗体加载
$(function () {
    //initMacCombobox();
    initPowerTable();
});

//初始化表格
function initPowerTable() {
    var code = '';
    var name = '';
    var isvalid = -1;
    var isenable = -1;
    $('.easyui-datagrid[title="SearchPartialPower"]').datagrid({
        //分页
        fitColumns: true,
        singleSelect: true,
        url: '/Power/GetPower',
        queryParams: { PowerCode: code, PowerName: name, IsVisible: isvalid, IsEnable: isenable },
        pagination: true,
        rownumbers: true,
        pagePosition: 'bottom',
        pageSize: 10,
        pageNumber: 1,
        pageList: [2, 10, 15, 20, 30, 50],
        toolbar: '.powerToolBar',
        fitColumns: true,
        //数据格式
        columns: [[
            { title: '序号', width: 100, field: 'Id', hidden: true },
            { title: '编号', width: 100, field: 'PowerCode', },
            { title: '名称', width: 100, field: 'PowerName', },
            { title: '备注', width: 100, field: 'PowerRemark', },
            {
                title: '创建时间', width: 100, field: 'CreateTime',
                formatter: function (value, row, index) {
                    // value /Date(1563950624813)/  .slice(6)  从第6位开始(下标起始0)
                    var time = new Date(parseInt(value.slice(6)));
                    return time.getFullYear() + '-' + time.getMonth() + '-' + time.getDate() +
                        ' ' + time.getHours() + ':' + time.getMinutes() + ':' + time.getSeconds();
                }
            },
            { title: '创建人', width: 100, field: 'UserName', },
            {
                title: '是否可见', width: 100, field: 'IsVisible',
                formatter: function (value, row, index) {
                    if (value == 0) return "不可见";
                    else if (value == 1) return "可见";
                }
            },
            {
                title: '是否激活', width: 100, field: 'IsEnable', formatter: function (value, row, index) {
                    if (value == 0) return "未激活";
                    else if (value == 1) return "已激活";
                }
            },
        ]],
    });
}


//重新加载表格
function reloadPowerTable(code, name, isvalid,isenable) {
    $('.easyui-datagrid[title="SearchPartialPower"]').datagrid('load', {
        PowerCode: code,
        PowerName: name,
        IsVisible: isvalid,
        IsEnable: isenable,
    });
}

function clickSearch(obj) {
    var $codeDoc = $(obj).siblings('.easyui-textbox[title="code"]').first();
    var code = $($codeDoc).textbox('getValue');
    var $nameDoc = $(obj).siblings('.easyui-textbox[title="name"]').first();
    var name = $($nameDoc).textbox('getValue');
    //var code = $(obj).siblings('.inputtext[title="code"]').first().val();
    //var name = $(obj).siblings('.inputtext[title="name"]').first().val();
    var $isvalidDoc = $(obj).siblings('.easyui-combobox[title="isvalid"]').first();
    var isvalid = $($isvalidDoc).combobox('getValue');
    var $isenableDoc = $(obj).siblings('.easyui-combobox[title="isenable"]').first();
    var isenable = $($isenableDoc).combobox('getValue');
    reloadPowerTable(code, name, isvalid, isenable);//重新加载表格
}

function clickSearchClear(obj) {
    var $codeDoc = $(obj).siblings('.easyui-textbox[title="code"]').first();
    $($codeDoc).textbox('setValue', '');
    var $nameDoc = $(obj).siblings('.easyui-textbox[title="name"]').first();
    $($nameDoc).textbox('setValue', '');
    //$(obj).siblings('.inputtext[title="code"]').first().val('');
    //$(obj).siblings('.inputtext[title="name"]').first().val('');
    var $isvalidDoc = $(obj).siblings('.easyui-combobox[title="isvalid"]').first();
    $($isvalidDoc).combobox('setValue', '-1');
    var $isenableDoc = $(obj).siblings('.easyui-combobox[title="isenable"]').first();
    $($isenableDoc).combobox('setValue', '-1');
}


