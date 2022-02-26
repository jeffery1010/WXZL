//窗体加载
$(function () {

    initPartDialog();
    initModelCheckBox();
    initPartTable();
    initPartClick();

});

function initModelCheckBox() {
    var data = ajaxSame('../../Model/GetModelInit', { type: '' }, 'post', 'json');
    if (data.Code == 200) {
        $('#tdPartModel').empty();
        $('#dgPartModel').empty();
        var modelList = data.rows;
        var $input = $('');
        var $input1 = $('');
        for (var i in modelList) {
            $input = $('<input type="radio" name="Model" value="' + modelList[i].code + '" /><span>' + modelList[i].code + '&nbsp;&nbsp;</span>');
            $input1 = $('<input type="radio" name="Model1" value="' + modelList[i].code + '" /><span>' + modelList[i].code + '&nbsp;&nbsp;</span>');
            $('#tdPartModel').append($input);
            $('#dgPartModel').append($input1);
        }
    } else {
        show(data.Message);
    }
}


//验证输入-点击提交时
function checkPartClickSubmit(idList) {
    $(idList).each(function (index, element) {
        checkPartAttention($(this));
    });
}
//验证输入-更改提示信息
function checkPartAttention(element) {
    var id = $(element).attr('id');
    var value = $(element).textbox('getValue');
    var errorMsg = $(element).parents('tr').find('.errorMsg');
    if (id == 'dgPartFeatures') {
        if (checkValLenString(value, 1, 64).code == 0) errorMsg.text('编号必须在1-64个字符之间'); else errorMsg.text('');
    } else if (id == 'dgPartName') {
        if (checkValLenString(value, 1, 64).code == 0) errorMsg.text('名称必须在1-64个字符之间'); else errorMsg.text('');
    }
}


//初始化提示框
function initPartDialog() {
    $('#PartDialog').dialog({
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
                checkPartClickSubmit('#dgPartFeatures,#dgPartName');
                if (!IsSubmitDialog('#PartDialog')) { return; }

                var param = getParamPartDG();
                debugger;
                if (param.modelcode == -1) {
                    show('请选择机种！');
                    return;
                }
                if (param.wsid == -1) {
                    show('请选择工位！');
                    return;
                }
                if (param.code == '') {
                    show('请输入编码！');
                    return;
                }
                
                var url = $('#PartDialog').panel('options').title == '新增' ? '../../Part/InsertPart' : '../../Part/UpdatePart';
                var result = dialogClickKeep(url, param, 'post', 'json');
                if (typeof (result) == 'string') { result = $.parseJSON(result); }
                if (result.Code == 200) {
                    clearDGPart();
                    $('#PartDialog').dialog({ closed: true, });
                    reloadPartTable();//重新加载表格
                } else {
                    show(result.Message);
                }
            }
        }, {
            text: '取消',
            iconCls: 'icon-cancel',
            handler: function () {
                clearDGPart();
                $('#PartDialog').dialog({ closed: true, });
            }
        }],
    });
}

//初始化表格
function initPartTable() {
    var param = getParamPart();
    $('#PartTable').datagrid({
        //分页
        fitColumns: true,
        singleSelect: true,
        url: '../../Part/GetPartAll',
        queryParams: param,
        pagination: true,
        rownumbers: true,
        pagePosition: 'bottom',
        pageSize: 20,
        pageNumber: 1,
        pageList: [2, 10, 15, 20, 30, 50],
        toolbar: '#searchPartToolBar',
        fitColumns: true,
        //数据格式
        columns: [[
            { title: '序号', width: 50, field: 'id', fixed: true,/*hidden: true*/ },
            { title: '编号', width: 100, field: 'code', },
            { title: '机种', width: 100, field: 'modelid', hidden: true, },
            { title: '机种', width: 80, field: 'modelcode', },
            { title: '工位', width: 100, field: 'wsid', hidden: true, },
            { title: '工位', width: 80, field: 'wscode', },
            {
                title: '状态', width: 50, field: 'isvalid', hidden: true, formatter: function (value, row) {
                    return value == 1 ? '激活' : '失效';
                }
            },
            { title: '描述', field: 'detail', },
        ]],
    });
}

