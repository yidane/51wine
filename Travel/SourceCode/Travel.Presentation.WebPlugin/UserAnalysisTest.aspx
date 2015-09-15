<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserAnalysisTest.aspx.cs" Inherits="Travel.Presentation.WebPlugin.UserAnalysisTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="styles/useranalysis.css" rel="stylesheet" />
    <link href="styles/baseuser.css" rel="stylesheet" />
    <link href="styles/date_range218878.css" rel="stylesheet" />
    <script src="http://cdn.hcharts.cn/highcharts/highcharts.js" type="text/javascript"></script>
    <script src="javascripts/jquery/jQuery-1.10.2.min.js"></script>
</head>
<body>
    <script type="text/javascript">
        window.cgiData = {
            list: [
                                                    {
                                                        user_source: 99999999,
                                                        list: [{
                                                            date: "2015-08-17",
                                                            cancel_user: 0,
                                                            //cumulate_user: 1,
                                                            //netgain_user: 0,
                                                            user_source: 0,
                                                            user_sourceDesc: "其他",
                                                            new_user: 0
                                                        }, {
                                                            date: "2015-08-18",
                                                            cancel_user: 0,
                                                            //cumulate_user: 1,
                                                            //netgain_user: 0,
                                                            user_source: 0,
                                                            user_sourceDesc: "其他",
                                                            new_user: 0
                                                        }]
                                                    }]
        };
    </script>
    <script type="text/javascript">

        function FormatDate(date) {
            var y = date.getFullYear();
            var m = date.getMonth() + 1;
            m = m < 10 ? '0' + m : m;
            var d = date.getDate();
            d = d < 10 ? ('0' + d) : d;
            return y + '-' + m + '-' + d;
        };

        var myDate = new Date();
        var recentDate = myDate.getTime();
        var n = 7;
        var end = new Date(recentDate - 24 * 60 * 60 * 1000);
        var endTime = FormatDate(end);
        $(document).ready(function () {
            var start = new Date(recentDate - 24 * 60 * 60 * 1000 * n);
            var startTime = FormatDate(start);
            $.ajax({
                type: "POST",
                url: "WebServices/WeChatWebService.asmx/GetUserAnalysis",
                data: { beginDate: startTime, endDate: endTime },
                dataType: 'json',
                success: function (result) {
                    //$('#dictionary').append(result.d);

                    window.cgiData.list = result.Data;
                }
            });

            $("#sevendays").click(function () {
                n = 7;
                var start = new Date(recentDate - 24 * 60 * 60 * 1000 * n);
                var startTime = FormatDate(start);
                $.ajax({
                    type: "POST",
                    url: "WebServices/WeChatWebService.asmx/GetUserAnalysis",
                    data: { beginDate: startTime, endDate: endTime },
                    dataType: 'json',
                    success: function (result) {
                        //$('#dictionary').append(result.d);
                        window.cgiData.list = result.Data;
                    }
                });
            });
            $("#fourteendays").click(function () {
                n = 14;
                var start = new Date(recentDate - 24 * 60 * 60 * 1000 * n);
                var startTime = FormatDate(start);
                $.ajax({
                    type: "POST",
                    url: "WebServices/WeChatWebService.asmx/GetUserAnalysis",
                    data: { beginDate: startTime, endDate: endTime },
                    dataType: 'json',
                    success: function (result) {
                        //$('#dictionary').append(result.d);
                        window.cgiData.list = result.Data;
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
                    url: "WebServices/WeChatWebService.asmx/GetUserAnalysis",
                    data: { beginDate: startTime, endDate: endTime },
                    dataType: 'json',
                    success: function (result) {
                        //$('#dictionary').append(result.d);
                        window.cgiData.list = result.Data;
                    }
                });
            });

            $('#js_msg_chart').highcharts({
                title: {
                    text: 'Monthly Average Temperature',
                    x: -20 //center
                },
                subtitle: {
                    text: 'Source: WorldClimate.com',
                    x: -20
                },
                xAxis: {
                    categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun','Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']
                },
                yAxis: {
                    title: {
                        text: 'Temperature (°C)'
                    },
                    plotLines: [{
                        value: 0,
                        width: 1,
                        color: '#808080'
                    }]
                },
                tooltip: {
                    valueSuffix: '°C'
                },
                legend: {
                    layout: 'vertical',
                    align: 'right',
                    verticalAlign: 'middle',
                    borderWidth: 0
                },
                series: [{
                    name: 'Tokyo',
                    data: [7.0, 6.9, 9.5, 14.5, 18.2, 21.5, 25.2, 26.5, 23.3, 18.3, 13.9, 9.6]
                }, {
                    name: 'New York',
                    data: [-0.2, 0.8, 5.7, 11.3, 17.0, 22.0, 24.8, 24.1, 20.1, 14.1, 8.6, 2.5]
                }, {
                    name: 'Berlin',
                    data: [-0.9, 0.6, 3.5, 8.4, 13.5, 17.0, 18.6, 17.9, 14.3, 9.0, 3.9, 1.0]
                }, {
                    name: 'London',
                    data: [3.9, 4.2, 5.7, 8.5, 11.9, 15.2, 17.0, 16.6, 14.2, 10.3, 6.6, 4.8]
                }]
            });
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
                    <div class="page_msg mini top">
                        <div class="inner group">
                            <span class="msg_icon_wrp">
                                <i class="icon_msg_mini info"></i>
                            </span>
                            <div class="msg_content">
                                <p>本页根据昨日数据来计算，而用户管理页根据当前数据计算，两者不一致。</p>
                            </div>
                        </div>
                    </div>

                    <div class="info_box" id="">
                        <div class="inner">
                            <div class="info_hd append_ask">
                                <h4>昨日关键指标</h4>
                                <div class="ext_info help">
                                    <i id="I1" class="icon_msg_mini ask"></i>
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
                                                            <dd class="ui_trendgrid_number"><strong></strong><em class="ui_trendgrid_unit"></em></dd>
                                                            <dd>日</dd>
                                                            <dd>周</dd>
                                                            <dd>月</dd>
                                                        </dl>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="ui_trendgrid_item">
                                                        <div class="ui_trendgrid_chart"></div>
                                                        <dl>
                                                            <dt><b>取消关注人数</b></dt>
                                                            <dd class="ui_trendgrid_number"><strong></strong><em class="ui_trendgrid_unit"></em></dd>
                                                            <dd>日 </dd>
                                                            <dd>周 </dd>
                                                            <dd>月 </dd>
                                                        </dl>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="ui_trendgrid_item">
                                                        <div class="ui_trendgrid_chart"></div>
                                                        <dl>
                                                            <dt><b>净增关注人数</b></dt>
                                                            <dd class="ui_trendgrid_number"><strong></strong><em class="ui_trendgrid_unit"></em></dd>
                                                            <dd>日</dd>
                                                            <dd>周</dd>
                                                            <dd>月</dd>
                                                        </dl>
                                                    </div>
                                                </td>
                                                <td class="last">
                                                    <div class="ui_trendgrid_item">
                                                        <div class="ui_trendgrid_chart"></div>
                                                        <dl>
                                                            <dt><b>累积关注人数</b></dt>
                                                            <dd class="ui_trendgrid_number"><strong></strong><em class="ui_trendgrid_unit"></em></dd>
                                                            <dd>日</dd>
                                                            <dd>周</dd>
                                                            <dd>月</dd>
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
                                <div class="info_hd">
                                    <strong class="lable time_lable">关键指标详解</strong>
                                    <div class="tabs">
                                        <a class="first current" href="javascript:;">新增人数</a>
                                        <a href="javascript:;">取消关注人数</a>
                                        <a href="javascript:;">净增人数</a>
                                        <a class="last" href="javascript:;">累积人数</a>
                                    </div>
                                </div>

                                <div class="sub_menu">
                                    <div>

                                        <strong class="lable time_lable">时间</strong>

                                        <div class="button_group">
                                            <a class="btn btn_default" href="javascript:;" range="7" id="sevendays">7日</a>
                                            <a class="btn btn_default" href="javascript:;" range="14" id="fourteendays">14日</a>
                                            <a class="btn btn_default selected" href="javascript:;" range="30" id="thirtydays">30日</a>

                                            <div class="btn_group_item td_data_container" id="js_date_container0">
                                                <div class="ta_date" id="div_js_dateRangeTitle1">
                                                    <span class="date_title" id="js_dateRangeTitle1">2015-08-15 至 2015-09-13</span>
                                                    <a class="opt_sel" id="js_dateRangeTrigger1" href="#">
                                                        <i class="i_orderd"></i>
                                                    </a>
                                                </div>
                                                <label class="contrast" for="needCompare_1442200323296" style="display: none;">
                                                    <input type="checkbox" class="pc" name="needCompare_1442200323296" id="needCompare_1442200323296" value="1" style="display: none;">对比</label><div class="ta_date" id="div_compare_js_dateRangeTitle1" style="display: none;"><span name="dateCompare" id="js_dateRangeTitle1Compare" class="date_title" style="display: none;">2015-07-16 至 2015-08-14</span>	<a class="opt_sel" id="compare_trigger_0" href="#"><i class="i_orderd"></i></a></div>
                                            </div>
                                            <div class="btn_group_item td_data_container" id="js_single_timer_container"></div>
                                        </div>


                                        <div class="setup">
                                            <button class="btn btn_primary" id="js_compare_btn0">按时间对比</button>
                                        </div>

                                    </div>
                                </div>
                                <div></div>
                            </div>



                            <div class="sub_menu" id="js_subselection">
                                <strong class="lable time_lable">增长来源</strong>
                                <div class="button_group">

                                    <a class="btn btn_default selected" href="javascript:;" data-tab="1" data-source="99999999">全部</a>
                                    <%--<a class="btn btn_default" href="javascript:;" data-tab="3" data-source="35">搜索公众号名称</a>
					    <a class="btn btn_default" href="javascript:;" data-tab="4" data-source="3">搜索微信号</a>
					    <a class="btn btn_default" href="javascript:;" data-tab="6" data-source="43">图文页右上角菜单</a>
					    <a class="btn btn_default" href="javascript:;" data-tab="2" data-source="17">名片分享</a>
					    <a class="btn btn_default" href="javascript:;" data-tab="7" data-source="0">其他</a>--%>
                                </div>
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
                                    <div class="sub_content" id="js_msg_chart"></div>
                                </div>

                                <%--<h4 class="sub_title">详细数据	                   	<div class="tr_ext_info"><a target="_blank" href="#" id="js_download_detail">导出Excel</a> </div>
               		</h4>
                    <div class="page_msg mini top">
                        <div class="inner group">
                            <span class="msg_icon_wrp">
                                <i class="icon_msg_mini info"></i>
                            </span>
                            <div class="msg_content">
                                <p>用户总数和用户增长数分别根据不同方法和时间点来统计，可能出现不匹配。</p>
                            </div>
                        </div>
                    </div>
                   <div class="sub_content">
	                     <div class="table_wrp">
	                     	  <table class="table" cellspacing="0" id="js_single_table">
                                <thead class="thead">
                                <tr>
                                    <th class="table_cell rank_area tl" data-type="date">
                                        时间                                        <span class="icon_rank rank_area">
                                            <i class="arrow arrow_up"></i><i class="arrow arrow_down"></i>
                                        </span>
                                    </th>
                                    <th class="table_cell rank_area tr" data-type="new_user">
                                        新关注人数                                        <span class="icon_rank">
                                            <i class="arrow arrow_up"></i><i class="arrow arrow_down"></i>
                                        </span>
                                    </th>
                                    <th class="table_cell rank_area tr" data-type="cancel_user">
                                        取消关注人数                                        <span class="icon_rank">
                                            <i class="arrow arrow_up"></i><i class="arrow arrow_down"></i>
                                        </span>
                                    </th>
                                     <th class="table_cell rank_area tr" data-type="netgain_user">
                                        净增关注人数                                        <span class="icon_rank">
                                            <i class="arrow arrow_up"></i><i class="arrow arrow_down"></i>
                                        </span>
                                    </th>
                                    <th class="table_cell tr rank_area last_child no_extra" data-type="cumulate_user">
                                        累积关注人数                                        <span class="icon_rank">
                                            <i class="arrow arrow_up"></i><i class="arrow arrow_down"></i>
                                        </span>
                                    </th>
                                    
                                </tr>
                                </thead>
                                <tbody class="tbody" id="js_detail">
                                    <tr class="empty_item"><td colspan="10" class="empty_tips">暂无数据</td></tr>
                                </tbody>
                            </table>  

                            <table class="table" cellspacing="0" id="js_compare_table" style="display: none;">
                                <thead class="thead">
                                <tr>
                                	<th class="table_cell rank_area tl">
                                        序号                                    </th>
                                    <th class="table_cell rank_area tl">
                                        时间                                    </th>
                                    <th class="table_cell rank_area tr">
                                        新关注人数                                    </th>
                                    <th class="table_cell rank_area tr">
                                        取消关注人数                                    </th>
                                     <th class="table_cell rank_area tr">
                                        净增关注人数                                    </th>
                                    <th class="table_cell tr rank_area last_child no_extra">
                                        累积关注人数                                    </th>
                                    
                                </tr>
                                </thead>
                                <tbody class="tbody">
                                </tbody>
                            </table>                          
	                     </div>
	                     
                         <div class="turn_page tr" id="js_pagebar"></div>
                   </div>--%>
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
