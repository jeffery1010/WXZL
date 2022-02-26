$(function () {
    initModelCheckBox();
    initWhereCombobox();
    loadByOPIDWhere();
    initByOPIDClick();
});

var comboboxJson = [{}];
function initWhereCombobox() {
    comboboxJson = ajaxSame('../../Model/GetModelComboboxNone', null, 'post', 'json');
    $('.easyui-combobox[title="Model"]').each(function () {
        loadDropDownListObj(this, comboboxJson, 'id', 'name', '2');
    });
    //var isValidJson = [{ name: '--全部--', id: '-1' }, { name: '失效', id: '0' }, { name: '激活', id: '1' }];
    //$('.easyui-combobox[title="isvalid"]').each(function () {
    //    loadDropDownListObj(this, isValidJson, 'id', 'name', '-1');
    //});
}

function initModelCheckBox() {
    var data = ajaxSame('../../Model/GetModelInit', { type: 'OP' }, 'post', 'json');
    if (data.Code == 200) {
        var modelList = data.rows;
        $('#tdByOPIDModelList').empty();
        var $input = $('');
        for (var i in modelList) {
            $input = $('<input type="radio" name="Model" value="' + modelList[i].code + '" /><span>' + modelList[i].code + '&nbsp;&nbsp;</span>');
            $('#tdByOPIDModelList').append($input);
        }
    } else {
        show(data.Message);
    }
}

function initByOPIDTable() {
    var param = getParamByOPID();
    if (param.Model == 'NULL') {
        show('请选择机种');
        return;
    }
    $('#SearchByOPIDTable').datagrid({
        //分页
        fitColumns: true,
        singleSelect: false,
        idField: "id",
        url: '../../Search/ForOP',
        queryParams: param,
        pagination: true,
        rownumbers: true,
        pagePosition: 'bottom',
        pageSize: 10,
        pageNumber: 1,
        pageList: [1, 10, 20, 30, 50, 100],
        fitColumns: true,
        //数据格式
        columns: [[
            { title: 'id', width: 50, field: 'id', hidden: true, },
            { title: 'xpathDownload', width: 50, field: 'xpathDownload', hidden: true, },
            { title: 'checkbox', width: 50, field: 'checkbox', checkbox: true },
            {
                title: '显示', field: 'xpathShow', formatter: function (value, row) {
                    return '<img src="' + value + '" style="height:80px;width:120px;" onmouseover="imgMouseOver(this);" onmouseout="imgMouseOut(this);" onmousemove="imgMouseMove(this);" />';
                },
            },
            { title: '机种', width: 50, field: 'model', hidden: false, },
            { title: '线别', width: 50, field: 'cellid', hidden: false, },
            { title: 'OPID', width: 150, field: 'opid', hidden: false, },
            { title: '工位', width: 150, field: 'wsname', hidden: false, },
            { title: '机台', width: 150, field: 'macname', hidden: false, },
            { title: '相机', width: 150, field: 'camel', hidden: false, },
            { title: '标识', width: 150, field: 'inputtime', hidden: false, },
            {
                title: '变更时间', width: 150, field: 'chtime', formatter: function (value, row) {
                    return toDateStr(value);
                }
            },
        ]],
        onLoadSuccess: function () {
            var selectedList = $('#ByOPIDIdList').val();
            var powerArray = selectedList.split(',');
            for (var i = 0; i < powerArray.length; i++) {
                if (powerArray[i] == '') continue;
                $(this).datagrid('selectRecord', powerArray[i]);
            }
        },
        onCheck: function (index, row) {
            var selectedList = $('#ByOPIDIdList').val();
            var oldStr = ',' + row.id;
            if (isContainsArr(selectedList, oldStr, ',')) return;//当包含字串时，不再新增
            selectedList = selectedList + oldStr;
            $('#ByOPIDIdList').val(selectedList);
        },
        onUncheck: function (index, row) {
            var selectedList = $('#ByOPIDIdList').val();
            var oldStr = ',' + row.id;
            //包含字串才替换
            if (isContainsArr(selectedList, oldStr, ',')) selectedList = arrStrRemoveStr(selectedList, oldStr, ',');
            $('#ByOPIDIdList').val(selectedList);
        },
        onSelectAll: function (rows) {
            //用户选中所有的行
            var selectedList = $('#ByOPIDIdList').val();
            var oldStr = '';
            for (var i in rows) {
                oldStr = ',' + rows[i].id;
                if (isContainsArr(selectedList, oldStr, ',')) continue;
                selectedList = selectedList + oldStr;
            }
            $('#ByOPIDIdList').val(selectedList);
        },
        onUnselectAll: function (rows) {
            //用户取消选中所有的行
            console.info(rows);
            var selectedList = $('#ByOPIDIdList').val();
            var oldStr = '';
            for (var i in rows) {
                oldStr = ',' + rows[i].id;
                if (isContainsArr(selectedList, oldStr, ',')) selectedList = arrStrRemoveStr(selectedList, oldStr, ',');
            }
            $('#ByOPIDIdList').val(selectedList);

        },
    });
}

