$(function () {
    initRalationUcClick();
    initRelationURTable();
    initRelationURDialog();
})

function initRelationURDialog() {
    $('#dgPartialUser').empty();
    var data = ajaxSame('/User/Search', null, 'post', 'html');
    $('#dgPartialUser').append(data);
    $('#dgPartialUser').dialog({
        closed: true,//默认关闭状态
        closable: true,//不提供关闭页面
        title: '',
        top: 0,
        width: 900,
        buttons: [{
            text: '确定',
            iconCls: 'icon-ok',
            handler: function () {
                var rows = $('.easyui-datagrid[title="SearchPartialUser"]').datagrid('getSelections');
                if (typeof rows == 'undefined') return;
                if (rows.length <= 0) { show('请选择任意一行！'); return; }
                $('#searchRelationURUserId').val(rows[0].UserId);
                $('#searchRelationURUserName').text(rows[0].UserNo + '-' + rows[0].UserName);
                $('#dgPartialUser').dialog({ closed: true, });
                loadRelationURRoleSelected(rows[0].UserId);
            }
        }, {
            text: '取消',
            iconCls: 'icon-cancel',
            handler: function () {
                $('#dgPartialUser').dialog({ closed: true, });
            }
        }],
    });
}

function initRelationURTable() {
    var code = '';
    var name = '';
    var isVisible = -1;
    var isEnable = -1;
    $('#selectRelationURTable').datagrid({
        idField: "Id",
        //分页
        fitColumns: true,
        singleSelect: false,//选择多行
        url: '/Role/GetRole',
        queryParams: { RoleCode: code, RoleName: name, IsVisible: isVisible, IsEnable: isEnable },
        pagination: true,
        rownumbers: true,
        pagePosition: 'bottom',
        pageSize: 10,
        pageNumber: 1,
        pageList: [2, 10, 15, 20, 30, 50],
        toolbar: '#searchRelationURToolbar',
        fitColumns: true,
        //数据格式
        columns: [[
            { checkbox: true, field: 'CreateRoleId', },
            { title: '序号', width: 100, field: 'Id', /*hidden: true*/ },
            { title: '编号', width: 100, field: 'RoleCode', },
            { title: '名称', width: 100, field: 'RoleName', },
            { title: '备注', width: 100, field: 'RoleRemark', },
            {
                title: '创建时间', width: 100, field: 'CreateTime', formatter: function (value, row, index) {
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
        onLoadSuccess: function () {
            var selectedList = $('#selectRaletionURSelected').val();
            var powerArray = selectedList.split(',');
            for (var i = 0; i < powerArray.length; i++) {
                if (powerArray[i] == '') continue;
                $(this).datagrid('selectRecord', powerArray[i]);
            }
            return;
        },
        onCheck: function (index, row) {
            var selectedList = $('#selectRaletionURSelected').val();
            var oldStr = ',' + row.Id;
            if (isContainsArr(selectedList, oldStr,',')) return;//当包含字串时，不再新增
            selectedList = selectedList + oldStr;
            $('#selectRaletionURSelected').val(selectedList);
            return;
        },
        onUncheck: function (index, row) {
            var selectedList = $('#selectRaletionURSelected').val();
            var oldStr = ',' + row.Id;
            //包含字串才替换
            if (isContainsArr(selectedList, oldStr, ',')) selectedList = arrStrRemoveStr(selectedList, oldStr, ',');
            $('#selectRaletionURSelected').val(selectedList);
            return;
        },
        onSelectAll: function (rows) {
            var selectedList = $('#selectRaletionURSelected').val();
            var oldStr = '';
            for (var i in rows) {
                oldStr = ',' + rows[i].Id;
                if (isContainsArr(selectedList, oldStr,',')) continue;
                selectedList = selectedList + oldStr;
            }
            $('#selectRaletionURSelected').val(selectedList);
            return;
        },
        onUnselectAll: function (rows) {
            console.info(rows);
            var selectedList = $('#selectRaletionURSelected').val();
            var oldStr = '';
            for (var i in rows) {
                oldStr = ',' + rows[i].Id;
                if (isContainsArr(selectedList, oldStr, ',')) selectedList = arrStrRemoveStr(selectedList, oldStr, ',');
            }
            $('#selectRaletionURSelected').val(selectedList);
            return;
        },
    });
}

function initRalationUcClick() {
    $('#searchRelationURUserId').click(function () {
        $('#dgPartialUser').dialog({
            closed: false,//默认关闭状态
            closable: false,//不提供关闭页面
        });
    });

    $('#searchRelationURSearch').click(function () {
        reloadRelationURTable();
    });

    $('#searchRelationURClear').click(function () {
        clearRelationUR();

    });

    $('#searchRelationURKeep').click(function () {
        var userId = $('#searchRelationURUserId').val();
        var roleList = $('#selectRaletionURSelected').val();
        var para = { UserNo: userId, RoleListStr: roleList };
        var data = ajaxSame('/UserInRole/SetRelationUR', para, 'post', 'json');
        if (typeof data == 'string') data = $.parseJSON(data)
        if (data.Code == 200) {
            clearRelationUR();
            show("保存成功!");
        } else {
            show(data.Message);
        }
    });
}


function reloadRelationURTable() {
    var code = $("#searchRelationURCode").textbox('getValue');
    var name = $('#searchRelationURName').textbox('getValue');
    var isvalid = $('#searchRelationURIsValid').combobox('getValue');
    var isenable = $('#searchRelationURIsEnable').combobox('getValue');
    
    $('#selectRelationURTable').datagrid('load', {
        RoleCode: code,
        RoleName: name,
        IsVisible: isvalid,
        IsEnable: isenable,
    });
}

function clearRelationUR() {
    $("#searchRelationURCode").textbox('setValue', '');
    $('#searchRelationURName').textbox('setValue', '');
    $('#searchRelationURIsValid').combobox('setValue', '-1');
    $('#searchRelationURIsEnable').combobox('setValue', '-1');
    $('#selectRelationURTable').datagrid('clearSelections');
    $('#searchRelationURUserId').val('');
    $('#searchRelationURUserName').text('');
    $('#selectRaletionURSelected').val('');
}

function loadRelationURRoleSelected(userid) {
    var para = { UserNo: userid };
    var data = ajaxSame('/UserInRole/GetRoleListStrByUserId', para, 'post', 'json');
    if (typeof data == 'string') data = $.parseJSON(data)
    if (data.Code == 200) {
        $('#selectRelationURTable').datagrid('clearSelections');
        $('#selectRaletionURSelected').val(data.Data);
        reloadRelationURTable();
    } else {
        show(data.Message);
    }
}
