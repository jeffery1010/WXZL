$(function () {
    loadForTimeWhere();
    initForTimeClick();
});

function initForTimeTable() {
    var param = getParamForTime();
    $('#SearchForTimeTable').datagrid({
        //分页
        fitColumns: true,
        singleSelect: false,
        idField: "id",
        url: '../../Search/ForTime',
        queryParams: param,
        pagination: true,
        rownumbers: true,
        pagePosition: 'bottom',
        pageSize: 10,
        pageNumber: 1,
        pageList: [1, 10, 20, 30,50,100],
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
            { title: '位置', width: 150, field: 'part', hidden: false, },
            {
                title: '生产时间', width: 150, field: 'inputtime', formatter: function (value, row) {
                    return timeStrFormatDetail(value);
                } },
            {
                title: '变更时间', width: 150, field: 'chtime', formatter: function (value, row) {
                    return toDateStr(value);
                }
            },
        ]],
        onLoadSuccess: function () {
            var selectedList = $('#FotTimeIdList').val();
            var powerArray = selectedList.split(',');
            for (var i = 0; i < powerArray.length; i++) {
                if (powerArray[i] == '') continue;
                $(this).datagrid('selectRecord', powerArray[i]);
            }
        },
        onCheck: function (index, row) {
            var selectedList = $('#FotTimeIdList').val();
            var oldStr = ',' + row.id;
            if (isContainsArr(selectedList, oldStr, ',')) return;//当包含字串时，不再新增
            selectedList = selectedList + oldStr;
            $('#FotTimeIdList').val(selectedList);
        },
        onUncheck: function (index, row) {
            var selectedList = $('#FotTimeIdList').val();
            var oldStr = ',' + row.id;
            //包含字串才替换
            if (isContainsArr(selectedList, oldStr, ',')) selectedList = arrStrRemoveStr(selectedList, oldStr, ',');
            $('#FotTimeIdList').val(selectedList);
        },
        onSelectAll: function (rows) {
            //用户选中所有的行
            var selectedList = $('#FotTimeIdList').val();
            var oldStr = '';
            for (var i in rows) {
                oldStr = ',' + rows[i].id;
                if (isContainsArr(selectedList, oldStr, ',')) continue;
                selectedList = selectedList + oldStr;
            }
            $('#FotTimeIdList').val(selectedList);
        },
        onUnselectAll: function (rows) {
            //用户取消选中所有的行  199-2843-2581
            console.info(rows);
            var selectedList = $('#FotTimeIdList').val();
            var oldStr = '';
            for (var i in rows) {
                oldStr = ',' + rows[i].id;
                if (isContainsArr(selectedList, oldStr, ',')) selectedList = arrStrRemoveStr(selectedList, oldStr, ',');
            }
            $('#FotTimeIdList').val(selectedList);
            
        },
    });
}

function initForTimeClick() {
    $('#btnForTimeSearch').click(function () {
        var $table = $('div#indexForTime').siblings('div.divWhere').first().children('table');

        var model = '';
        $table.find('input[name=Model]').each(function (index, element) {
            if (element.checked) {
                model = element.value;
            }
        });
        if (model == '') {
            show('请先选择机种');
            return;
        } 
        initForTimeTable();
    });
    $('#btnForTimeClear').click(function () {
        var $table = $(this).siblings('div.divWhere').first().children('table');
        $table.find('input[name=Model]').attr('checked', false);
        //工位
        $table.find('#tdWhereWSList').empty();
        $table.find('#tdWhereWSList').append('<span style="color:red;font-weight:bold;">请先选择机种</span>');
        //线别
        $table.find('#tdWhereLineList').empty();
        $table.find('#tdWhereLineList').append('<span style="color:red;font-weight:bold;">请先选择机种</span>');
        //机台
        $table.find('input[title=Mac]').first().val('');
        $table.find('input[title=Mac]').first().siblings('input[title=value]').first().val('');
        //相机
        $table.find('.easyui-textbox[title=camera]').first().textbox('setValue', '');
        //位置
        $table.find('#tdWherePartList').empty();
        $table.find('#tdWherePartList').append('<span style="color:red;font-weight:bold;">请先选择机种，再选择工位</span>');
        //结果
        $table.find('.easyui-combobox[title=Judge]').first().combobox('setValue','-1');

        $table.find('.easyui-datetimebox[title=timeBegin]').first().datetimebox('setValue','');
        $table.find('.easyui-datetimebox[title=timeEnd]').first().datetimebox('setValue', '');

        $('#FotTimeIdList').val('');
        $('#SearchForTimeTable').datagrid('clearSelections');
        $('#SearchForTimeTable').datagrid('loadData', { total: 0, rows: [] });
    });
    $('#btnForTimeDownload').click(function () {
        var rows = $('table#SearchForTimeTable').datagrid('getSelections');
        debugger;
        if (rows.length < 1) { show('请选择至少一行！'); return; }
        var params = getParamForTime();
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
    $('#btnForTimeDownloadAll').click(function () {
        var params = getParamForTime();
        var data = ajaxSame('../../Search/DownloadAllForTime', params, 'post', 'json');
        if (data.Code == 200) {
            window.location = '../../Search/ReturnZip' + data.Message;
        } else {
            show(data.Message);
        }
    });
}

function loadForTimeWhere() {
    var data = ajaxSame('../../Search/Where', null, 'get', 'html');
    $('div.divWhere').append(data);
    
}

function getParamForTime() {
    var $table = $('div#indexForTime').siblings('div.divWhere').first().children('table');

    var model = '';
    $table.find('input[name=Model]').each(function (index, element) {
        if (element.checked) {
            model = element.value;
        }
    });
    var workstation = '';
    $table.find('input[name=WorkStation]').each(function (index, element) {
        if (element.checked) {
            if (workstation == 'NULL') workstation = '';
            workstation += ',' + element.value;
        }
    });
    var line = '';
    $table.find('input[name=Line]').each(function (index, element) {
        if (element.checked) {
            if (line == 'NULL') line = '';
            line += ',' + element.value;
        }
    });
    var mac = $table.find('input[title=Mac]').first().siblings('input[title=value]').first().val();
    //位置
    var location = '';
    $table.find('input[name=Part]').each(function (index, element) {
        if (element.checked) {
            if (location == 'NULL') location = '';
            location += ',' + element.value;
        }
    });

    var judge = $table.find('.easyui-combobox[title=Judge]').first().combobox('getValue');
    var camera = $table.find('.easyui-textbox[title=camera]').first().textbox('getValue');
    var timeBeginStr = $table.find('.easyui-datetimebox[title=timeBegin]').first().datetimebox('getValue');
    var timeEndStr = $table.find('.easyui-datetimebox[title=timeEnd]').first().datetimebox('getValue');
    var timeBegin = timeStrFormat(timeBeginStr);
    var timeEnd = timeStrFormat(timeEndStr);
    var param = { Model: model, Line: line, Mt: workstation, Mac: mac, Location: location, Judge: judge, timeBegin: timeBegin, timeEnd: timeEnd, Cameras: camera  };
    console.info(param);
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
    var docId = 'imgForTimeMaxShow';
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
