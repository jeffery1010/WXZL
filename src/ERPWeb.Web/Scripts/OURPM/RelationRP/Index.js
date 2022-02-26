$(function () {
    initRalationUcClick();
    initRelationRPTable();
    initRelationRPDialog();
})

function initRelationRPDialog() {
    $('#dgPartialRole').empty();
    var data = ajaxSame('/Role/Search', null, 'post', 'html');
    $('#dgPartialRole').append(data);
    $('#dgPartialRole').dialog({
        closed: true,//默认关闭状态
        closable: true,//不提供关闭页面
        title: '',
        top: 0,
        width: 900,
        buttons: [{
            text: '确定',
            iconCls: 'icon-ok',
            handler: function () {
                var rows = $('.easyui-datagrid[title="SearchPartialRole"]').datagrid('getSelections');
                if (typeof rows == 'undefined') return;
                if (rows.length <= 0) { show('请选择任意一行！'); return; }
                $('#searchRelationRPRoleId').val(rows[0].Id);
                $('#searchRelationRPRoleName').text(rows[0].RoleCode + '-' + rows[0].RoleName);
                $('#dgPartialRole').dialog({ closed: true, });
                loadRelationRPRoleSelected(rows[0].Id);
            }
        }, {
            text: '取消',
            iconCls: 'icon-cancel',
            handler: function () {
                $('#dgPartialRole').dialog({ closed: true, });
            }
        }],
    });
}

function initRelationRPTable() {
    var code = '';
    var name = '';
    var isVisible = -1;
    var isEnable = -1;
    $('#selectRelationRPTable').datagrid({
        singleSelect: false,
        url: '/Power/GetPower',
        queryParams: { PowerCode: code, PowerName: name, IsVisible: isVisible, IsEnable: isEnable },
        idField: 'Id',
        animate: true,
        pagination: true,
        rownumbers: true,
        pagePosition: 'bottom',
        pageSize: 10,
        pageNumber: 1,
        fitColumns: true,
        pageList: [2, 10, 15, 20, 30, 50],
        toolbar: '#searchPowerToolBar',
        //数据格式
        columns: [[
            { checkbox: true, field: 'CreateRoleId', fixed: true, },
            { title: '序号', width: 100, field: 'Id', hidden: true, },
            { title: '编号', width: 100, field: 'PowerCode', fixed: true,},
            { title: '名称', width: 100, field: 'PowerName', fixed: true ,},
            { title: '备注', width: 100, field: 'PowerRemark', fixed: true,},
            {
                title: '创建时间', width: 100, field: 'CreateTime', fixed: true,
                formatter: function (value, row, index) {
                    return toDateStr(value);
                    //var time = new Date(parseInt(value.slice(6)));
                    //return time.getFullYear() + '-' + time.getMonth() + '-' + time.getDate() +
                    //    ' ' + time.getHours() + ':' + time.getMinutes() + ':' + time.getSeconds();
                }
            },
            { title: '创建人', width: 100, field: 'UserName', fixed: true, },
            {
                title: '是否可见', width: 100, field: 'IsVisible',
                formatter: function (value, row, index) {
                    if (value == 0) return "不可见";
                    else if (value == 1) return "可见";
                }
            },
            {
                title: '是否激活', width: 100, field: 'IsEnable', fixed: true, formatter: function (value, row, index) {
                    if (value == 0) return "未激活";
                    else if (value == 1) return "已激活";
                }
            },
        ]],
        onLoadSuccess: function () {
            var selectedList = $('#selectRelationRPSelected').val();
            var powerArray = selectedList.split(',');
            for (var i = 0; i < powerArray.length; i++) {
                if (powerArray[i] == '') continue;
                $(this).datagrid('selectRecord', powerArray[i]);
            }
        },
        onCheck: function (index, row) {
            var selectedList = $('#selectRelationRPSelected').val();
            var oldStr = ',' + row.Id;
            if (isContainsArr(selectedList, oldStr,',')) return;//当包含字串时，不再新增
            selectedList = selectedList + oldStr;
            $('#selectRelationRPSelected').val(selectedList);
        },
        onUncheck: function (index, row) {
            var selectedList = $('#selectRelationRPSelected').val();
            var oldStr = ',' + row.Id;
            if (isContainsArr(selectedList, oldStr, ',')) selectedList = arrStrRemoveStr(selectedList, oldStr, ',');
            $('#selectRelationRPSelected').val(selectedList);
        },
        onSelectAll: function (rows) {
            var selectedList = $('#selectRelationRPSelected').val();
            var oldStr = '';
            for (var i in rows) {
                oldStr = ',' + rows[i].Id;
                if (isContainsArr(selectedList, oldStr,',')) continue;
                selectedList = selectedList + oldStr;
            }
            $('#selectRelationRPSelected').val(selectedList);
        },
        onUnselectAll: function (rows) {
            var selectedList = $('#selectRelationRPSelected').val();
            var oldStr = '';
            for (var i in rows) {
                oldStr = ',' + rows[i].Id;
                if (isContainsArr(selectedList, oldStr, ',')) selectedList = arrStrRemoveStr(selectedList, oldStr, ',');
            }
            $('#selectRelationRPSelected').val(selectedList);
        },
    });
}

function initRalationUcClick() {
    $('#searchRelationRPRoleId').click(function () {
        $('#dgPartialRole').dialog({
            closed: false,//默认关闭状态
            closable: false,//不提供关闭页面
        });
    });

    $('#searchRelationRPSearch').click(function () {
        reloadRelationRPTable();
    });

    $('#searchRelationRPClear').click(function () {
        clearRelationRP();

    });

    $('#searchRelationRPKeep').click(function () {
        var roleId = $('#searchRelationRPRoleId').val();
        var powerList = $('#selectRelationRPSelected').val();
        var para = { RoleId: roleId, PowerListStr: powerList };
        var data = ajaxSame('/PowerInRole/SetRelationRP', para, 'post', 'json');
        if (typeof data == 'string') data = $.parseJSON(data)
        if (data.Code == 200) {
            clearRelationRP();
        } else {
            show(data.Message);
        }
    });
}


function reloadRelationRPTable() {
    var code = $("#searchRelationRPCode").textbox('getValue');
    var name = $('#searchRelationRPName').textbox('getValue');
    var isvalid = $('#searchRelationRPIsValid').combobox('getValue');
    var isenable = $('#searchRelationRPIsEnable').combobox('getValue');

    $('#selectRelationRPTable').datagrid('load', {
        RoleCode: code,
        RoleName: name,
        IsVisible: isvalid,
        IsEnable: isenable,
    });
}

function clearRelationRP() {
    $("#searchRelationRPCode").textbox('setValue', '');
    $('#searchRelationRPName').textbox('setValue', '');
    $('#searchRelationRPIsValid').combobox('setValue', '-1');
    $('#searchRelationRPIsEnable').combobox('setValue', '-1');
    $('#selectRelationRPTable').datagrid('clearSelections');
    $('#searchRelationRPRoleId').val('');
    $('#searchRelationRPRoleName').text('');
    $('#selectRelationRPSelected').val('');
}

function loadRelationRPRoleSelected(roleid) {
    var para = { RoleId: roleid };
    var data = ajaxSame('/PowerInRole/GetPowerListStrByRoleId', para, 'post', 'json');
    if (typeof data == 'string') data = $.parseJSON(data)
    if (data.Code == 200) {
        $('#selectRelationRPTable').datagrid('clearSelections');
        $('#selectRelationRPSelected').val(data.Data);
        reloadRelationRPTable();
    } else {
        show(data.Message);
    }
}
