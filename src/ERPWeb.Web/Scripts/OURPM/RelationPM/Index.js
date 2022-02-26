$(function () {
    initRalationUcClick();
    initRelationPMTable();
    initRelationPMDialog();
})

function initRelationPMDialog() {
    $('#dgPartialPower').empty();
    var data = ajaxSame('/Power/Search', null, 'post', 'html');
    $('#dgPartialPower').append(data);
    $('#dgPartialPower').dialog({
        closed: true,//默认关闭状态
        closable: true,//不提供关闭页面
        title: '',
        top: 0,
        width: 900,
        buttons: [{
            text: '确定',
            iconCls: 'icon-ok',
            handler: function () {
                var rows = $('.easyui-datagrid[title="SearchPartialPower"]').datagrid('getSelections');
                if (typeof rows == 'undefined') return;
                if (rows.length <= 0) { show('请选择任意一行！'); return; }
                $('#searchRelationPMPowerId').val(rows[0].Id);
                $('#searchRelationPMPowerName').text(rows[0].UserNo + '-' + rows[0].UserName);
                $('#dgPartialPower').dialog({ closed: true, });
                loadRelationPMRoleSelected(rows[0].Id);
            }
        }, {
            text: '取消',
            iconCls: 'icon-cancel',
            handler: function () {
                $('#dgPartialPower').dialog({ closed: true, });
            }
        }],
    });
}