function initByOPIDClick() {
    $('#btnByOPIDSearch').click(function () {
        initByOPIDTable();
    });
    $('#btnByOPIDClear').click(function () {
        $('#SearchByOPIDWhere input[name=Model]').attr('checked', false);

        $('#SearchByOPIDWhere td.WorkStation').empty();
        $('#SearchByOPIDWhere td.WorkStation').append('<span style="color:red;font-weight:bold;">请先选择机种，再选择工位</span>');

        $('#SearchByOPIDWhere input[name=OPID]').first().val('');
        $('#ByOPIDIdList').val('');
        $('#SearchByOPIDTable').datagrid('clearSelections');
        $('#SearchByOPIDTable').datagrid('loadData', { total: 0, rows: [] });
    });
    $('#btnByOPIDDownload').click(function () {
        var rows = $('table#SearchByOPIDTable').datagrid('getSelections');
        debugger;
        if (rows.length < 1) { show('请选择至少一行！'); return; }
        var params = getParamByOPID();
        var model = params.Model;
        var arrStr = '';
        var idStr = '';
        for (var i in rows) {
            arrStr += ',' + rows[i].xpathDownload;
            idStr += ',' + rows[i].id;
        }
        console.info(arrStr);
        var data = ajaxSame('../../Search/DownloadZip', { model: model, idStr: idStr, pathStr: arrStr }, 'post', 'json');
        if (data.Code == 200) {
            window.location = '../../Search/ReturnZip' + data.Message;
        } else {
            show(data.Message);
        }

    });
    $('#btnByOPIDDownloadAll').click(function () {
        var params = getParamByOPID();
        var data = ajaxSame('../../Search/DownloadAllForOP', params, 'post', 'json');
        if (data.Code == 200) {
            window.location = '../../Search/ReturnZip' + data.Message;
        } else {
            show(data.Message);
        }

    });
    $('#SearchByOPIDWhere input[name=Model]').change(function () {
        var model = $(this).val();
        $('#SearchByOPIDWhere td.WorkStation').empty();
        var data = ajaxSame('../../WorkStation/GetWorkStationByModel', { model: model }, 'post', 'json');
        var $checkBox = $('');
        for (var i in data.rows) {
            $checkBox = $('<input type="checkbox" name="WorkStation" value="' + data.rows[i].id + '" /><span>' + data.rows[i].code + '&nbsp;&nbsp;</span>');
            $('#SearchByOPIDWhere td.WorkStation').append($checkBox);
        }
    });
}

function loadByOPIDWhere() {
    var data = ajaxSame('../../Search/Where', null, 'get', 'html');
    $('div.divWhere').append(data);

}

function getParamByOPID() {
    var model = 'NULL';
    var workstation = '';
    $('#SearchByOPIDWhere input[name=Model]').each(function (index, element) {
        if (element.checked) {
            model = element.value;
        }
    });
    $('#SearchByOPIDWhere input[name=WorkStation]').each(function (index, element) {
        if (element.checked) {
            if (workstation == 'NULL') workstation = '';
            workstation += ',' + element.value;
        }
    });
    var opid = $('#SearchByOPIDWhere input[name=OPID]').first().val();
    var param = { Model: model, Mt: workstation, OPID: opid };
    return param;
}

function imgMouseOver(obj) {
    var src = $(obj).attr('src');
    $('div.max').find('img:first').attr('src', src);
    $('div.max').css({ "display": "block" });
}
function imgMouseOut(obj) {
    $('div.max').css({ "display": "none" });
}
function imgMouseMove(obj) {
    var docId = 'imgByOPIDMaxShow';
    var bWidth = document.body.scrollWidth * 0.6;
    var arr = getWHPX(docId);
    var x = 0;
    var width = 0;
    var height = 0;
    if (arr[0] > bWidth) {
        x = bWidth / arr[0];
        width = arr[0] * x;
        height = arr[1] * x;
    } else {
        x = arr[0] / bWidth;
        width = arr[0] / x;
        height = arr[1] / x;
    }
    $('div.max').find('img:first').css({ "width": width + "px", "height": height + "px" });
    
}


