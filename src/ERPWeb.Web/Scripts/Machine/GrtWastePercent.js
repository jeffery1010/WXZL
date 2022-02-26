//窗体加载
$(function () {
    initGRTTable();
    initGRTClick();
    $('#searchGRTDate').datebox({
        onSelect: function (date)
        {
            $('#searchGRTDate').textbox('setValue', date.getFullYear() +'-' +(date.getMonth() + 1) +'-' +date.getDate());
        }
    });
});



//初始化表格
function initGRTTable() {
    var modelLine = $('#searchGRTInputLine').textbox('getValue');
    var shift = $('#searchGRTInputShift').textbox('getValue');
    var checkDate = $('#searchGRTDate').textbox('getValue');
    $('#GRTTable').datagrid({
        //分页
        fitColumns: true,
        singleSelect: true,
        url: '../../Machine/GetGrtWastePercent',
        queryParams: { ModelLine: modelLine, Shift: shift, CheckDate: checkDate },
        pagination: true,
        rownumbers: true,
        pagePosition: 'bottom',
        pageSize: 10,
        pageNumber: 1,
        pageList: [2, 10, 15, 20, 30, 50],
        toolbar: '#searchGRTToolBar',
        fitColumns: true,
        //数据格式
        columns: [[
            { title: '机种与线别', width: 150, field: 'ModelLine', },
            { title: '班别', width: 150, field: 'Shift', },
            { title: '已投入托盘', width: 150, field: 'TrayInputNum', },
            { title: '已投入GRT总数', width: 150, field: 'GRTTotalNum', },
            { title: '已消耗GRT总数', width: 150, field: 'GRTShiftNum', },
            { title: '抛料数', width: 125, field: 'GRTWasteNum' },
            { title: '抛料率', width: 125, field: 'GRTWastePercent' },
            { title: '查询时间', width: 200, field: 'CheckDate' },
        ]],
    });
}

function initGRTClick() {
    //点击Search按钮
    $('#btnGRTSearch').click(function () {
        reloadGRTTable();//重新加载表格
    });
}

function reloadGRTTable() {
    $('#GRTTable').datagrid('load', {
        modelLine: $('#searchGRTInputLine').textbox('getValue'),
        shift: $('#searchGRTInputShift').textbox('getValue'),
        checkDate: $('#searchGRTDate').textbox('getValue'),
    });
}