function initRelationPMTable() {
    var code = '';
    var name = '';
    var url = '';
    var isVisible = -1;
    var isEnable = -1;
    $('#selectRelationPMTable').treegrid({
        //分页
        fitColumns: true,
        singleSelect: false,
        url: '/Menu/GetMenu',
        queryParams: {
            Name: name, Code: code, XPath: url,
            IsVisible: isVisible, IsEnable: isEnable,
        },
        idField: 'Id',
        treeField: 'Name',
        animate: true,
        pagination: true,
        rownumbers: true,
        pagePosition: 'bottom',
        pageSize: 50,
        pageNumber: 1,
        pageList: [ 50,100,200,500,10000],
        toolbar: '#searchMenuToolBar',
        //数据格式
        columns: [[
            { checkbox: true, field: 'CreateRoleId', },
            { title: '序号', width: 30, field: 'Id', /*hidden: true*/ },
            { title: '名称', width: 170, field: 'Name', },
            { title: '编号', width: 100, field: 'Code', },
            { title: 'Url', width: 100, field: 'XPath', },
            { title: '备注', width: 100, field: 'Remark', },
            {
                title: '创建时间', width: 100, field: 'CreateTime',
                formatter: function (value, row, index) {
                    // value /Date(1563950624813)/  .slice(6)  从第6位开始(下标起始0)
                    var time = new Date(parseInt(value.slice(6)));
                    return time.getFullYear() + '-' + time.getMonth() + '-' + time.getDate() +
                        ' ' + time.getHours() + ':' + time.getMinutes() + ':' + time.getSeconds();
                }
            },
            { title: '父级编号', width: 100, field: 'ParentCode', },
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
            var selectedList = $('#selectRelationPMSelected').val();
            var powerArray = selectedList.split(',');
            for (var i = 0; i < powerArray.length; i++) {
                if (powerArray[i] == '') continue;
                //$(this).treegrid('selectRecord', powerArray[i]);
                $(this).treegrid('checkRow', powerArray[i]);
            }
            return;
        },
        onCheck: function (node, checked) {
            
            var selectedList = $('#selectRelationPMSelected').val();
            var oldStr = '';
            /*
            var childList = $(this).treegrid('getChildren',node.Id);
            if (childList.length > 0) {

                $.each(childList, function (index, currentValue) {
                    $('#selectRelationPMTable').treegrid('checkRow', currentValue.Id);
                    oldStr = ',' + currentValue.Id;
                    if (!isContainsArr(selectedList, oldStr,',')) selectedList = selectedList + oldStr;
                })
            }
            */
            var oldStr = ',' + node.Id;
            if (!isContainsArr(selectedList, oldStr, ',')) selectedList = selectedList + oldStr;
            $('#selectRelationPMSelected').val(selectedList);
            return;
        },
        onUncheck: function (index, row) {
            //index行号(0开始)  row当前选中的行   row.XXX行中的某一条属性
            var selectedList = $('#selectRelationPMSelected').val();
            var oldStr = ',' + index.Id;
            //包含字串才替换
            if (isContainsArr(selectedList, oldStr, ',')) {
                selectedList = arrStrRemoveStr(selectedList, oldStr, ',');
            }
            $('#selectRelationPMSelected').val(selectedList);
            return;
        },
        onSelectAll: function (rows) {
            var selectedList = $('#selectRelationPMSelected').val();
            var oldStr = '';
            for (var i in rows) {
                oldStr = ',' + rows[i].Id;
                if (isContainsArr(selectedList, oldStr,',')) continue;
                selectedList = selectedList + oldStr;
            }
            $('#selectRelationPMSelected').val(selectedList);
            return;
        },
        onUnselectAll: function (rows) {
            console.info(rows);
            var selectedList = $('#selectRelationPMSelected').val();
            var oldStr = '';
            for (var i in rows) {
                oldStr = ',' + rows[i].Id;
                if (isContainsArr(selectedList, oldStr, ',')) selectedList = arrStrRemoveStr(selectedList, oldStr, ',');
            }
            $('#selectRelationPMSelected').val(selectedList);
            return;
        },
    });
}

function initRalationUcClick() {
    $('#searchRelationPMPowerId').click(function () {
        $('#dgPartialPower').dialog({
            closed: false,//默认关闭状态
            closable: false,//不提供关闭页面
        });
    });

    $('#searchRelationPMSearch').click(function () {
        reloadRelationPMTable();
    });

    $('#searchRelationPMClear').click(function () {
        clearRelationPM();

    });

    $('#searchRelationPMKeep').click(function () {
        var powerId = $('#searchRelationPMPowerId').val();
        var menuList = $('#selectRelationPMSelected').val();
        var para = { PowerId: powerId, MenuListStr: menuList };
        var data = ajaxSame('/PowerInRes/SetRelationPM', para, 'post', 'json');
        if (typeof data == 'string') data = $.parseJSON(data)
        if (data.Code == 200) {
            clearRelationPM();
        } else {
            show(data.Message);
        }
    });
}


function reloadRelationPMTable() {
    var code = $("#searchRelationPMCode").textbox('getValue');
    var name = $('#searchRelationPMName').textbox('getValue');
    var url = '';
    var isvalid = $('#searchRelationPMIsValid').combobox('getValue');
    var isenable = $('#searchRelationPMIsEnable').combobox('getValue');

    $('#selectRelationPMTable').treegrid('load', {
        Code: code,
        Name: name,
        XPath: url,
        IsVisible: isvalid,
        IsEnable: isenable,
    });
}

function clearRelationPM() {
    $("#searchRelationPMCode").textbox('setValue', '');
    $('#searchRelationPMName').textbox('setValue', '');
    $('#searchRelationPMIsValid').combobox('setValue', '-1');
    $('#searchRelationPMIsEnable').combobox('setValue', '-1');
    $('#selectRelationPMTable').treegrid('clearSelections');
    $('#searchRelationPMPowerId').val('');
    $('#searchRelationPMPowerName').text('');
    $('#selectRelationPMSelected').val('');
}

function loadRelationPMRoleSelected(powerid) {
    var para = { PowerId: powerid };
    var data = ajaxSame('/PowerInRes/GetMenuListStrByPowerId', para, 'post', 'json');
    if (typeof data == 'string') data = $.parseJSON(data)
    if (data.Code == 200) {
        $('#selectRelationPMTable').treegrid('clearSelections');
        $('#selectRelationPMSelected').val(data.Data);
        reloadRelationPMTable();
    } else {
        show(data.Message);
    }
}



