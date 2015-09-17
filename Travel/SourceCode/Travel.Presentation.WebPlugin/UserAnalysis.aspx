<%@ Page Language="C#" MasterPageFile="~/ListContent.Master" AutoEventWireup="true" CodeBehind="UserAnalysis.aspx.cs" Inherits="Travel.Presentation.WebPlugin.UserAnalysis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .content {
            width: 100% !important;
            display: block;
            min-height: 160px !important;
            height: 180px !important;
        }
    </style>
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
                    header();
                    showicon();
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
            function showicon(id) {
                var text = $('#' + id + '').text();
                if (text.indexOf("-") > 0) {
                    $('#' + id + '').parent().children()[0].className = "icon_down";
                    $('#' + id + '').parent().children()[0].title = "下降";
                }
            }
            function header() {
                $('#newuser').text(window.cgiData.UserStatisricsRadioInfo.ThisDayNewUser);
                $('#canceluser').text(window.cgiData.UserStatisricsRadioInfo.ThisDayCancelUser);
                $('#netgainuser').text(window.cgiData.UserStatisricsRadioInfo.ThisDayNetgainUser);
                $('#cumulateuser').text(window.cgiData.UserStatisricsRadioInfo.ThisDayCumulateUser);
                $('#newday').text('   ' + window.cgiData.UserStatisricsRadioInfo.DayNewUser + '');
                $('#cancelday').text('   ' + window.cgiData.UserStatisricsRadioInfo.DayCancelUser + '');
                $('#netgainday').text('   ' + window.cgiData.UserStatisricsRadioInfo.DayNetgainUser + '');
                $('#cumulateday').text('   ' + window.cgiData.UserStatisricsRadioInfo.DayCumulateUser + '');
                $('#newweek').text('   ' + window.cgiData.UserStatisricsRadioInfo.WeekNewUser + '');
                $('#cancelweek').text('   ' + window.cgiData.UserStatisricsRadioInfo.WeekCancelUser + '');
                $('#netgainweek').text('   ' + window.cgiData.UserStatisricsRadioInfo.WeekNetgainUser + '');
                $('#cumulateweek').text('   ' + window.cgiData.UserStatisricsRadioInfo.WeekCumulateUser + '');
                $('#newmonth').text('   ' + window.cgiData.UserStatisricsRadioInfo.MonthNewUser + '');
                $('#cancelmonth').text('   ' + window.cgiData.UserStatisricsRadioInfo.MonthCancelUser + '');
                $('#netgainmonth').text('   ' + window.cgiData.UserStatisricsRadioInfo.MonthNetgainUser + '');
                $('#cumulatemonth').text('   ' + window.cgiData.UserStatisricsRadioInfo.MonthCumulateUser + '');
                showicon("newday");
                showicon("cancelday");
                showicon("netgainday");
                showicon("cumulateday");
                showicon("newweek");
                showicon("cancelweek");
                showicon("netgainweek");
                showicon("cumulateweek");
                showicon("newmonth");
                showicon("cancelmonth");
                showicon("netgainmonth");
                showicon("cumulatemonth");
            }
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
            }
        });
    </script>
    <div>
        <div class="location">
            <a class="home"><i></i><span>统计</span></a>

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
                                                        <dd>日<i class="icon_up" title="上升"></i><span id="newday"></span></dd>
                                                        <dd>周<i class="icon_up" title="上升"></i><span id="newweek"></span></dd>
                                                        <dd>月<i class="icon_up" title="上升"></i><span id="newmonth"></span></dd>
                                                    </dl>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="ui_trendgrid_item">
                                                    <div class="ui_trendgrid_chart"></div>
                                                    <dl>
                                                        <dt><b>取消关注人数</b></dt>
                                                        <dd class="ui_trendgrid_number"><strong><span id="canceluser"></span></strong><em class="ui_trendgrid_unit"></em></dd>
                                                        <dd>日<i class="icon_up" title="上升"></i><span id="cancelday"></span></dd>
                                                        <dd>周<i class="icon_up" title="上升"></i><span id="cancelweek"></span></dd>
                                                        <dd>月<i class="icon_up" title="上升"></i><span id="cancelmonth"></span></dd>
                                                    </dl>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="ui_trendgrid_item">
                                                    <div class="ui_trendgrid_chart"></div>
                                                    <dl>
                                                        <dt><b>净增关注人数</b></dt>
                                                        <dd class="ui_trendgrid_number"><strong><span id="netgainuser"></span></strong><em class="ui_trendgrid_unit"></em></dd>
                                                        <dd>日<i class="icon_up" title="上升"></i><span id="netgainday"></span></dd>
                                                        <dd>周<i class="icon_up" title="上升"></i><span id="netgainweek"></span></dd>
                                                        <dd>月<i class="icon_up" title="上升"></i><span id="netgainmonth"></span></dd>
                                                    </dl>
                                                </div>
                                            </td>
                                            <td class="last">
                                                <div class="ui_trendgrid_item">
                                                    <div class="ui_trendgrid_chart"></div>
                                                    <dl>
                                                        <dt><b>累积关注人数</b></dt>
                                                        <dd class="ui_trendgrid_number"><strong><span id="cumulateuser"></span></strong><em class="ui_trendgrid_unit"></em></dd>
                                                        <dd>日<i class="icon_up" title="上升"></i><span id="cumulateday"></span></dd>
                                                        <dd>周<i class="icon_up" title="上升"></i><span id="cumulateweek"></span></dd>
                                                        <dd>月<i class="icon_up" title="上升"></i><span id="cumulatemonth"></span></dd>
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


</asp:Content>

