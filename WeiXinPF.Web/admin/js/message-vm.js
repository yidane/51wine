

var MessageViewModel = function (params) {
    var self = this;
    this.params = ko.observable(params);
    this.shortmsg = ko.observable({
        title: '',
        content: '',
        createTime: '',
        fromSystem: {
            systemId: '',
            systemName: ''
        },
        fromUser: {
            userId: '',
            displayName: ''
        }
    });
    this.shotmsgList = ko.observableArray([{
        msg: {
            title: '',
            content: '',
            createTime: '',
            fromSystem: {
                systemId: '',
                systemName: ''
            },
            fromUser: {
                userId: '',
                displayName: ''
            }
        }
    , count: 0
    }]);
    this.shortmsgCount = ko.observable(0);
    this.historyShortMsg = ko.observableArray();
    this.thisUser = ko.observable({
        userId: '',
        displayName: ''
    });

    this.loadData = function () {
        self.loadShortMsgList();
    };

    this.loadShortMsg = function (afterLoadMsg) {
        var id = self.params().id;
        sup.ajax({
            url: '/Api/Message/GetNewMsg/' + id,
            type: 'get'

        }).done(function (json) {
            console.log('获取消息成功！');
            console.log(json);
            if (json) {
                self.shortmsg(json.msg);
                self.shortmsgCount(json.count);
                if (json.msg) self.thisUser(json.msg.toUser);

                if (typeof afterLoadMsg != 'undefined' && afterLoadMsg instanceof Function) {
                    afterLoadMsg();
                }
            }
        }).fail(function (a, b, c) {
            console.log(a);
        });
    };
    this.loadShortMsgList = function (afterLoadMsg) {
        self.shotmsgList(null);
        sup.ajax({
            url: '/Api/Message/GetAllLastNewMsg',
            type: 'get'

        }).done(function (json) {
            console.log('获取消息成功！');
            console.log(json);
            if (json) {
                var count = json.count;
                 
                 
                self.shortmsgCount(count);
                self.shotmsgList(json.msgs);
//                if (json.msgs.length > 0) self.thisUser(json.msgs[0].msg.toUser);

                if (typeof afterLoadMsg != 'undefined' && afterLoadMsg instanceof Function) {
                    afterLoadMsg();
                }
            }
        }).fail(function (a, b, c) {
            console.log(a);
        });
    };

    this.afterReadAllNewMsg = function() {

    };
    this.readAllNewMsg = function () {

        sup.ajax({
            url: '/Api/Message/ReadAllNewMsg?fromUserId=' + self.shortmsg().fromUser.userId,
            type: 'get'

        }).done(function (json) {
            console.log(json);
            console.log("阅读成功");
            self.afterReadAllNewMsg();

        }).fail(function (a, b, c) {
            console.log(a);
        });
    };


    this.shortmsgViewDetail = function (msgWithCount) {
        var id = msgWithCount.msg.id;
        self.LoadMessageDetail(id);
    };


    this.loadHistoryMsg = function () {
        var data = {
            fromUserId: self.shortmsg().fromUser.userId
        };
        sup.ajax({
            url: '/Api/Message/GetAllLastNewMsg',
            type: 'get',
            data: data

        }).done(function (json) {
            console.log(json);
            console.log("获取历史记录成功");
            self.historyShortMsg(json);
        }).fail(function (a, b, c) {
            console.log(a);
        });
    };
    this.toggleShowHistory = function ($obj) {
        if ($obj.is(":hidden")) {
            $obj.show(); //如果元素为隐藏,则将它显现
        } else {
            $obj.hide(); //如果元素为显现,则将其隐藏
        }

    };
    this.sendMsg = function (options) {
        var toUserId = self.shortmsg().fromUser.userId;






        top.sup.dialog({

            title: "消息发送",
            message: '<textarea  id="comments" class="form-control" rows="3" placeholder="请输入消息"></textarea>',
            cssClass: "dialog-center",
            closable: true,
            draggable: true,
            buttons: [
                    {
                        label: '取消',
                        action: function (dialogRef) {
                            //  alert(self.OrgName());
                            dialogRef.close();
                        }
                    },
                    {
                        icon: 'glyphicon glyphicon-send',
                        label: '发送',
                        cssClass: 'btn-primary',
                        hotkey: 13, // Enter.
                        action: function (dialogRef) {
                            var txt = dialogRef.getModalBody().find('#comments').val();
                            txt = $.trim(txt);
                            if (!txt || txt == "请输入消息") {
                                dialogRef.setClosable(false);
                                return false;
                            } else {
                                dialogRef.setClosable(true);
                                var msg = {
                                    title: '',
                                    content: txt,
                                    fromUserId: 0,
                                    toUserId: toUserId,
                                    fromSystemId: 0,
                                    toSystemId: 0
                                };
                                var data = JSON.stringify(msg);
                                sup.ajax({
                                    url: '/Api/Message/SendMsg',

                                    data: data

                                }).done(function (data) {
                                    if (options && options.success) {
                                        options.success();
                                        dialogRef.close();
                                    }
                                }).fail(function (a, b, c) {
                                    if (options && options.error) {
                                        options.error();
                                        dialogRef.close();
                                    }
                                });


                            }
                        }
                    }
            ]
        });
    };


    self.LoadMessage = function () {
        document.location.href = '/MessageAndAlarm/Message/MyMessage.aspx';
//        var $message = $('<iframe id="ifm" src="/dialog/myMessage.html" frameborder="0" scrolling="auto"></iframe>');
//        $message.css({
//            width: '100%',
//            height: '350px'
//        });
//        sup.dialog({
//            title: '消息提醒',
//            message: $message
//        });
//        self.resize();
    };
    self.LoadMessageDetail = function (id) {
        
        var $message = $('<iframe id="ifm" src="/dialog/myMessageDetail.html?id='+ id+'" frameborder="0" scrolling="auto"></iframe>');
        $message.css({
            width: '100%',
            height: '160px'
        });
        sup.dialog({
            title: '消息',
            message: $message,
            buttons: [
                {
                    label: '消息记录',
                    cssClass: 'btn-primary',
                    action: function (dialogRef) {
                        var $msgHistory = $("#ifm").contents().find("#MessageHistory");
                        //                        self.toggleShowHistory($msgHistory);
                        var mc = $(".modal-content");
                        if ($("#MessageHistory").length > 0) {
                            var dis = $("#MessageHistory").css("display");
                            if (dis == "none") {
                                mc.height(mc.height() + 190);
                                $("#MessageHistory").show();
                            }
                            else {
                                mc.height(mc.height() - 190);
                                $("#MessageHistory").hide();
                            }
                        }
                        else {
                            mc.height(mc.height() + 190);

                            $(".modal-footer").after($msgHistory.clone().show()[0].outerHTML);
                        }

                        //
                    }
                }, {
                    label: '回复',
                    cssClass: 'btn-success',
                    action: function (dialogRef) {


                        var viewModel = $("#ifm")[0].contentWindow.myMessageDetailViewModel;
                        viewModel.sendMsg({
                            success: function () {
                                dialogRef.close();

                                console.log("发送消息成功");
                                top.sup.message.success('发送成功！');
                            },
                            error: function () {
                                //                                dialogRef.close();
                                sup.message.error("发送失败。");
                            }
                        });

                    }
                }
            ],
            onhide: function (dialogRef) {
              
                var viewModel = $("#ifm")[0].contentWindow.myMessageDetailViewModel;
                if(  viewModel ){
                    self.loadData();

                    if (typeof myMessageTable != 'undefined') {
                        myMessageTable.ajax.reload();
                    }
                }
               

               
            }
        }
        );
        self.resize();
    };
    self.LoadEmailDetail = function () {
        var $message = $('<iframe id="ifm" src="/dialog/myEmailDetail.html" frameborder="0" scrolling="auto"></iframe>');
        $message.css({
            width: '100%',
            height: '160px'
        });
        sup.dialog({
            title: '邮件信息',
            message: $message
        });
    };
    self.LoadAlarm = function () {
        var $message = $('<iframe id="ifm" src="/dialog/myAlarm.html" frameborder="0" scrolling="auto"></iframe>');
        $message.css({
            width: '100%',
            height: '350px'
        });
        sup.dialog({
            title: '预警信息',
            message: $message
        });
        self.resize();
    };
    self.resize = function () {
        $(".modal-dialog").width(800);
    };
};