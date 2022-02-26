$(function () {
    InitIndexDropDownList();
    var ddl = $('#IndexIndexRelationORP');
    var firstId = ddl.combobox('getValue');

    InitIndexIndexExit();
    loadCenterTabs();
    loadDefaultBackGroundImage();
    loadLeftTree(firstId);
    loadIndexDropDownList();

    //显示出是第几项
    ddl.combobox('setValue', firstId);
});

//Index默认初始化RelationORP
function InitIndexDropDownList() {
    loadDropDownList('/Organization/GetDDLOrganizationByUserNo', '#IndexIndexRelationORP');
}
//Index加载点击选择事件
function loadIndexDropDownList() {
    $('#IndexIndexRelationORP').combobox({
        onSelect: function (node) {
            //console.info(node);
            loadLeftTree(node.Id);
        }
    });
}
//Index初始化退出按钮
function InitIndexIndexExit() {
    $('#IndexIndexExit').click(function () {
        var result = ajaxSame('/Index/Exit', {}, 'get', 'text');
        if (result=='YES') {
            window.location.href = '/Login/Index';
        } else {
            show(result);
        }
    });
}
//加载默认背景
function loadDefaultBackGroundImage() {
    $('#ii_rightTab').tabs('add', {
        title: '欢迎',
        content: '<img src="/Image/homepage.jpg" style="width:100%;height:99%;" />',
        closable: false,
        fit: true,
    });
}
//加载左侧树列表
function loadLeftTree(organizationId) {
    $('#ii_leftTree').tree({
        url: '/Index/GetMenuTree',
        queryParams: { organizationId: organizationId },
        onClick: function (node) {
            //如果是根节点
            if (node.children.length >0) return;
            if ($('#ii_rightTab').tabs('exists', node.text)) {
                //有这个页面则跳转到这个页面
                $('#ii_rightTab').tabs('select', node.text);
            } else {
                $('#ii_rightTab').tabs('add', {
                    title: node.text,
                    content: "<iframe marginheight='0' marginwidth='0' frameborder='0' name='ifrm3' width='100%' height='97%' src='" + node.url + "'></iframe>",
                    //href: node.url,
                    closable: true,
                    fit: true,
                });
            }

        },
    });
    
}
//右键关闭选项卡
function loadCenterTabs() {
    $('#ii_rightTab').tabs({
        onContextMenu: function (e, title, index) {
            if (title == '欢迎') return;
            if (window.confirm('Do you want to close 【' + title + '】 ?')) {
                $('#ii_rightTab').tabs('close', title);
            }
        },
    });
}