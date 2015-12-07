

var MessageViewModel = function (params) {
    var self = this;
    this.params = ko.observable(params);
    this.shortmsg = ko.observable({
        title: '',
        content: '',
        createTime: '',
        isShowButton:false,
        buttonText:'',
        buttonMutipleUrl:'',
        buttonUrl:'',

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
            isShowButton:false,
            buttonText:'',
            buttonMutipleUrl:'',
            buttonUrl:'',

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


    this.loadShortMsgList = function (afterLoadMsg) {
        //self.shotmsgList(null);
        $.ajax({
            url: '/WebServices/MessageWebService.asmx/GetAllLastNewMsg',
            type: 'get',
            dataType:'json'

        }).done(function (json) {
            console.log('获取消息成功！');
            console.log(json);
            if(json.isSuccess)
            {
                json=json.data;
                if (json) {
                    //var count = json.count;
                    var count = 0;
                    if(json.msgs)
                    {
                        count=json.msgs.length;
                    }
                    self.shortmsgCount(count);
                    self.shotmsgList(json.msgs);
//                if (json.msgs.length > 0) self.thisUser(json.msgs[0].msg.toUser);

                    if (typeof afterLoadMsg != 'undefined' && afterLoadMsg instanceof Function) {
                        afterLoadMsg();
                    }
                }
            }
        }).fail(function (a, b, c) {
            console.log(a);
        });
    };


    this.readAllNewMsg = function (msg,afterReadAllNewMsg) {

        $.ajax({
            url: '/WebServices/MessageWebService.asmx/ReadAllNewMsg' ,
            //type: 'get',
            data:{fromUserId: msg.fromUserId},
            dataType:'json'

        }).done(function (json) {
            if(json.isSuccess) {
                json=json.data;
                console.log(json);
                console.log("阅读成功");
                if (typeof afterReadAllNewMsg != 'undefined' && afterReadAllNewMsg instanceof Function) {
                    afterReadAllNewMsg();
                }
            }

        }).fail(function (a, b, c) {
            console.log(a);
        });
    };


    this.shortmsgViewDetail = function (msgWithCount) {
        if(msgWithCount.count<=1)
        {
            self.readAllNewMsg(msgWithCount.msg, function () {
                parent.linkMenuTree(true, msgWithCount.msg.menuType);
                frames["mainframe"].location.href = msgWithCount.msg.buttonUrl;
                self.removeReadedMsg(msgWithCount);
            });
        }
        else
        {
            self.readAllNewMsg(msgWithCount.msg,function(){
                parent.linkMenuTree(true, msgWithCount.msg.menuType);
                frames["mainframe"].location.href = msgWithCount.msg.buttonMutipleUrl;
                self.removeReadedMsg(msgWithCount);
            });
        }
    };

    this.removeReadedMsg = function (msgWithCount) {
        var count = msgWithCount.count;
        self.shotmsgList.remove(msgWithCount);
        if(self.shortmsgCount()>0)
        {
            self.shortmsgCount(self.shortmsgCount() - 1);

            //self.shortmsgCount(self.shortmsgCount() - count);
        }
    };


};