//清楚dialog中的选项
function clearDGPart() {
    $('#dgPartId').textbox('setValue', '');
    $('#dgPartCode').textbox('setValue', '');
    $('#dgPartDetail').textbox('setValue', '');
    $('#dgPartModel input[name=Model1]').attr('checked', false);
    $('#dgPartWorkStation').empty();
    $('#dgPartWorkStation').append('<span style="color:red;font-weight:bold;">请先选择机种，再选择工位</span>');
    $('.errorMsg').text('');
}


//初始化点击按钮、更改下拉框事件
function initPartClick() {
    //点击Search按钮
    $('#btnPartSearch').click(function () {
        reloadPartTable();//重新加载表格
    });
    //点击清除
    $('#btnPartClear').click(function () {
        $('#tdPartModel input[name=Model]').attr('checked', false);
        $('#tdPartWorkStation').empty();
        $('#tdPartWorkStation').append('<span style="color:red;font-weight:bold;">请先选择机种，再选择工位</span>');
        $('#searchPartCode').textbox('setValue', '');
    });
    //点击新增
    $('#btnPartInsert').click(function () {
        clearDGPart();
        tableClickInsert('#PartDialog', '新增');
    });
    //点击修改
    $('#btnPartUpdate').click(function () {
        var rows = $('#PartTable').datagrid('getSelections');
        if (rows.length <= 0) { show('请选择任意一行！'); return; }
        clearDGPart();
        $('#PartDialog').dialog({
            closed: false,
            title: '修改',
        });
        $('#dgPartId').textbox('setValue', rows[0].id);
        $('#dgPartCode').textbox('setValue', rows[0].code);
        $('#dgPartDetail').textbox('setValue', rows[0].detail);
        //选中机种
        var modelcode = '';
        $('#dgPartModel input[name=Model1]').each(function (index, element) {
            if (element.value == rows[0].modelcode) {
                modelcode = rows[0].modelcode;
                element.checked = true;
            }
        });
        appendPartWorkStation(modelcode, 'WorkStation1', 'dgPartWorkStation');
        $('#dgPartWorkStation input[name=WorkStation1]').each(function (index, element) {
            if (element.value == rows[0].wsid) {
                element.checked = true;
            }
        });
    });
    $('#tdPartModel input[name=Model]').change(function () {
        var model = $(this).val();
        appendPartWorkStation(model, 'WorkStation', 'tdPartWorkStation');
    });
    $('#dgPartModel input[name=Model1]').change(function () {
        var model = $(this).val();
        appendPartWorkStation(model, 'WorkStation1', 'dgPartWorkStation');
    });
}

//重新加载表格
function reloadPartTable() {
    var param = getParamPart();
    $('#PartTable').datagrid('load', param);
}

function getParamPart() {
    var code = $('#searchPartCode').textbox('getValue');
    var modelid = -1;
    var wsid = -1;
    var isvalid = -1;
    $('#tdPartModel input[name=Model]').each(function (index, element) {
        if (element.checked) {
            modelid = element.value;
        }
    });
    $('#tdPartWorkStation input[name=WorkStation]').each(function (index, element) {
        if (element.checked) {
            wsid = element.value;
        }
    });
    var param = { code: code, wsid: wsid, modelcode: modelid, isvalid: isvalid };
    return param;
}

function getParamPartDG() {
    var id = $('#dgPartId').textbox('getValue');
    var code = $('#dgPartCode').textbox('getValue');
    var detail = $('#dgPartDetail').textbox('getValue');
    var modelid = -1;
    var wsid = -1;
    var isvalid = 1;
    $('#dgPartModel input[name=Model1]').each(function (index, element) {
        if (element.checked) {
            modelid = element.value;
        }
    });
    $('#dgPartWorkStation input[name=WorkStation1]').each(function (index, element) {
        if (element.checked) {
            wsid = element.value;
        }
    });
    var param = { id:id,code: code,detail:detail, wsid: wsid, modelcode: modelid, isvalid: isvalid };
    return param;
}

function appendPartWorkStation(modelcode,redioname,tdid) {
    $('#' + tdid).empty();
    var data = ajaxSame('../../WorkStation/GetWorkStationByModel', { model: modelcode }, 'post', 'json');
    var $checkBox = $('');
    for (var i in data.rows) {
        $checkBox = $('<input type="radio" name="' + redioname+'" value="' + data.rows[i].id + '" /><span>' + data.rows[i].code + '&nbsp;&nbsp;</span>');
        $('#' + tdid).append($checkBox);
    }
}