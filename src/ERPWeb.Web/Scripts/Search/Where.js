$(function () {
    initModelCheckBox();
    initWhereCombobox();
    initDialog();
    initClick();
    $('.easyui-combobox[title=Judge]').combobox('setValue', -1);
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
    var type = $('#WhereType').val();
    var data = ajaxSame('../../Model/GetModelInit', { type: type }, 'post', 'json');
    if (data.Code == 200) {
        var modelList = data.rows;
        $('#tdWhereModelList').empty();
        var $input = $('');
        for (var i in modelList) {
            $input = $('<input type="radio" name="Model" value="' + modelList[i].code + '" /><span>' + modelList[i].code + '&nbsp;&nbsp;</span>');
            $('#tdWhereModelList').append($input);
        }
    } else {
        show(data.Message);
    }
}

function initClick() {
    $('#tdWhereModelList input[name=Model]').change(function () {
        var model = $(this).val();
        //工位
        $('#tdWhereWSList').empty();
        var data = ajaxSame('../../WorkStation/GetWorkStationByModel', { model: model }, 'post', 'json');
        var $checkBox = $('');
        for (var i in data.rows) {
            $checkBox = $('<input type="checkbox" name="WorkStation" value="' + data.rows[i].id + '" /><span>' + data.rows[i].code + '&nbsp;&nbsp;</span>');
            $('#tdWhereWSList').append($checkBox);
        }
        //线别
        $('#tdWhereLineList').empty();
        data = ajaxSame('../../Cell/GetLineByModel', { model: model }, 'post', 'json');
        $checkBox = $('');
        for (var i in data.rows) {
            $checkBox = $('<input type="checkbox" name="Line" value="' + data.rows[i].id + '" /><span>#' + data.rows[i].name + '&nbsp;&nbsp;</span>');
            $('#tdWhereLineList').append($checkBox);
        }

        $('#tdWherePartList').empty();
        $('#tdWherePartList').append('<span style="color:red;font-weight:bold;">请先选择机种，再选择工位</span>');
    });

    $('#searchWhereMacName').click(function () {
        var model = '';
        $('#tdWhereModelList input[name=Model]').each(function (index, element) {
            if (element.checked) {
                model = element.value;
            }
        });
        var wsIdList = '';
        $('#tdWhereWSList input[name=WorkStation]').each(function (index, element) {
            if (element.checked) {
                if (wsIdList == 'NULL') wsIdList = '';
                wsIdList += ',' + element.value;
            }
        });
        var lineIdList = '';
        $('#tdWhereLineList input[name=Line]').each(function (index, element) {
            if (element.checked) {
                if (lineIdList == 'NULL') lineIdList = '';
                lineIdList += ',' + element.value;
            }
        });
        
        var param = { model: model, wsIdList: wsIdList, lineIdList: lineIdList };
        showSelectTableWithParam('../../Mac/SearchForTime', '#searchWhereMac', param);
    });
    

    $('#tdWhereWSList').on('click', 'input[name=WorkStation]', function () {
        var wsIdList = '';
        $('#tdWhereWSList input[name=WorkStation]').each(function (index, element) {
            if (element.checked) {
                if (wsIdList == 'NULL') wsIdList = '';
                wsIdList += ',' + element.value;
            }
        });
        $('#tdWherePartList').empty();

        data = ajaxSame('../../Part/GetPartByWS', { wsidList: wsIdList }, 'post', 'json');
        debugger;
        if (data.Code = 200) {
            $checkBox = $('');
            for (var i in data.rows) {
                $checkBox = $('<input type="checkbox" name="Part" value="' + data.rows[i].code + '" /><span>#' + data.rows[i].code + '&nbsp;&nbsp;</span>');
                $('#tdWherePartList').append($checkBox);
            }
        } else {
            show(data.Message);
        }
        
    });
}


function initDialog() {

    loadSelectTable('#searchWhereMac', '-', 870,
        function () {
            var rows = $('.easyui-datagrid[title="SearchMac"]').datagrid('getSelections');
            if (rows.length <= 0) { show('请选择任意一行！'); return; }
            var idList = '';
            var nameList = '';
            for (var i = 0; i < rows.length; i++) {
                idList += ',' + rows[i].id;
                nameList += ',' + rows[i].name;
            }
            $('#searchWhereMacName').val(nameList);
            $('#searchWhereMacName').siblings('input[title=value]').first().val(idList);
        }, null);
}


//修改日历框的显示格式
function formatter(date) {
    var year = date.getFullYear();
    var month = date.getMonth() + 1;
    var day = date.getDate();
    var hour = date.getHours();
    var min = date.getMinutes();
    var second = date.getSeconds();
    month = month < 10 ? '0' + month : month;
    day = day < 10 ? '0' + day : day;
    hour = hour < 10 ? '0' + hour : hour;
    min = min < 10 ? '0' + min : min;
    second = second < 10 ? '0' + second : second;
    var time = year + "-" + month + "-" + day + " " + hour + ":" + min + ":" + second;
    
    return time;
}
var time;
function parser(s) {
    var t;
    if (time && time != "") {
        t = time;
    }
    if (s.indexOf("(") > -1) {
        t = Date.parse(t);
    } else {
        t = Date.parse(s);
    }
    if (!isNaN(t)) {
        return new Date(t);
    } else {
        return new Date();
    }
}
