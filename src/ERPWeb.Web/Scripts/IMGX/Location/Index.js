//窗体加载
$(function () {
    initLocationTable();
    initLocationDialog();
    initLocationClick();

});

//验证输入-点击提交时
function checkLocationClickSubmit(idList) {
    $(idList).each(function (index, element) {
        checkLocationAttention($(this));
    });
}
//验证输入-更改提示信息
function checkLocationAttention(element) {
    var id = $(element).attr('id');
    var value = $(element).textbox('getValue');
    var errorMsg = $(element).parents('tr').find('.errorMsg');
    if (id == 'dgLocationFeatures') {
        if (checkValLenString(value, 1, 64).code == 0) errorMsg.text('编号必须在1-64个字符之间'); else errorMsg.text('');
    } else if (id == 'dgLocationName') {
        if (checkValLenString(value, 1, 64).code == 0) errorMsg.text('名称必须在1-64个字符之间'); else errorMsg.text('');
    } 
}


//初始化提示框
function initLocationDialog() {
    $('#LocationDialog').dialog({
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
                checkLocationClickSubmit('#dgLocationFeatures,#dgLocationName');
                if (!IsSubmitDialog('#LocationDialog')) { return; }

                var id = $('#dgLocationId').textbox('getValue');
                var features = $('#dgLocationFeatures').textbox('getValue');
                var name = $('#dgLocationName').textbox('getValue');

                var data = { Id: id, features: features, name: name };
                var url = $('#LocationDialog').panel('options').title == '新增' ? '../../Location/InsertLocation' : '../../Location/UpdateLocation';
                var result = dialogClickKeep(url, data, 'post', 'json');
                if (typeof (result) == 'string') { result = $.parseJSON(result); }
                if (result.Code == 200) {
                    clearDGLocation();
                    $('#LocationDialog').dialog({ closed: true, });
                    reloadLocationTable();//重新加载表格
                } else {
                    show(result.Message);
                }
            }
        }, {
            text: '取消',
            iconCls: 'icon-cancel',
            handler: function () {
                clearDGLocation();
                $('#LocationDialog').dialog({ closed: true, });
            }
        }],
    });
}

//初始化表格
function initLocationTable() {
    var features = $('#searchLocationFeatures').textbox('getValue');
    var name = $('#searchLocationName').textbox('getValue');
    $('#LocationTable').datagrid({
        //分页
        fitColumns: true,
        singleSelect: true,
        url: '../../Location/GetLocation',
        queryParams: { features: features, name: name},
        pagination: true,
        rownumbers: true,
        pagePosition: 'bottom',
        pageSize: 10,
        pageNumber: 1,
        pageList: [2, 10, 15, 20, 30, 50],
        toolbar: '#searchLocationToolBar',
        fitColumns: true,
        //数据格式
        columns: [[
            { title: '序号', width: 100, field: 'id', fixed: true,/*hidden: true*/ },
            { title: '名称', width: 100, field: 'name', },
            { title: '特性', width: 100, field: 'features', },
        ]],
    });
}

//清楚dialog中的选项
function clearDGLocation() {
    $('#dgLocationId').textbox('setValue', '');
    $('#dgLocationFeatures').textbox('setValue', '');
    $('#dgLocationName').textbox('setValue', '');
    $('.errorMsg').text('');
}


//初始化点击按钮、更改下拉框事件
function initLocationClick() {
    //点击Search按钮
    $('#btnLocationSearch').click(function () {
        reloadLocationTable();//重新加载表格
    });
    //点击清除
    $('#btnLocationClear').click(function () {
        $('#searchLocationFeatures').textbox('setValue', '');
        $('#searchLocationName').textbox('setValue', '');
    });
    //点击新增
    $('#btnLocationInsert').click(function () {
        clearDGLocation();
        tableClickInsert('#LocationDialog', '新增');
    });
    //点击修改
    $('#btnLocationUpdate').click(function () {
        var rows = $('#LocationTable').datagrid('getSelections');
        if (rows.length <= 0) { show('请选择任意一行！'); return; }
        clearDGLocation();
        $('#LocationDialog').dialog({
            closed: false,
            title: '修改',
        });
        $('#dgLocationId').textbox('setValue', rows[0].id);
        $('#dgLocationFeatures').textbox('setValue', rows[0].features);
        $('#dgLocationName').textbox('setValue', rows[0].name);
    });

}

//重新加载表格
function reloadLocationTable() {
    $('#LocationTable').datagrid('load', {
        ip4: $('#searchLocationFeatures').textbox('getValue'),
        name: $('#searchLocationName').textbox('getValue'),
    });
}