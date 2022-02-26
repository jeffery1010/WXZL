//提示信息
function show(message) {
    $.messager.show({
        title: '警告',
        msg: message,
        showType: 'show'
    });
}

//Dialog是否提交
function IsSubmitDialog(dialogId) {
    var result = true;
    $(dialogId).find('.errorMsg').each(function (index, element) {
        if ($(this).text().length > 0) {
            result = false;
            return false;
        }
    });
    return result;
}

//使用Ajax请求返回数据,同步
function ajaxSame(url, data, type, dataType) {
    var result = '';
    $.ajax({
        url: url,
        data: data,
        type: type,
        dataType: dataType,
        async: false,//非异步
        success: function (data) {
            result = data;
        },
        error: function (data) {
            result = data;
        },
    });
    return result;
}


//在表格上点击新增
function tableClickInsert(dialogId, dialogTitle) {
    $(dialogId).dialog({
        closed: false,
        title: dialogTitle,
    });
}
//在表格上点击修改
function tableClickUpdate(rows, url, dialogId, dialogTitle) {
    var data = { Id: rows[0].UserId };
    //console.info(rows[0].Id);
    $(dialogId).dialog({
        closed: false,
        title: dialogTitle,
    });
    return ajaxSame(url, data, 'post', 'json');
}
function tableClickUpdateById(id, url, dialogId, dialogTitle) {
    var data = { Id: id };
    //console.info(rows[0].Id);
    $(dialogId).dialog({
        closed: false,
        title: dialogTitle,
    });
    return ajaxSame(url, data, 'post', 'json');
}
//在表格上点击删除
function tableClickDelete(rows, url) {
    var data = { Id: rows[0].UserId };
    var result = '';
    if (window.confirm('删除该记录？')) result = ajaxSame(url, data, 'post', 'text');
    return result;
}
function tableClickDeleteById(id, url) {
    var data = { Id: id };
    var result = '';
    if (window.confirm('删除该记录？')) result = ajaxSame(url, data, 'post', 'text');
    return result;
}
//在Dialog上点击保存
function dialogClickKeep(url, data) {
    //if (!window.confirm('是否保存?')) { return ''; } else {
    return ajaxSame(url, data, 'post', 'text');
    //}
}
function dialogClickKeep(url, data, type, datatype) {
    //if (!window.confirm('是否保存?')) { return ''; } else {
    return ajaxSame(url, data, type, datatype);
    //}
}

//判断母串中是否包含字串 mother  son
function isContainsArr(mStr, sStr, mSplit) {
    sStr = sStr.replace(mSplit, '');
    var mArr = mStr.split(mSplit);
    var index = mArr.indexOf(sStr);
    if (index == -1) {
        return false;
    } else {
        return true;
    }
}

function arrStrRemoveStr(mStr, sStr, mSplit) {
    sStr = sStr.replace(mSplit, '');
    var mArr = mStr.split(mSplit);
    var result = '';
    for (var i = 0; i < mArr.length; i++) {
        if (mArr[i] == sStr) continue;
        if (mArr[i] == "") continue;
        result += "," + mArr[i];
    }
    return result;
}

//加载下拉框，异步
function loadDropDownList(url, id) {
    $.ajax({
        url: url,
        type: 'post',
        datatype: 'json',
        async: false,//
        error: function () { alert('error'); },
        success: function (data) {
            //有可能是字符串       Newtonsoft.Json.JsonConvert.SerializeObject(data)
            //有可能是Json         Json(data).Data
            debugger;
            if (typeof (data) == 'string') data = $.parseJSON(data);
            $(id).combobox({
                data: data,
                valueField: 'Id',
                textField: 'Text',
            });
            //选择第一项
            $(id).combobox('setValue', data[0].Id);
            /*
            var $select = $(id);
            var $option = $('<option selected="selected" value="-1">--选择--</option>');
            $select.empty();
            $select.append($option);
            for (var i in data) {
                $option = $('<option  value="' + data[i].Id + '">' + data[i].Text + '</option>');
                $select.append($option);
            }
            */
        }
    });
}
function loadDropDownListAll(id, data, bandId, bandText, selValue) {
    $(id).combobox({
        data: data,
        valueField: bandId,
        textField: bandText
    });
    $(id).combobox('setValue', selValue);
}

function loadDropDownListObj(obj, data, bandId, bandText, selValue) {
    $(obj).combobox({
        data: data,
        valueField: bandId,
        textField: bandText
    });
    $(obj).combobox('setValue', selValue);
}

