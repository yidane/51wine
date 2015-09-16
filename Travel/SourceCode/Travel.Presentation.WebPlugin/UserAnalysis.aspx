<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserAnalysis.aspx.cs" Inherits="Travel.Presentation.WebPlugin.UserAnalysis" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="styles/useranalysis.css" rel="stylesheet" />
    <link href="styles/baseuser.css" rel="stylesheet" />
    <link href="styles/date_range218878.css" rel="stylesheet" />
    <script src="javascripts/jquery/jQuery-1.10.2.min.js"></script>
    <script src="javascripts/highcharts.js" type="text/javascript"></script>
</head>
<body>
    <script type="text/javascript">
        window.cgiData = {
            list: [
                                                    {
                                                        user_source: 99999999,
                                                        list: [{
                                                            date: "2015-08-17",
                                                            cancel_user: 1,
                                                            cumulate_user: 1,
                                                            netgain_user: 2,
                                                            user_source: 0,
                                                            user_sourceDesc: "其他",
                                                            new_user: 3
                                                        }, {
                                                            date: "2015-08-18",
                                                            cancel_user: 4,
                                                            cumulate_user: 1,
                                                            netgain_user: 8,
                                                            user_source: 0,
                                                            user_sourceDesc: "其他",
                                                            new_user: 12
                                                        }],
                                                        newuser: {
                                                            users: 0,
                                                            day: 0,
                                                            week: 0,
                                                            month: 0
                                                        },
                                                        canceluser: {
                                                            users: 0,
                                                            day: 0,
                                                            week: 0,
                                                            month: 0
                                                        },
                                                        netgainuser: {
                                                            users: 0,
                                                            day: 0,
                                                            week: 0,
                                                            month: 0
                                                        },
                                                        cumulateuser: {
                                                            users: 0,
                                                            day: 0,
                                                            week: 0,
                                                            month: 0
                                                        }
                                                    }]
        };
    </script>
    <script type="text/javascript">

        function show() {
            $(".help_content").show();
        }
        function hide() {
            $(".help_content").hide();
        }

        function FormatDate(date) {
            var y = date.getFullYear();
            var m = date.getMonth() + 1;
            m = m < 10 ? '0' + m : m;
            var d = date.getDate();
            d = d < 10 ? ('0' + d) : d;
            return y + '-' + m + '-' + d;
        };
        function DateArray(n) {
            var arr = new Array();
            for (var i = n; i > 0; i--) {
                var date_rec = new Date(recentDate - 24 * 60 * 60 * 1000 * i);
                var rec = FormatDate(date_rec);
                arr.push([rec]);
            }
            return arr;
        }
        var myDate = new Date();
        var recentDate = myDate.getTime();
        var n = 7;
        var end = new Date(recentDate - 24 * 60 * 60 * 1000);
        var endTime = FormatDate(end);
        var arrdate = new Array();
        arrdate = DateArray(n);
        $(document).ready(function () {
            var start = new Date(recentDate - 24 * 60 * 60 * 1000 * n);
            var startTime = FormatDate(start);
            $.ajax({
                type: "POST",
                url: "WebServices/WeChatWebService.asmx/GetUserAnalysisWeb",
                data: { beginDate: startTime, endDate: endTime },
                dataType: 'json',
                success: function (result) {
                    //$('#dictionary').append(result.d);

                    window.cgiData = result.Data;
                    bind(n);
                }
            });

            $("#sevendays").click(function () {
                n = 7;
                var start = new Date(recentDate - 24 * 60 * 60 * 1000 * n);
                var startTime = FormatDate(start);
                $.ajax({
                    type: "POST",
                    url: "WebServices/WeChatWebService.asmx/GetUserAnalysisWeb",
                    data: { beginDate: startTime, endDate: endTime },
                    dataType: 'json',
                    success: function (result) {
                        //$('#dictionary').append(result.d);
                        window.cgiData = result.Data;
                        bind(n);
                        document.getElementById('sevendays').className = "btn btn_default selected";
                        document.getElementById('fourteendays').className = "btn btn_default";
                        document.getElementById('thirtydays').className = "btn btn_default";
                    }
                });
            });
            $("#fourteendays").click(function () {
                n = 14;
                var start = new Date(recentDate - 24 * 60 * 60 * 1000 * n);
                var startTime = FormatDate(start);
                $.ajax({
                    type: "POST",
                    url: "WebServices/WeChatWebService.asmx/GetUserAnalysisWeb",
                    data: { beginDate: startTime, endDate: endTime },
                    dataType: 'json',
                    success: function (result) {
                        //$('#dictionary').append(result.d);
                        window.cgiData = result.Data;
                        bind(n);
                        document.getElementById('fourteendays').className = "btn btn_default selected";
                        document.getElementById('thirtydays').className = "btn btn_default";
                        document.getElementById('sevendays').className = "btn btn_default";
                    }
                });
            });
            $("#thirtydays").click(function () {
                n = 30;
                var start = new Date(recentDate - 24 * 60 * 60 * 1000 * n);
                var startTime = FormatDate(start);
                $.ajax({
                    type: "POST",
                    //contentType: "application/json",
                    url: "WebServices/WeChatWebService.asmx/GetUserAnalysisWeb",
                    data: { beginDate: startTime, endDate: endTime },
                    dataType: 'json',
                    success: function (result) {
                        //$('#dictionary').append(result.d);
                        window.cgiData = result.Data;
                        bind(n);
                        document.getElementById('thirtydays').className = "btn btn_default selected";
                        document.getElementById('fourteendays').className = "btn btn_default";
                        document.getElementById('sevendays').className = "btn btn_default";
                    }
                });
            });

            function bind(n) {
                var arrnew = new Array();
                var arrcancel = new Array();
                var arrnetgain = new Array();
                var arrcumulate = new Array();
                for (var i in window.cgiData.list[0].list) {
                    arrnew.push([window.cgiData.list[0].list[i].new_user]);
                    arrcancel.push([window.cgiData.list[0].list[i].cancel_user]);
                    arrnetgain.push([window.cgiData.list[0].list[i].netgain_user]);
                    arrcumulate.push([window.cgiData.list[0].list[i].cumulate_user]);
                }
                var arrdate = new Array();
                arrdate = DateArray(n);
                $('#js_msg_charttest').highcharts({
                    title: {
                        text: '用户分析',
                        x: -20 //center
                    },
                    xAxis: {
                        categories: arrdate
                    },
                    yAxis: {
                        title: {
                            text: '人数'
                        },
                        plotLines: [{
                            value: 0,
                            width: 1,
                            color: '#808080'
                        }]
                    },
                    tooltip: {
                        valueSuffix: '人'
                    },
                    legend: {
                        layout: 'vertical',
                        align: 'right',
                        verticalAlign: 'middle',
                        borderWidth: 0
                    },
                    series: [{
                        name: '新增关注人数',
                        data: arrnew
                    }, {
                        name: '取消关注人数',
                        data: arrcancel
                    }, {
                        name: '净增关注人数',
                        data: arrnetgain
                    }, {
                        name: '累计关注人数',
                        data: arrcumulate
                    }]
                });
                $('#newuser').text(window.cgiData.list[0].newuser.users);
                $('#canceluser').text(window.cgiData.list[0].canceluser.users);
                $('#netgainuser').text(window.cgiData.list[0].netgainuser.users);
                $('#cumulateuser').text(window.cgiData.list[0].cumulateuser.users);
                $('#newday').text('   '+window.cgiData.list[0].newuser.day+'%');
                $('#cancelday').text('   ' + window.cgiData.list[0].canceluser.day + '%');
                $('#netgainday').text('   ' + window.cgiData.list[0].netgainuser.day + '%');
                $('#cumulateday').text('   ' + window.cgiData.list[0].cumulateuser.day + '%');
                $('#newweek').text('   ' + window.cgiData.list[0].newuser.week + '%');
                $('#cancelweek').text('   ' + window.cgiData.list[0].canceluser.week + '%');
                $('#netgainweek').text('   ' + window.cgiData.list[0].netgainuser.week + '%');
                $('#cumulateweek').text('   ' + window.cgiData.list[0].cumulateuser.week + '%');
                $('#newmonth').text('   ' + window.cgiData.list[0].newuser.month + '%');
                $('#cancelmonth').text('   ' + window.cgiData.list[0].canceluser.month + '%');
                $('#netgainmonth').text('   ' + window.cgiData.list[0].netgainuser.month + '%');
                $('#cumulatemonth').text('   ' + window.cgiData.list[0].cumulateuser.month + '%');
            }
        });
    </script>
    <form id="form1" runat="server">
        <div>
            <div class="main_hd">
                <h2>统计</h2>
                <%--<div class="title_tab" id="js_topTab">
    </div>--%>
            </div>
            <div class="main_bd user_analysis">
                <div class="wrp_overview">


                    <div class="info_box" id="">
                        <div class="inner">
                            <div class="info_hd append_ask">
                                <h4>昨日关键指标</h4>
                                <div class="ext_info help">
                                    <i id="I1" class="icon_msg_mini ask" onmouseover="show();" onmouseout="hide();"></i>
                                    <div class="help_content">
                                        <i class="dropdown_arrow out"></i>
                                        <i class="dropdown_arrow in"></i>
                                        <dl class="help-change-list" id="pop_items_info">
                                            <dt>新关注人数</dt>
                                            <dd>新关注的去重用户数</dd>
                                            <dt>取消关注人数</dt>
                                            <dd>取消关注的去重用户数</dd>
                                            <dt>净增关注人数</dt>
                                            <dd>新关注与取消关注的用户数之差</dd>
                                            <dt>累积关注人数</dt>
                                            <dd>当前关注的用户总数</dd>
                                            <dt>日、周、月</dt>
                                            <dd>分别计算昨日数据相比1天、7天、30天前的变化情况</dd>
                                            <dt>请注意</dt>
                                            <dd>用户总数根据昨日数据计算，而用户管理模块根据当前实时数据计算，两者可能不一致</dd>
                                        </dl>
                                        <div class="help-change-footer" id="footer_items_info"><span class="wechat_data_range">数据从2013年7月1日开始统计。</span>由于服务器缓存，以及指标计算方法和统计时间的差异，数据可能出现微小误差，一般在1%以内。</div>

                                    </div>
                                </div>
                            </div>
                            <div class="info_bd">
                                <div class="content" id="js_keydata">
                                    <div class="table_wrp">
                                        <table class="ui_trendgrid ui_trendgrid_3">
                                            <tr>
                                                <td class="first">
                                                    <div class="ui_trendgrid_item">
                                                        <div class="ui_trendgrid_chart"></div>
                                                        <dl>
                                                            <dt><b>新关注人数</b></dt>
                                                            <dd class="ui_trendgrid_number"><strong><span id="newuser"></span></strong><em class="ui_trendgrid_unit"></em></dd>
                                                            <dd>日</dd><span id="newday"></span>
                                                            <dd>周</dd><span id="newweek"></span>
                                                            <dd>月</dd><span id="newmonth"></span>
                                                        </dl>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="ui_trendgrid_item">
                                                        <div class="ui_trendgrid_chart"></div>
                                                        <dl>
                                                            <dt><b>取消关注人数</b></dt>
                                                            <dd class="ui_trendgrid_number"><strong><span id="canceluser"></span></strong><em class="ui_trendgrid_unit"></em></dd>
                                                            <dd>日</dd><span id="cancelday"></span>
                                                            <dd>周</dd><span id="cancelweek"></span>
                                                            <dd>月</dd><span id="cancelmonth"></span>
                                                        </dl>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="ui_trendgrid_item">
                                                        <div class="ui_trendgrid_chart"></div>
                                                        <dl>
                                                            <dt><b>净增关注人数</b></dt>
                                                            <dd class="ui_trendgrid_number"><strong><span id="netgainuser"></span></strong><em class="ui_trendgrid_unit"></em></dd>
                                                            <dd>日</dd><span id="netgainday"></span>
                                                            <dd>周</dd><span id="netgainweek"></span>
                                                            <dd>月</dd><span id="netgainmonth"></span>
                                                        </dl>
                                                    </div>
                                                </td>
                                                <td class="last">
                                                    <div class="ui_trendgrid_item">
                                                        <div class="ui_trendgrid_chart"></div>
                                                        <dl>
                                                            <dt><b>累积关注人数</b></dt>
                                                            <dd class="ui_trendgrid_number"><strong><span id="cumulateuser"></span></strong><em class="ui_trendgrid_unit"></em></dd>
                                                            <dd>日</dd><span id="cumulateday"></span>
                                                            <dd>周</dd><span id="cumulateweek"></span>
                                                            <dd>月</dd><span id="cumulatemonth"></span>
                                                        </dl>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="info_box drop_hd_right">
                        <div class="inner" id="js_actionstest">
                            <div>

                                <div class="sub_menu">
                                    <div>

                                        <strong class="lable time_lable">时间</strong>

                                        <div class="button_group">
                                            <a class="btn btn_default selected" href="javascript:;" range="7" id="sevendays">7日</a>
                                            <a class="btn btn_default" href="javascript:;" range="14" id="fourteendays">14日</a>
                                            <a class="btn btn_default" href="javascript:;" range="30" id="thirtydays">30日</a>


                                            <div class="btn_group_item td_data_container" id="js_single_timer_container"></div>
                                        </div>
                                    </div>
                                </div>
                                <div></div>
                            </div>
                            <div class="info_bd">
                                <div class="page_msg mini">
                                    <div class="inner group">
                                        <span class="msg_icon_wrp">
                                            <i class="icon_msg_mini info"></i>
                                        </span>
                                        <div class="msg_content">
                                            <p>用户增长来源目前只支持过滤趋势图，暂不支持区分详细数据，请知悉。</p>
                                        </div>
                                    </div>
                                </div>

                                <div class="page_msg mini" style="display: none;">
                                    <div class="inner group">
                                        <span class="msg_icon_wrp">
                                            <i class="icon_msg_mini info"></i>
                                        </span>
                                        <div class="msg_content">
                                            <p>用户增长来源增加了新的来源，新来源将缺少历史统计数据，请知悉。</p>
                                        </div>
                                    </div>
                                </div>

                                <div class="sub_title">趋势图</div>
                                <div class="sub_content">
                                    <div class="sub_content" id="js_msg_charttest"></div>
                                </div>


                            </div>

                        </div>
                    </div>

                    <div class="wrp_loading">
                        <div class="stat_loading">
                            <i class="icon_loading"></i><span>数据加载中...</span>
                        </div>
                        <div class="mask"></div>
                    </div>


                </div>

            </div>
        </div>
    </form>


</body>
</html>