function toDateStr(timestamp) {
    if (timestamp == null) return;
    if (timestamp == '') return;
    var date = new Date(parseInt(timestamp.replace("/Date(", "").replace(")/", ""), 10));
    Y = date.getFullYear() + '-';
    M = (date.getMonth() + 1 < 10 ? '0' + (date.getMonth() + 1) : date.getMonth() + 1) + '-';
    D = (date.getDate() < 10 ? '0' + (date.getDate()) : date.getDate()) + ' ';
    h = (date.getHours() < 10 ? '0' + (date.getHours()) : date.getHours()) + ':';
    m = (date.getMinutes() < 10 ? '0' + (date.getMinutes()) : date.getMinutes()) + ':';
    s = (date.getSeconds() < 10 ? '0' + (date.getSeconds()) : date.getSeconds());
    var NewDtime = Y + M + D + h + m + s;
    return NewDtime + '';
}
/**
 * url          地址  e.g:'../../C/A'
 * tabLable     要加载的父容器 e.g:'#Id'  or  '.Class[Attr=Value]'
 */
function showSelectTable(url, tabLable) {
    $.ajax({
        url: url,
        data: null,
        type: 'post',
        dataType: 'html',
        async: false,//非异步
       
        error: function (data) {
            show(data);
        },
        success: function (data) {
            $(tabLable).empty();
            $(tabLable).append(data);
            $(tabLable).dialog({
                closed: false,//默认关闭状态
                closable: false,//不提供关闭页面
            });
        },

    })
}

function showSelectTableWithParam(url, tabLable,data) {
    $.ajax({
        url: url,
        data: data,
        type: 'post',
        dataType: 'html',
        async: false,//非异步
        success: function (data) {
            $(tabLable).empty();
            $(tabLable).append(data);
            $(tabLable).dialog({
                closed: false,//默认关闭状态
                closable: false,//不提供关闭页面
            });
        },
        error: function (data) {
            show(data);
        },
    });
}

/**
 * tabLable     要加载的父容器 e.g:'#Id'  or  '.Class[Attr=Value]'
 * title        标题-String
 * width        宽度-int
 * yesHandler   保存处理方法  function(){...}
 * noHandler    取消处理方法  function(){...}
 */
function loadSelectTable(tabLable, title, width, yesHandler, noHandler) {
    $(tabLable).dialog({
        title: title,
        closed: true,//默认关闭状态
        closable: false,//不提供关闭页面
        top: 0,
        width: width,
        iconCls: 'icon-more',
        resizable: false,//是否可改变窗口大小
        modal: true,//模式化窗口，打开其他地方不可点击
        buttons: [{
            text: '确定',
            iconCls: 'icon-ok',
            handler: function () {
                if (typeof yesHandler == 'function') { yesHandler(); }
                $(tabLable).dialog({ closed: true, });
            },
        }, {
            text: '取消',
            iconCls: 'icon-cancel',
            handler: function () {
                if (typeof noHandler == 'function') { noHandler(); }
                $(tabLable).dialog({ closed: true, });
            },
        }],
    });
}

function timeStrFormat(dateStr) {
    if (dateStr == '') {
        return '1900/01/01 00:00:00';
    } else {
        return dateStr;
    }
    //var pre = dateStr.substr(6, 4) + '/' + dateStr.substr(0, 5);
    //var suf = dateStr.substr(11);
    //return pre + ' ' + suf;
}

function timeStrFormatDetail(dateStr) {
    dateStr = dateStr + '';
    if (dateStr == '') {
        return '1900/01/01 00:00:00';
    }
    var pre = '20' + dateStr.substr(0, 2) + '/' + dateStr.substr(2, 2) + '/' + dateStr.substr(4, 2);
    var suf = dateStr.substr(6, 2) + ':' + dateStr.substr(8, 2) +':'+ dateStr.substr(10, 2);
    return pre + ' ' + suf;
}

function DateTimeParser(s) {
    var t = Date.parse(s);
    if (!isNaN(t)) {
        return new Date(t);
    } else {
        return new Date(s + ":00:00");
    }
}

function getWHPX(docId) {
    var img = document.getElementById(docId);
    var h;
    var w;
    if (img.naturalWidth) {
        // HTML5 browsers
        w = img.naturalWidth;
        h = img.naturalHeight;
    } else {
        // IE 6/7/8
        var nImg = new Image();
        nImg.src = img.src;
        if (nImg.complete) {
            w = nImg.width;
            h = nImg.height;
        } else {
            nImg.onload = function () {
                w = nImg.width;
                h = nImg.height;
            }
        }
    }
    var arr = new Array();
    arr.push(w);
    arr.push(h);
    return arr;
}

