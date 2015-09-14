define("common/qq/mask.js", [ "biz_web/lib/spin.js" ], function(e, t, n) {
try {
var r = +(new Date);
"use strict", e("biz_web/lib/spin.js");
var i = 0, s = '<div class="mask"></div>';
function o(e) {
if (this.mask) this.mask.show(); else {
var t = "body";
e && !!e.parent && (t = $(e.parent)), this.mask = $(s).appendTo(t), this.mask.id = "wxMask_" + ++i, this.mask.spin("flower");
}
if (e) {
if (e.spin === !1) return this;
this.mask.spin(e.spin || "flower");
}
return this;
}
o.prototype = {
show: function() {
this.mask.show();
},
hide: function() {
this.mask.hide();
},
remove: function() {
this.mask.remove();
}
}, t.show = function(e) {
return new o(e);
}, t.hide = function() {
$(".mask").hide();
}, t.remove = function() {
$(".mask").remove();
};
} catch (u) {
wx.jslog({
src: "common/qq/mask.js"
}, u);
}
});define("biz_web/ui/dateRange.js",["tpl/biz_web/ui/dateRange.html.js","biz_web/widget/date_range.css"],function(t,e,a){
function s(t){
t.title_id="js_dateRangeTitle"+r,t.inputTrigger="js_dateRangeTrigger"+r,r++,$(t.container).html(template.compile(d)(t));
var e=new i(t.title_id,t);
return e.initOpt=t,e;
}
function i(t,e){
var a={
aToday:"aToday",
aYesterday:"aYesterday",
aRecent7Days:"aRecent7Days",
aRecent14Days:"aRecent14Days",
aRecent30Days:"aRecent30Days",
aRecent90Days:"aRecent90Days",
aDirectDay:[],
startDate:"",
endDate:"",
startCompareDate:"",
endCompareDate:"",
minValidDate:"315507600",
maxValidDate:"",
success:function(){
return!0;
},
startDateId:"startDate",
startCompareDateId:"startCompareDate",
endDateId:"endDate",
endCompareDateId:"endCompareDate",
target:"",
needCompare:!1,
suffix:"",
inputTrigger:"input_trigger",
compareTrigger:"compare_trigger",
compareCheckboxId:"needCompare",
calendars:2,
dayRangeMax:0,
monthRangeMax:12,
dateTable:"dateRangeDateTable",
selectCss:"dateRangeSelected",
compareCss:"dateRangeCompare",
coincideCss:"dateRangeCoincide",
firstCss:"first",
lastCss:"last",
clickCss:"today",
disableGray:"dateRangeGray",
isToday:"dateRangeToday",
joinLineId:"joinLine",
isSingleDay:!1,
defaultText:" 至 ",
singleCompare:!1,
stopToday:!0,
isTodayValid:!1,
weekendDis:!1,
disCertainDay:[],
disCertainDate:[],
shortOpr:!1,
noCalendar:!1,
theme:"gri",
autoCommit:!1,
autoSubmit:!1,
replaceBtn:"btn_compare",
onsubmit:$.noop,
beforeSelect:$.noop
},s=this;
if(this.inputId=t,this.inputCompareId=t+"Compare",this.compareInputDiv="div_compare_"+t,
this.mOpts=$.extend({},a,e),this.mOpts.calendars=Math.min(this.mOpts.calendars,3),
this.mOpts.compareCss="ta"==this.mOpts.theme?this.mOpts.selectCss:this.mOpts.compareCss,
this.periodObj={},s.mOpts.aDirectDay)for(var i=s.mOpts.aDirectDay,d=0,r=i.length;r>d;d++)this.periodObj[i[d].id]=i[d].value;else this.periodObj[s.mOpts.aToday]=0,
this.periodObj[s.mOpts.aYesterday]=1,this.periodObj[s.mOpts.aRecent7Days]=6,this.periodObj[s.mOpts.aRecent14Days]=13,
this.periodObj[s.mOpts.aRecent30Days]=29,this.periodObj[s.mOpts.aRecent90Days]=89;
this.startDefDate="";
var n=""==this.mOpts.suffix?(new Date).getTime():this.mOpts.suffix;
this.calendarId="calendar_"+n,this.dateListId="dateRangePicker_"+n,this.dateRangeCompareDiv="dateRangeCompareDiv_"+n,
this.dateRangeDiv="dateRangeDiv_"+n,this.compareCheckBoxDiv="dateRangeCompareCheckBoxDiv_"+n,
this.submitBtn="submit_"+n,this.closeBtn="closeBtn_"+n,this.preMonth="dateRangePreMonth_"+n,
this.nextMonth="dateRangeNextMonth_"+n,this.startDateId=this.mOpts.startDateId+"_"+n,
this.endDateId=this.mOpts.endDateId+"_"+n,this.compareCheckboxId=this.mOpts.compareCheckboxId+"_"+n,
this.startCompareDateId=this.mOpts.startCompareDateId+"_"+n,this.endCompareDateId=this.mOpts.endCompareDateId+"_"+n;
var p={
gri:['<div id="'+this.calendarId+'" class="gri_dateRangeCalendar">','<table class="gri_dateRangePicker"><tr id="'+this.dateListId+'"></tr></table>','<div class="gri_dateRangeOptions" '+(this.mOpts.autoSubmit?' style="display:none" ':"")+">",'<div class="gri_dateRangeInput" id="'+this.dateRangeDiv+'" >','<input type="text" class="gri_dateRangeInput" name="'+this.startDateId+'" id="'+this.startDateId+'" value="'+this.mOpts.startDate+'" readonly />','<span id="'+this.mOpts.joinLineId+'"> - </span>','<input type="text" class="gri_dateRangeInput" name="'+this.endDateId+'" id="'+this.endDateId+'" value="'+this.mOpts.endDate+'" readonly /><br />',"</div>",'<div class="gri_dateRangeInput" id="'+this.dateRangeCompareDiv+'">','<input type="text" class="gri_dateRangeInput" name="'+this.startCompareDateId+'" id="'+this.startCompareDateId+'" value="'+this.mOpts.startCompareDate+'" readonly />','<span class="'+this.mOpts.joinLineId+'"> - </span>','<input type="text" class="gri_dateRangeInput" name="'+this.endCompareDateId+'" id="'+this.endCompareDateId+'" value="'+this.mOpts.endCompareDate+'" readonly />',"</div>","<div>",'<input type="button" name="'+this.submitBtn+'" id="'+this.submitBtn+'" value="确定" />','&nbsp;<a id="'+this.closeBtn+'" href="javascript:;">关闭</a>',"</div>","</div>","</div>"],
ta:['<div id="'+this.calendarId+'" class="ta_calendar ta_calendar2 cf">','<div class="ta_calendar_cont cf" id="'+this.dateListId+'">',"</div>",'<div class="ta_calendar_footer cf" '+(this.mOpts.autoSubmit?' style="display:none" ':"")+">",'<div class="frm_msg">','<div id="'+this.dateRangeDiv+'">','<input type="text" class="ta_ipt_text_s" name="'+this.startDateId+'" id="'+this.startDateId+'" value="'+this.mOpts.startDate+'" readonly />','<span class="'+this.mOpts.joinLineId+'"> - </span>','<input type="text" class="ta_ipt_text_s" name="'+this.endDateId+'" id="'+this.endDateId+'" value="'+this.mOpts.endDate+'" readonly /><br />',"</div>",'<div id="'+this.dateRangeCompareDiv+'">','<input type="text" class="ta_ipt_text_s" name="'+this.startCompareDateId+'" id="'+this.startCompareDateId+'" value="'+this.mOpts.startCompareDate+'" readonly />','<span class="'+this.mOpts.joinLineId+'"> - </span>','<input type="text" class="ta_ipt_text_s" name="'+this.endCompareDateId+'" id="'+this.endCompareDateId+'" value="'+this.mOpts.endCompareDate+'" readonly />',"</div>","</div>",'<div class="frm_btn">','<input class="ta_btn ta_btn_primary" type="button" name="'+this.submitBtn+'" id="'+this.submitBtn+'" value="确定" />','<input class="ta_btn" type="button" id="'+this.closeBtn+'" value="取消"/>',"</div>","</div>","</div>"]
},m={
gri:['<label class="gri_contrast" for ="'+this.compareCheckboxId+'">','<input type="checkbox" class="gri_pc" name="'+this.compareCheckboxId+'" id="'+this.compareCheckboxId+'" value="1"/>对比',"</label>",'<input type="text" name="'+this.inputCompareId+'" id="'+this.inputCompareId+'" value="" class="gri_date"/>'],
ta:['<label class="contrast" for ="'+this.compareCheckboxId+'">','<input type="checkbox" class="pc" name="'+this.compareCheckboxId+'" id="'+this.compareCheckboxId+'" value="1"/>对比',"</label>",'<div class="ta_date" id="'+this.compareInputDiv+'">','	<span name="dateCompare" id="'+this.inputCompareId+'" class="date_title"></span>','	<a class="opt_sel" id="'+this.mOpts.compareTrigger+'" href="#">','		<i class="i_orderd"></i>',"	</a>","</div>"]
};
if($(m[this.mOpts.theme].join("")).insertAfter("ta"==this.mOpts.theme?$("#div_"+this.inputId):$("#"+this.inputId)),
this.mOpts.noCalendar&&($("#"+this.inputId).css("display","none"),$("#"+this.compareCheckboxId).parent().css("display","none")),
$(0<$("#appendParent").length?"#appendParent":document.body).append(p[this.mOpts.theme].join("")),
$("#"+this.calendarId).css("z-index",9999),1>$("#"+this.mOpts.startDateId).length?$(""!=this.mOpts.target?"#"+this.mOpts.target:"body").append('<input type="hidden" id="'+this.mOpts.startDateId+'" name="'+this.mOpts.startDateId+'" value="'+this.mOpts.startDate+'" />'):$("#"+this.mOpts.startDateId).val(this.mOpts.startDate),
1>$("#"+this.mOpts.endDateId).length?$(""!=this.mOpts.target?"#"+this.mOpts.target:"body").append('<input type="hidden" id="'+this.mOpts.endDateId+'" name="'+this.mOpts.endDateId+'" value="'+this.mOpts.endDate+'" />'):$("#"+this.mOpts.endDateId).val(this.mOpts.endDate),
1>$("#"+this.mOpts.compareCheckboxId).length&&$(""!=this.mOpts.target?"#"+this.mOpts.target:"body").append('<input type="checkbox" id="'+this.mOpts.compareCheckboxId+'" name="'+this.mOpts.compareCheckboxId+'" value="0" style="display:none;" />'),
0==this.mOpts.needCompare?($("#"+this.compareInputDiv).css("display","none"),$("#"+this.compareCheckBoxDiv).css("display","none"),
$("#"+this.dateRangeCompareDiv).css("display","none"),$("#"+this.compareCheckboxId).attr("disabled",!0),
$("#"+this.startCompareDateId).attr("disabled",!0),$("#"+this.endCompareDateId).attr("disabled",!0),
$("#"+this.compareCheckboxId).parent().css("display","none"),$("#"+this.mOpts.replaceBtn).length>0&&$("#"+this.mOpts.replaceBtn).hide()):(1>$("#"+this.mOpts.startCompareDateId).length?$(""!=this.mOpts.target?"#"+this.mOpts.target:"body").append('<input type="hidden" id="'+this.mOpts.startCompareDateId+'" name="'+this.mOpts.startCompareDateId+'" value="'+this.mOpts.startCompareDate+'" />'):$("#"+this.mOpts.startCompareDateId).val(this.mOpts.startCompareDate),
1>$("#"+this.mOpts.endCompareDateId).length?$(""!=this.mOpts.target?"#"+this.mOpts.target:"body").append('<input type="hidden" id="'+this.mOpts.endCompareDateId+'" name="'+this.mOpts.endCompareDateId+'" value="'+this.mOpts.endCompareDate+'" />'):$("#"+this.mOpts.endCompareDateId).val(this.mOpts.endCompareDate),
(""==this.mOpts.startCompareDate||""==this.mOpts.endCompareDate)&&($("#"+this.compareCheckboxId).attr("checked",!1),
$("#"+this.mOpts.compareCheckboxId).attr("checked",!1))),this.dateInput=this.startDateId,
this.changeInput(this.dateInput),$("#"+this.startDateId).bind("click",function(){
return s.endCompareDateId==s.dateInput&&$("#"+s.startCompareDateId).val(s.startDefDate),
s.startDefDate="",s.removeCSS(1),s.changeInput(s.startDateId),!1;
}),$("#"+this.calendarId).bind("click",function(t){
t.stopPropagation();
}),$("#"+this.startCompareDateId).bind("click",function(){
return s.endDateId==s.dateInput&&$("#"+s.startDateId).val(s.startDefDate),s.startDefDate="",
s.removeCSS(0),s.changeInput(s.startCompareDateId),!1;
}),$("#"+this.submitBtn).bind("click",function(){
return s.close(1),s.mOpts.success({
startDate:$("#"+s.mOpts.startDateId).val(),
endDate:$("#"+s.mOpts.endDateId).val(),
needCompare:$("#"+s.mOpts.compareCheckboxId).val(),
startCompareDate:$("#"+s.mOpts.startCompareDateId).val(),
endCompareDate:$("#"+s.mOpts.endCompareDateId).val()
}),s.mOpts.onsubmit({
startDate:$("#"+s.mOpts.startDateId).val(),
endDate:$("#"+s.mOpts.endDateId).val(),
needCompare:$("#"+s.mOpts.compareCheckboxId).val(),
startCompareDate:$("#"+s.mOpts.startCompareDateId).val(),
endCompareDate:$("#"+s.mOpts.endCompareDateId).val()
}),!1;
}),$("#"+this.closeBtn).bind("click",function(){
return s.close(),!1;
}),$("#"+this.inputId).bind("click",function(){
return s.init(),s.show(!1,s),!1;
}),$("#"+this.mOpts.inputTrigger).bind("click",function(){
return"none"==$("#"+s.calendarId).css("display")?(s.init(),s.show(!1,s)):s.close(),
!1;
}),$("#"+this.mOpts.compareTrigger).bind("click",function(){
return s.init(!0),s.show(!0,s),!1;
}),$("#"+this.inputCompareId).bind("click",function(){
return s.init(!0),s.show(!0,s),!1;
}),this.mOpts.singleCompare&&("ta"===this.mOpts.theme?($("#"+s.startDateId).val(s.mOpts.startDate),
$("#"+s.endDateId).val(s.mOpts.startDate),$("#"+s.startCompareDateId).val(s.mOpts.startCompareDate),
$("#"+s.endCompareDateId).val(s.mOpts.startCompareDate)):($("#"+s.startDateId).val(s.mOpts.startDate),
$("#"+s.endDateId).val(s.mOpts.startDate),$("#"+s.startCompareDateId).val(s.mOpts.startCompareDate),
$("#"+s.endCompareDateId).val(s.mOpts.startCompareDate),$("#"+this.compareCheckboxId).attr("checked",!0),
$("#"+this.mOpts.compareCheckboxId).attr("checked",!0))),$("#"+this.dateRangeCompareDiv).css("display",$("#"+this.compareCheckboxId).attr("checked")?"":"none"),
$("#"+this.compareInputDiv).css("display",$("#"+this.compareCheckboxId).attr("checked")?"":"none"),
$("#"+this.compareCheckboxId).bind("click",function(){
$("#"+s.inputCompareId).css("display",this.checked?"":"none"),$("#"+s.dateRangeCompareDiv).css("display",this.checked?"":"none"),
$("#"+s.compareInputDiv).css("display",this.checked?"":"none"),$("#"+s.startCompareDateId).css("disabled",this.checked?!1:!0),
$("#"+s.endCompareDateId).css("disabled",this.checked?!1:!0),$("#"+s.mOpts.compareCheckboxId).attr("checked",$("#"+s.compareCheckboxId).attr("checked")),
$("#"+s.mOpts.compareCheckboxId).val($("#"+s.compareCheckboxId).attr("checked")?1:0),
$("#"+s.compareCheckboxId).attr("checked")?(sDate=s.str2date($("#"+s.startDateId).val()),
sTime=sDate.getTime(),eDate=s.str2date($("#"+s.endDateId).val()),eTime=eDate.getTime(),
scDate=$("#"+s.startCompareDateId).val(),ecDate=$("#"+s.endCompareDateId).val(),
(""==scDate||""==ecDate)&&(ecDate=s.str2date(s.date2ymd(sDate).join("-")),ecDate.setDate(ecDate.getDate()-1),
scDate=s.str2date(s.date2ymd(sDate).join("-")),scDate.setDate(scDate.getDate()-(eTime-sTime)/864e5-1),
ecDate.getTime()<1e3*s.mOpts.minValidDate&&(scDate=sDate,ecDate=eDate),ecDate.getTime()>=1e3*s.mOpts.minValidDate&&scDate.getTime()<1e3*s.mOpts.minValidDate&&(scDate.setTime(1e3*s.mOpts.minValidDate),
scDate=s.str2date(s.date2ymd(scDate).join("-")),ecDate.setDate(scDate.getDate()+(eTime-sTime)/864e5-1)),
$("#"+s.startCompareDateId).val(s.formatDate(s.date2ymd(scDate).join("-"))),$("#"+s.endCompareDateId).val(s.formatDate(s.date2ymd(ecDate).join("-")))),
s.addCSS(1),s.changeInput(s.startCompareDateId)):(s.removeCSS(1),s.changeInput(s.startDateId)),
s.close(1),s.mOpts.success({
startDate:$("#"+s.mOpts.startDateId).val(),
endDate:$("#"+s.mOpts.endDateId).val(),
needCompare:$("#"+s.mOpts.compareCheckboxId).val(),
startCompareDate:$("#"+s.mOpts.startCompareDateId).val(),
endCompareDate:$("#"+s.mOpts.endCompareDateId).val()
});
}),this.init(),this.close(1),this.mOpts.replaceBtn&&$("#"+this.mOpts.replaceBtn).length>0){
var h=$(this.mOpts.container);
$("#"+s.compareCheckboxId).hide(),h.find(".contrast").hide(),$("#"+this.mOpts.replaceBtn).bind("click",function(){
var t=this,e=$("#"+s.compareCheckboxId);
e.click(),e.attr("checked")?function(){
e.removeAttr("checked"),h.find(".contrast").hide(),$(t).text("按时间对比");
}():function(){
e.attr("checked","checked"),h.find(".contrast").show(),$(t).text("取消对比");
}();
});
}
this.mOpts.autoCommit&&this.mOpts.success({
startDate:$("#"+s.mOpts.startDateId).val(),
endDate:$("#"+s.mOpts.endDateId).val(),
needCompare:$("#"+s.mOpts.compareCheckboxId).val(),
startCompareDate:$("#"+s.mOpts.startCompareDateId).val(),
endCompareDate:$("#"+s.mOpts.endCompareDateId).val()
}),$(document).bind("click",function(){
s.close();
});
}
var d=t("tpl/biz_web/ui/dateRange.html.js");
t("biz_web/widget/date_range.css");
var r=0;
a.exports=s,i.prototype.init=function(t){
var e=this,a="undefined"!=typeof t?t&&$("#"+e.compareCheckboxId).attr("checked"):$("#"+e.compareCheckboxId).attr("checked");
$("#"+this.dateListId).empty();
var s=""==this.mOpts.endDate?new Date:this.str2date(this.mOpts.endDate);
this.calendar_endDate=new Date(s.getFullYear(),s.getMonth()+1,0);
for(var i=0;i<this.mOpts.calendars;i++){
var d=null;
if("ta"==this.mOpts.theme?d=this.fillDate(s.getFullYear(),s.getMonth(),i):(d=document.createElement("td"),
$(d).append(this.fillDate(s.getFullYear(),s.getMonth(),i)),$(d).css("vertical-align","top")),
0==i)$("#"+this.dateListId).append(d);else{
var r="ta"==this.mOpts.theme?$("#"+this.dateListId).find("table").get(0):$("#"+this.dateListId).find("td").get(0);
$(r).before(d);
}
s.setMonth(s.getMonth()-1,1);
}
$("#"+this.preMonth).bind("click",function(){
return e.calendar_endDate.setMonth(e.calendar_endDate.getMonth()-1,1),e.mOpts.endDate=e.date2ymd(e.calendar_endDate).join("-"),
e.init(t),1==e.mOpts.calendars&&e.changeInput(""==$("#"+e.startDateId).val()?e.startDateId:e.endDateId),
!1;
}),$("#"+this.nextMonth).bind("click",function(){
return e.calendar_endDate.setMonth(e.calendar_endDate.getMonth()+1,1),e.mOpts.endDate=e.date2ymd(e.calendar_endDate).join("-"),
e.init(t),1==e.mOpts.calendars&&e.changeInput(""==$("#"+e.startDateId).val()?e.startDateId:e.endDateId),
!1;
}),this.calendar_startDate=new Date(s.getFullYear(),s.getMonth()+1,1),this.endDateId!=this.dateInput&&this.endCompareDateId!=this.dateInput&&this.addCSS(a&&"undefined"!=typeof t?1:0),
e.addCSS(a&&"undefined"!=typeof t?1:0),$("#"+e.inputCompareId).css("display",a?"":"none"),
$("#"+this.compareInputDiv).css("display",$("#"+this.compareCheckboxId).attr("checked")?"":"none");
for(var n in e.periodObj)$("#"+n).length>0&&($("#"+n).unbind("click"),$("#"+n).bind("click",function(){
var t="ta"==e.mOpts.theme?"active":"a";
$(this).parent().nextAll().removeClass(t),$(this).parent().prevAll().removeClass(t),
$(this).parent().addClass(t);
var a=e.getSpecialPeriod(e.periodObj[$(this).attr("id")]);
$("#"+e.startDateId).val(e.formatDate(a.otherday)),$("#"+e.endDateId).val(e.formatDate(a.today)),
$("#"+e.mOpts.startDateId).val($("#"+e.startDateId).val()),$("#"+e.mOpts.endDateId).val($("#"+e.endDateId).val()),
"ta"==e.mOpts.theme?$("#"+e.compareInputDiv).hide():$("#"+e.inputCompareId).css("display","none"),
$("#"+e.compareCheckboxId).attr("checked",!1),$("#"+e.mOpts.compareCheckboxId).attr("checked",!1),
$("#"+this.compareInputDiv).css("display",$("#"+this.compareCheckboxId).attr("checked")?"":"none"),
e.close(1),$("#"+e.startCompareDateId).val(""),$("#"+e.endCompareDateId).val(""),
$("#"+e.mOpts.startCompareDateId).val(""),$("#"+e.mOpts.endCompareDateId).val(""),
$("#"+e.mOpts.compareCheckboxId).val(0),$("#"+e.mOpts.replaceBtn).length>0&&($(".contrast").hide(),
$("#"+e.mOpts.replaceBtn).text("按时间对比")),e.mOpts.success({
startDate:$("#"+e.mOpts.startDateId).val(),
endDate:$("#"+e.mOpts.endDateId).val(),
needCompare:$("#"+e.mOpts.compareCheckboxId).val(),
startCompareDate:$("#"+e.mOpts.startCompareDateId).val(),
endCompareDate:$("#"+e.mOpts.endCompareDateId).val()
});
}));
$(document).bind("click",function(){
e.close();
}),$("#"+this.inputId).bind("change",function(){
""===$(this).val()&&($("#"+e.startDateId).val(""),$("#"+e.endDateId).val(""),$("#"+e.startCompareDateId).val(""),
$("#"+e.endCompareDateId).val(""));
});
},i.prototype.getSpecialPeriod=function(t){
var e=this,a=new Date;
1==e.mOpts.isTodayValid&&""!=e.mOpts.isTodayValid||2>t?"":a.setTime(a.getTime()-864e5);
var s=a.getTime()-24*t*60*60*1e3<1e3*e.mOpts.minValidDate?1e3*e.mOpts.minValidDate:a.getTime()-24*t*60*60*1e3,i=a.getFullYear()+"-"+(a.getMonth()+1)+"-"+a.getDate();
a.setTime(s);
var d=a.getFullYear()+"-"+(a.getMonth()+1)+"-"+a.getDate();
return t==e.periodObj.aYesterday&&(i=d),{
today:i,
otherday:d
};
},i.prototype.getCurrentDate=function(){
return{
startDate:$("#"+this.startDateId).val(),
endDate:$("#"+this.endDateId).val(),
needCompare:$("#"+this.mOpts.compareCheckboxId).val(),
startCompareDate:$("#"+this.mOpts.startCompareDateId).val(),
endCompareDate:$("#"+this.mOpts.endCompareDateId).val()
};
},i.prototype.removeCSS=function(t,e){
"undefined"==typeof e&&(e=this.mOpts.theme+"_"+this.mOpts.coincideCss),"undefined"==typeof t&&(t=0);
for(var a=new Date(this.calendar_startDate.getFullYear(),this.calendar_startDate.getMonth(),this.calendar_startDate.getDate()),s="",i=new Date(a);i.getTime()<=this.calendar_endDate.getTime();i.setDate(i.getDate()+1))s=0==t?this.mOpts.theme+"_"+this.mOpts.selectCss:this.mOpts.theme+"_"+this.mOpts.compareCss,
$("#"+this.calendarId+"_"+this.date2ymd(i).join("-")).removeClass(s),$("#"+this.calendarId+"_"+this.date2ymd(i).join("-")).removeClass(this.mOpts.firstCss).removeClass(this.mOpts.lastCss).removeClass(this.mOpts.clickCss);
},i.prototype.addCSS=function(t,e){
"undefined"==typeof e&&(e=this.mOpts.theme+"_"+this.mOpts.coincideCss),"undefined"==typeof t&&(t=0);
for(var a=this.str2date($("#"+this.startDateId).val()),s=this.str2date($("#"+this.endDateId).val()),i=this.str2date($("#"+this.startCompareDateId).val()),d=this.str2date($("#"+this.endCompareDateId).val()),r=0==t?a:i,n=0==t?s:d,p="",m=new Date(r);m.getTime()<=n.getTime();m.setDate(m.getDate()+1))0==t?(p=this.mOpts.theme+"_"+this.mOpts.selectCss,
$("#"+this.calendarId+"_"+this.date2ymd(m).join("-")).removeClass(this.mOpts.firstCss).removeClass(this.mOpts.lastCss).removeClass(this.mOpts.clickCss),
$("#"+this.calendarId+"_"+this.date2ymd(m).join("-")).removeClass(p)):p=this.mOpts.theme+"_"+this.mOpts.compareCss,
$("#"+this.calendarId+"_"+this.date2ymd(m).join("-")).attr("class",p);
"ta"==this.mOpts.theme&&($("#"+this.calendarId+"_"+this.date2ymd(new Date(r)).join("-")).removeClass().addClass(this.mOpts.firstCss),
$("#"+this.calendarId+"_"+this.date2ymd(new Date(n)).join("-")).removeClass().addClass(this.mOpts.lastCss),
r.getTime()==n.getTime()&&$("#"+this.calendarId+"_"+this.date2ymd(new Date(n)).join("-")).removeClass().addClass(this.mOpts.clickCss));
},i.prototype.checkDateRange=function(t,e){
var a=this.str2date(t),s=this.str2date(e),i=a.getTime(),d=s.getTime(),r=31*this.mOpts.monthRangeMax+this.mOpts.dayRangeMax,n=Math.abs(d-i)/864e5;
return r>0&&n>r?(alert("所选日期跨度最大不能超过"+r+"天"),!1):!0;
},i.prototype.selectDate=function(t){
this.changeInput(this.dateInput);
var e=this.formatDate(t);
if(this.startDateId==this.dateInput)this.removeCSS(0),this.removeCSS(1),$("#"+this.calendarId+"_"+t).attr("class","ta"==this.mOpts.theme?this.mOpts.clickCss:this.mOpts.theme+"_"+this.mOpts.selectCss),
this.startDefDate=$("#"+this.dateInput).val(),$("#"+this.dateInput).val(e),1==this.mOpts.singleCompare||1==this.mOpts.isSingleDay?(this.dateInput=this.startDateId,
$("#"+this.endDateId).val(e),(this.mOpts.shortOpr||this.mOpts.autoSubmit)&&this.close(1),
this.mOpts.success({
startDate:$("#"+this.mOpts.startDateId).val(),
endDate:$("#"+this.mOpts.endDateId).val(),
needCompare:$("#"+this.mOpts.compareCheckboxId).val(),
startCompareDate:$("#"+this.mOpts.startCompareDateId).val(),
endCompareDate:$("#"+this.mOpts.endCompareDateId).val()
})):this.dateInput=this.endDateId;else if(this.endDateId==this.dateInput){
if(""==$("#"+this.startDateId).val())return this.dateInput=this.startDateId,this.selectDate(t),
!1;
if(0==this.checkDateRange($("#"+this.startDateId).val(),t))return!1;
-1==this.compareStrDate(t,$("#"+this.startDateId).val())&&($("#"+this.dateInput).val($("#"+this.startDateId).val()),
$("#"+this.startDateId).val(e),e=$("#"+this.dateInput).val()),$("#"+this.dateInput).val(e),
this.dateInput=this.startDateId,this.removeCSS(0),this.addCSS(0),this.startDefDate="",
this.mOpts.autoSubmit&&(this.close(1),this.mOpts.success({
startDate:$("#"+this.mOpts.startDateId).val(),
endDate:$("#"+this.mOpts.endDateId).val(),
needCompare:$("#"+this.mOpts.compareCheckboxId).val(),
startCompareDate:$("#"+this.mOpts.startCompareDateId).val(),
endCompareDate:$("#"+this.mOpts.endCompareDateId).val()
}));
}else if(this.startCompareDateId==this.dateInput)this.removeCSS(1),this.removeCSS(0),
$("#"+this.calendarId+"_"+t).attr("class","ta"==this.mOpts.theme?this.mOpts.clickCss:this.mOpts.theme+"_"+this.mOpts.compareCss),
this.startDefDate=$("#"+this.dateInput).val(),$("#"+this.dateInput).val(e),1==this.mOpts.singleCompare||1==this.mOpts.isSingleDay?(this.dateInput=this.startCompareDateId,
$("#"+this.endCompareDateId).val(e),(this.mOpts.shortOpr||this.mOpts.autoSubmit)&&this.close(1),
this.mOpts.success({
startDate:$("#"+this.mOpts.startDateId).val(),
endDate:$("#"+this.mOpts.endDateId).val(),
needCompare:$("#"+this.mOpts.compareCheckboxId).val(),
startCompareDate:$("#"+this.mOpts.startCompareDateId).val(),
endCompareDate:$("#"+this.mOpts.endCompareDateId).val()
})):this.dateInput=this.endCompareDateId;else if(this.endCompareDateId==this.dateInput){
if(""==$("#"+this.startCompareDateId).val())return this.dateInput=this.startCompareDateId,
this.selectDate(t),!1;
if(0==this.checkDateRange($("#"+this.startCompareDateId).val(),t))return!1;
-1==this.compareStrDate(t,$("#"+this.startCompareDateId).val())&&($("#"+this.dateInput).val($("#"+this.startCompareDateId).val()),
$("#"+this.startCompareDateId).val(e),e=$("#"+this.dateInput).val()),$("#"+this.dateInput).val(e),
this.dateInput=this.startCompareDateId,this.removeCSS(1),this.addCSS(1),this.startDefDate="",
this.mOpts.autoSubmit&&(this.close(1),this.mOpts.success({
startDate:$("#"+this.mOpts.startDateId).val(),
endDate:$("#"+this.mOpts.endDateId).val(),
needCompare:$("#"+this.mOpts.compareCheckboxId).val(),
startCompareDate:$("#"+this.mOpts.startCompareDateId).val(),
endCompareDate:$("#"+this.mOpts.endCompareDateId).val()
}));
}
},i.prototype.show=function(t,e){
if(!this._disabled){
$("#"+e.dateRangeDiv).css("display",t?"none":""),$("#"+e.dateRangeCompareDiv).css("display",t?"":"none");
var a=t?$("#"+this.inputCompareId).offset():$("#"+this.inputId).offset(),s=(t?$("#"+this.inputCompareId).height():$("#"+this.inputId).height(),
parseInt($(document.body)[0].clientWidth)),i=a.left;
return $("#"+this.calendarId).css("display","block"),(1==this.mOpts.singleCompare||1==this.mOpts.isSingleDay)&&($("#"+this.endDateId).css("display","none"),
$("#"+this.endCompareDateId).css("display","none"),$("#"+this.mOpts.joinLineId).css("display","none"),
$("."+this.mOpts.joinLineId).css("display","none")),s>0&&$("#"+this.calendarId).width()+a.left>s&&(i=a.left+$("#"+this.inputId).width()-$("#"+this.calendarId).width()+(/msie/i.test(navigator.userAgent)&&!/opera/i.test(navigator.userAgent)?5:0),
"ta"==e.mOpts.theme&&(i+=50)),$("#"+this.calendarId).css("left",i+"px"),$("#"+this.calendarId).css("top",a.top+("ta"==e.mOpts.theme?35:22)+"px"),
this.changeInput(t?this.startCompareDateId:this.startDateId),!1;
}
},i.prototype.close=function(t){
if(t){
this.mOpts.shortOpr===!0?($("#"+this.inputId).val($("#"+this.startDateId).val()),
$("#"+this.inputCompareId).val($("#"+this.startCompareDateId).val())):$("#"+this.inputId).val($("#"+this.startDateId).val()+(""==$("#"+this.endDateId).val()?"":this.mOpts.defaultText+$("#"+this.endDateId).val()));
var e=1==this.mOpts.isTodayValid&&""!=this.mOpts.isTodayValid?(new Date).getTime():(new Date).getTime()-864e5,a=this.str2date($("#"+this.startDateId).val()).getTime(),s=this.str2date($("#"+this.endDateId).val()).getTime();
if(a>s){
var i=$("#"+this.startDateId).val();
$("#"+this.startDateId).val($("#"+this.endDateId).val()),$("#"+this.endDateId).val(i);
}
var d=this.str2date($("#"+this.startCompareDateId).val()).getTime(),r=this.str2date($("#"+this.endCompareDateId).val()).getTime();
if(d>r){
var i=$("#"+this.startCompareDateId).val();
$("#"+this.startCompareDateId).val($("#"+this.endCompareDateId).val()),$("#"+this.endCompareDateId).val(i);
}
var n=1==this.mOpts.shortOpr?$("#"+this.startDateId).val():$("#"+this.startDateId).val()+(""==$("#"+this.endDateId).val()?"":this.mOpts.defaultText+$("#"+this.endDateId).val()),p=document.getElementById(this.inputId);
if(p&&"INPUT"==p.tagName?($("#"+this.inputId).val(n),$("#"+this.inputCompareId).is(":visible")&&$("#"+this.inputCompareId).val(c)):($("#"+this.inputId).html(n),
$("#"+this.inputCompareId).is(":visible")&&$("#"+this.inputCompareId).html(c)),"ta"!=this.mOpts.theme&&""!=$("#"+this.startCompareDateId).val()&&""!=$("#"+this.endCompareDateId).val()){
var m=this.str2date($("#"+this.startCompareDateId).val()).getTime(),h=this.str2date($("#"+this.endCompareDateId).val()).getTime(),o=m+s-a;
o>e&&(o=e,$("#"+this.startCompareDateId).val(this.formatDate(this.date2ymd(new Date(o+a-s)).join("-")))),
$("#"+this.endCompareDateId).val(this.formatDate(this.date2ymd(new Date(o)).join("-")));
var m=this.str2date($("#"+this.startCompareDateId).val()).getTime(),h=this.str2date($("#"+this.endCompareDateId).val()).getTime();
if(m>h){
var i=$("#"+this.startCompareDateId).val();
$("#"+this.startCompareDateId).val($("#"+this.endCompareDateId).val()),$("#"+this.endCompareDateId).val(i);
}
}
var c=1==this.mOpts.shortOpr?$("#"+this.startCompareDateId).val():$("#"+this.startCompareDateId).val()+(""==$("#"+this.endCompareDateId).val()?"":this.mOpts.defaultText+$("#"+this.endCompareDateId).val());
p&&"INPUT"==p.tagName?$("#"+this.inputCompareId).val(c):$("#"+this.inputCompareId).html(c);
$("#"+this.mOpts.startDateId).val($("#"+this.startDateId).val()),$("#"+this.mOpts.endDateId).val($("#"+this.endDateId).val()),
$("#"+this.mOpts.startCompareDateId).val($("#"+this.startCompareDateId).val()),$("#"+this.mOpts.endCompareDateId).val($("#"+this.endCompareDateId).val());
for(var l in this.periodObj)$("#"+this.mOpts[l])&&$("#"+this.mOpts[l]).parent().removeClass("a");
}
return $("#"+this.calendarId).css("display","none"),!1;
},i.prototype.fillDate=function(t,e,a){
var s=this,i="ta"==this.mOpts.theme,d=new Date(t,e,1),r=new Date(t,e,1),n=r.getDay();
r.setDate(1-n);
var p=new Date(t,e+1,0),m=new Date(t,e+1,0);
n=m.getDay(),m.setDate(m.getDate()+6-n);
var h=new Date,o=h.getDate(),c=h.getMonth(),l=h.getFullYear(),D=document.createElement("table");
if(i){
D.className=this.mOpts.dateTable,cap=document.createElement("caption"),$(cap).append(t+"年"+(e+1)+"月"),
$(D).append(cap),thead=document.createElement("thead"),tr=document.createElement("tr");
for(var I=["日","一","二","三","四","五","六"],C=0;7>C;C++)th=document.createElement("th"),
$(th).append(I[C]),$(tr).append(th);
$(thead).append(tr),$(D).append(thead),tr=document.createElement("tr"),td=document.createElement("td"),
0==a&&$(td).append('<a href="javascript:void(0);" id="'+this.nextMonth+'"><i class="i_next"></i></a>'),
a+1==this.mOpts.calendars&&$(td).append('<a href="javascript:void(0);" id="'+this.preMonth+'"><i class="i_pre"></i></a>'),
$(td).attr("colSpan",7),$(td).css("text-align","center"),$(tr).append(td),$(D).append(tr);
}else{
D.className=this.mOpts.theme+"_"+this.mOpts.dateTable,tr=document.createElement("tr"),
td=document.createElement("td"),0==a&&$(td).append('<a href="javascript:void(0);" id="'+this.nextMonth+'" class="gri_dateRangeNextMonth"><span>next</span></a>'),
a+1==this.mOpts.calendars&&$(td).append('<a href="javascript:void(0);" id="'+this.preMonth+'" class="gri_dateRangePreMonth"><span>pre</span></a>'),
$(td).append(t+"年"+(e+1)+"月"),$(td).attr("colSpan",7),$(td).css("text-align","center"),
$(td).css("background-color","#F9F9F9"),$(tr).append(td),$(D).append(tr);
var I=["日","一","二","三","四","五","六"];
tr=document.createElement("tr");
for(var C=0;7>C;C++)td=document.createElement("td"),$(td).append(I[C]),$(tr).append(td);
$(D).append(tr);
}
for(var v="",O=0,u="",g=r;g.getTime()<=m.getTime();g.setDate(g.getDate()+1)){
if(g.getTime()<d.getTime())v=this.mOpts.theme+"_"+this.mOpts.disableGray,O="-1";else if(g.getTime()>p.getTime())v=this.mOpts.theme+"_"+this.mOpts.disableGray,
O="1";else if(1==this.mOpts.stopToday&&g.getTime()>h.getTime()||g.getTime()<1e3*s.mOpts.minValidDate||""!==s.mOpts.maxValidDate&&g.getTime()>1e3*s.mOpts.maxValidDate)v=this.mOpts.theme+"_"+this.mOpts.disableGray,
O="2";else{
if(O="0",g.getDate()==o&&g.getMonth()==c&&g.getFullYear()==l?1==this.mOpts.isTodayValid?v=this.mOpts.theme+"_"+this.mOpts.isToday:(v=this.mOpts.theme+"_"+this.mOpts.disableGray,
O="2"):v="",!this.mOpts.weekendDis||6!=g.getDay()&&0!=g.getDay()||(v=this.mOpts.theme+"_"+this.mOpts.disableGray,
O="3"),this.mOpts.disCertainDay&&this.mOpts.disCertainDay.length>0)for(var y in this.mOpts.disCertainDay)isNaN(this.mOpts.disCertainDay[y])||g.getDay()!==this.mOpts.disCertainDay[y]||(v=this.mOpts.theme+"_"+this.mOpts.disableGray,
O="4");
if(this.mOpts.disCertainDate&&this.mOpts.disCertainDate.length>0){
var f=!1;
for(var y in this.mOpts.disCertainDate)if(!isNaN(this.mOpts.disCertainDate[y])||isNaN(parseInt(this.mOpts.disCertainDate[y])))if(this.mOpts.disCertainDate[0]===!0){
if(f=!(g.getDate()===this.mOpts.disCertainDate[y]),!f)break;
}else if(f=!(g.getDate()!==this.mOpts.disCertainDate[y]))break;
f&&(v=this.mOpts.theme+"_"+this.mOpts.disableGray,O="4");
}
}
0==g.getDay()&&(tr=document.createElement("tr")),td=document.createElement("td"),
td.innerHTML=g.getDate(),""!=v&&$(td).attr("class",v),0==O&&(u=g.getFullYear()+"-"+(g.getMonth()+1)+"-"+g.getDate(),
$(td).attr("id",s.calendarId+"_"+u),$(td).css("cursor","pointer"),function(t){
$(td).bind("click",t,function(){
return s.mOpts.beforeSelect.call(s,t)===!1?!1:(s.selectDate(t),!1);
});
}(u)),$(tr).append(td),6==g.getDay()&&$(D).append(tr);
}
return D;
},i.prototype.str2date=function(t){
var e=t.split("-");
return new Date(e[0],e[1]-1,e[2]);
},i.prototype.compareStrDate=function(t,e){
var a=this.str2date(t),s=this.str2date(e);
return a.getTime()>s.getTime()?1:a.getTime()==s.getTime()?0:-1;
},i.prototype.date2ymd=function(t){
return[t.getFullYear(),t.getMonth()+1,t.getDate()];
},i.prototype.changeInput=function(t){
1==this.mOpts.isSingleDay&&(t=this.startDateId);
var e=[this.startDateId,this.startCompareDateId,this.endDateId,this.endCompareDateId],a="";
a=t==this.startDateId||t==this.endDateId?this.mOpts.theme+"_"+this.mOpts.selectCss:this.mOpts.theme+"_"+this.mOpts.compareCss,
t==this.endDateId&&this.mOpts.singleCompare&&(a=this.mOpts.theme+"_"+this.mOpts.compareCss);
for(var s in e)e.hasOwnProperty(s)&&($("#"+e[s]).removeClass(this.mOpts.theme+"_"+this.mOpts.selectCss),
$("#"+e[s]).removeClass(this.mOpts.theme+"_"+this.mOpts.compareCss));
$("#"+t).addClass(a),$("#"+t).css("background-repeat","repeat"),this.dateInput=t;
},i.prototype.formatDate=function(t){
return t.replace(/(\d{4})\-(\d{1,2})\-(\d{1,2})/g,function(t,e,a,s){
return 10>a&&(a="0"+a),10>s&&(s="0"+s),e+"-"+a+"-"+s;
});
},i.prototype.setDate=function(t){
return t=$.extend({},this.initOpt||{},t),s(t);
};
});define("tpl/statistics/date-range.tpl.js",[],function(){
return'<div>\n\n<strong class="lable time_lable">时间</strong>\n\n<div class="button_group">\n    <a class="btn btn_default" href="javascript:;" range="7">7日</a>\n    <a class="btn btn_default" href="javascript:;" range="14">14日</a>\n    <a class="btn btn_default selected" href="javascript:;" range="30">30日</a>\n\n    <div class="btn_group_item td_data_container" id="js_begin_time_container"> </div>                            \n    <div class="btn_group_item td_data_container" id="js_single_timer_container"> </div>                            \n</div>\n\n\n<div class="setup">\n    <button class="btn btn_primary" id="btn_compare">按时间对比</button>\n</div>\n\n</div>\n';
});define("statistics/components/event-emitter.js",[],function(){
"use strict";
function t(){
this.emitter=$(this);
}
var e=t.prototype;
return e.on=function(t,e){
this.emitter.on(t,function(){
e.apply(e,[].slice.call(arguments,1));
});
},e.off=function(){
this.emitter.off.apply(this.emitter,arguments);
},e.emit=function(){
var t=[].slice.call(arguments,1),e=arguments[0];
this.emitter.trigger.call(this.emitter,e,t);
},e.once=function(){
this.emitter.one.apply(this.emitter,arguments);
},t;
});define("tpl/statistics/tab-bar.tpl.js",[],function(){
return'<div>\n<div class="info_hd">\n    <strong class="lable time_lable">{name}</strong>\n    <div class="tabs">\n        {each tabs as tab}\n\n        {if $index === 0}\n        <a class="first" href="javascript:;">{tab.text}</a>\n        {else if $index === tabs.length - 1}\n        <a class="last" href="javascript:;">{tab.text}</a>\n        {else}\n        <a href="javascript:;">{tab.text}</a>\n        {/if}\n\n        {/each}\n    </div>                                  \n</div>\n\n<div class="sub_menu"> </div>\n<div>\n';
});define("statistics/user_stat/top.js",["common/wx/top.js","biz_common/moment.js"],function(s){
"use strict";
var t=s("common/wx/top.js"),e=s("biz_common/moment.js"),n=e().add("d",-1).format("YYYY-MM-DD"),a=[{
id:"user_stat",
name:"用户增长",
url:"/misc/useranalysis?source=99999999"
},{
id:"user_attr",
name:"用户属性",
url:"/misc/useranalysis?action=attr&begin_date=%s&end_date=%s".sprintf(n,n)
}];
return new t("#js_topTab",a);
});define("statistics/common.js",["common/wx/Cgi.js","biz_common/moment.js"],function(_,E){
"use strict";
function A(_){
var E=""+_+":00";
return 4===E.length&&(E="0"+E),E;
}
var T=_("common/wx/Cgi.js"),R=jQuery,I=_("biz_common/moment.js"),S="YYYY-MM-DD";
E.reportKeys={
LOAD_MSG_DATA_AJAX_KEY:0,
LOAD_KEYWORD_DATA_AJAX_KEY:1,
LOAD_USER_SUMMARY_DATA_AJAX_KEY:2,
MSG_PAGE:3,
MSG_KEYWORD_PAGE:4,
USER_SUMMARY_PAGE:5,
USER_ATTR_PAGE:6,
LOAD_ARTICLE_DATA_AJAX_KEY:7,
ARTICLE_ANALYSE_PAGE:8,
LOAD_ARTICLE_ITEM_AJAX_KEY:9,
LOAD_ARTICLE_DETAIL_AJAX_KEY:10,
ARTICLE_DETAIL_PAGE:11,
LOAD_INTERFACE_AJAX_KEY:12,
INTERFACE_PAGE:13,
MSG_TAB_USER_COUNT:20,
MSG_TAB_MSG_COUNT:21,
MSG_TAB_AVERAGE_MSG_COUNT:22,
USER_SUM_TAB_NEW_USER:23,
USER_SUM_TAB_CANCEL_USER:24,
USER_SUM_TAB_NETGAIN_USER:25,
USER_SUM_TAB_CUMULATE_USER:26,
USER_SUM_COMPARE:27,
USER_SUM_SRC_99999999:28,
USER_SUM_SRC_35:29,
USER_SUM_SRC_3:30,
USER_SUM_SRC_43:31,
USER_SUM_SRC_17:32,
USER_SUM_SRC_0:33,
USER_SUM_EXCEL:34,
USER_SUM_NAV_LEFT:35,
USER_SUM_NAV_RIGHT:36,
USER_SUM_NAV_JUMP:37,
USER_ATTR_PROVINCE_NAV_LEFT:38,
USER_ATTR_PROVINCE_NAV_RIGHT:39,
USER_ATTR_PROVINCE_NAV_JUMP:40,
USER_ATTR_CITY_DROPDOWN:41,
USER_ATTR_CITY_NAV_LEFT:42,
USER_ATTR_CITY_NAV_RIGHT:43,
USER_ATTR_CITY_NAV_JUMP:44,
USER_ATTR_DETAIL_GENDERS:45,
USER_ATTR_DETAIL_LANGS:46,
USER_ATTR_DETAIL_PROVINCES:47,
USER_ATTR_DETAIL_CITIES:48,
USER_ATTR_DETAIL_ENDPOINTS:49,
USER_ATTR_DETAIL_TYPES:50,
INTERFACE_TOP_LEFT:51,
INTERFACE_TOP_RIGHT:52,
INTERFACE_GROUP_TAB_ALL:53,
INTERFACE_GROUP_TAB_DETAIL:54,
INTERFACE_GROUP_TAB_COMPARE:55,
INTERFACE_GROUP_ALL_FILTER_DATE:56,
INTERFACE_GROUP_ALL_FILTER_SORT_TYPE:57,
INTERFACE_GROUP_ALL_FILTER_SORT_DIR:58,
INTERFACE_GROUP_ALL_FILTER_SEARCH:59,
INTERFACE_GROUP_ALL_ITEM_LINK_DETAIL:60,
INTERFACE_GROUP_ALL_ITEM_LINK_COMPARE:61,
INTERFACE_GROUP_DETAIL_TAB_INT:62,
INTERFACE_GROUP_DETAIL_TAB_ORI:63,
INTERFACE_GROUP_DETAIL_TAB_SHARE:64,
INTERFACE_GROUP_DETAIL_TAB_FAV:65,
INTERFACE_GROUP_DETAIL_PROVINCE_SORT:66,
INTERFACE_GROUP_DETAIL_PROVINCE_PREV:67,
INTERFACE_GROUP_DETAIL_PROVINCE_NEXT:68,
INTERFACE_GROUP_DETAIL_PROVINCE_JUMP:69,
INTERFACE_GROUP_DETAIL_TABLE_INT_USER:70,
INTERFACE_GROUP_DETAIL_TABLE_INT_COUNT:71,
INTERFACE_GROUP_DETAIL_TABLE_ORI_USER:72,
INTERFACE_GROUP_DETAIL_TABLE_ORI_COUNT:73,
INTERFACE_GROUP_DETAIL_TABLE_SHARE_USER:74,
INTERFACE_GROUP_DETAIL_TABLE_SHARE_COUNT:75,
INTERFACE_GROUP_DETAIL_TABLE_FAV_USER:76,
INTERFACE_STAT_TYPE_SELECT:77,
INTERFACE_STAT_TAB_INT:78,
INTERFACE_STAT_TAB_ORI:79,
INTERFACE_STAT_TAB_SHARE:80,
INTERFACE_STAT_TAB_FAV:81,
INTERFACE_STAT_DATE_7:82,
INTERFACE_STAT_DATE_14:83,
INTERFACE_STAT_DATE_30:84,
INTERFACE_STAT_DATE_RANGE:85,
INTERFACE_STAT_DATE_COMPARE:86,
INTERFACE_STAT_SRC_ALL:87,
INTERFACE_STAT_SRC_CONVERSATION:88,
INTERFACE_STAT_SRC_SHARE:89,
INTERFACE_STAT_SRC_MOMENT:90,
INTERFACE_STAT_SRC_WEIBO:91,
INTERFACE_STAT_SRC_HISTORY:92,
INTERFACE_STAT_SRC_OTHER:93,
INTERFACE_STAT_EXCEL:94,
INTERFACE_STAT_TABLE_PREV:96,
INTERFACE_STAT_TABLE_NEXT:97,
INTERFACE_STAT_TABLE_JUMP:98
},E.logKeys={
MSG_NETWORK_OVERTIME:41,
MSG_JS_OVERTIME:43,
MSG_KEYWORD_NETWORK_OVERTIME:44,
MSG_KEYWORD_JS_OVERTIME:45,
USER_SUMMARY_NETWORK_OVERTIME:46,
USER_SUMMARY_JS_OVERTIME:47,
USER_ATTR_NETWORK_OVERTIME:48,
USER_ATTR_JS_OVERTIME:49,
ARTICLE_ANALYSE_NETWORK_OVERTIME:50,
ARTICLE_ANALYSE_JS_OVERTIME:51,
ARTICLE_DETAIL_NETWORK_OVERTIME:52,
ARTICLE_DETAIL_JS_OVERTIME:53,
INTERFACE_NETWORK_OVERTIME:58,
INTERFACE_JS_OVERTIME:59
},E.ajaxReport=function(_,E,A){
R.ajax({
url:wx.url("/misc/fdevreport?id=%s&key=%s&uin=%s&cost_time=%s".sprintf("10001",_,A,E)),
type:"GET",
success:function(){}
});
};
var e={};
E.time=function(_){
e[_]=+new Date;
},E.timeEnd=function(_){
if(e[_]){
var E=+new Date-e[_];
return E;
}
},E.loopHour=function(_,E,T){
for(var R=_,I=E+1;I>R;R++)T(A(R));
},E.numberToTime=A,E.loopDay=function(_,E,A,T){
if(T)for(var R=E,e=+I(E).format("X"),i=+I(_).format("X");e>=i;)A(R),R=I(R).subtract(1,"days").format(S),
e=+I(R).format("X");else for(var R=_,e=+I(_).format("X"),i=+I(E).format("X");i>=e;)A(R),
R=I(R).add(1,"days").format(S),e=+I(R).format("X");
},E.help=function(_,E){
var A=$(E),T=null,I=[_,E].join(", ");
R(I).mouseover(function(){
A.show(),clearTimeout(T);
}),R(I).mouseout(function(){
clearTimeout(T),T=setTimeout(function(){
A.hide();
},300);
});
},E.clickReport=function(_){
var E=0,A=wx.data.uin;
R.ajax({
url:wx.url("/misc/fdevreport?id=%s&key=%s&uin=%s&cost_time=%s".sprintf("10001",_,A,E)),
type:"GET",
success:function(){}
});
},E.typesMap={
"Apple-iPhone4;1":"iPhone 4S",
"Apple-iPhone4;2":"iPhone 4S(GSM)",
"Apple-iPhone5;1":"iPhone 5",
"Apple-iPhone5;2":"iPhone 5(GSM)",
"Apple-iPhone6;1":"iPhone 5S",
"Apple-iPhone6;2":"iPhone 5S(GSM)",
"Apple-iPhone3;1":"iPhone 4",
"Apple-iPhone3;2":"iPhone 4(GSM)",
"Apple-iPhone7;1":"iPhone 6 Plus",
"Apple-iPhone7;2":"iPhone 6",
"Apple-iPhone2;1":"iPhone 3GS",
"Apple-iPhone5;4":"iPhone 5C(GSM/CDMA)",
"Apple-iPhone5;3":"iPhone 5C",
"Apple-iPhone3;3":"iPhone 4(CDMA)",
"Apple-iPad2;5":"iPad Mini(WIFI)",
"Apple-iPad4;4":"iPad Mini 2(WIFI)",
"Apple-iPad4;1":"iPad Air(WIFI)",
"Apple-iPad3;4":"iPad 4(WIFI)",
"Apple-iPad5;3":"iPad Air 2(GSM)",
"Apple-iPad2;1":"iPad 2(WIFI)",
"Apple-iPad3;1":"iPad 3(WIFI)",
"Apple-iPad2;2":"iPad 2(GSM)",
"Apple-iPad2;7":"iPad Mini(GSM+CDMA)",
"Apple-iPad4;7":"iPad Mini 2",
"Apple-iPad2;4":"iPad 2(New WIFI)",
"Apple-iPad3;3":"iPad 3(GSM)",
"Apple-iPad4;2":"iPad Air(GSM+CDMA)",
"Apple-iPad3;6":"iPad 4(GSM+CDMA)",
"Apple-iPod4;1":"iPod Touch 4",
"Apple-iPod5;1":"iPod Touch 5"
},E.mGet=function(_,E,A){
for(var R=1-_.length,I=[],S=!1,e=0,i=_.length;i>e;e++)!function(e,i){
T.get({
url:wx.url(_[i]),
success:function(_){
if(!S){
if(A&&A(e,i,_),0!==_.base_resp.ret)return void(S=!0);
R++,I[i]=_,1===R&&E&&E.apply(E,I);
}
}
});
}(+new Date,e);
},E.makeUrl=function(_,E){
for(var A in E)E[A]&&(_+="&"+A+"="+encodeURI(E[A]));
return wx.url(_);
},E.transformTailZero=function(_){
return _=""+_,-1===_.indexOf(".")?_:("0"===_[_.length-1]&&(_=_.slice(0,_.length-1)),
"0"===_[_.length-1]&&(_=_.slice(0,_.length-2)),_);
},E.delegateClickReport=function(_,A){
R(document).on("click",_,null,function(){
E.clickReport(A);
});
},E.directClickReport=function(_,A){
R(_).on("click",function(){
E.clickReport(A);
});
};
});define("statistics/user_stat/common.js",["biz_common/moment.js","common/wx/Cgi.js"],function(o,t){
"use strict";
function n(){
console.log.apply(console,arguments);
}
function e(o,t,n,e){
if(e)for(var r=t,m=+s(t).format("X"),i=+s(o).format("X");m>=i;)n(r),r=s(r).subtract(1,"days").format(a),
m=+s(r).format("X");else for(var r=o,m=+s(o).format("X"),i=+s(t).format("X");i>=m;)n(r),
r=s(r).add(1,"days").format(a),m=+s(r).format("X");
}
function r(o,t){
var n=$(t),e=null,r=[o,t].join(", ");
i(r).mouseover(function(){
n.show(),clearTimeout(e);
}),i(r).mouseout(function(){
clearTimeout(e),e=setTimeout(function(){
n.hide();
},300);
});
}
function m(o,t,n){
for(var e=1-o.length,r=[],m=!1,s=0,a=o.length;a>s;s++)!function(s,a){
u.get({
url:wx.url(o[a]),
success:function(o){
if(!m){
if(n&&n(s,a,o),0!==o.base_resp.ret)return void(m=!0);
e++,r[a]=o,1===e&&t&&t.apply(t,r);
}
}
});
}(+new Date,s);
}
var s=o("biz_common/moment.js"),t={},a="YYYY-MM-DD",i=jQuery,u=o("common/wx/Cgi.js");
return t.help=r,t.log=n,t.loopDay=e,t.mGet=m,t;
});define("statistics/user_stat/summary/summary-chart.js",["statistics/chart/jquery-chart.js","statistics/user_stat/summary/summary-state.js","statistics/user_stat/common.js","biz_common/moment.js"],function(a,e){
"use strict";
function t(){
var a,e=[],t=[];
a="new_user"===m.index&&"99999999"!==m.source?m.sourceData:m.userStatList,d(m.drawBeginDate,m.drawEndDate,function(s){
t.push(s);
var r=i(a,s);
e.push({
name:s,
y:r
});
});
var r=D[m.index];
"new_user"===m.index&&(r=r+"-"+m.sourceText),y.series=[{
name:r,
yAxis:0,
dataFormat:1,
data:e
}],y.categories=t,o.createChart(y),s();
}
function s(){
u("div.nodata h4").html("暂无数据");
}
function r(){
var a,e,t=[],r=[],u=[];
if("new_user"===m.index&&"99999999"!==m.source?(a=m.sourceData,e=m.compareSourceData):(a=m.userStatList,
e=m.compareUserStatList),e.length>a.length)var c=m.beginCompareDate,l=m.endCompareDate,_=!1,g=m.drawBeginDate;else var c=m.drawBeginDate,l=m.drawEndDate,_=!0,g=m.beginCompareDate;
d(c,l,function(s){
t.push(s),_?(r.push({
name:s,
y:i(a,s)
}),u.push({
name:g,
y:i(e,g)
})):(r.push({
name:g,
y:i(a,g)
}),u.push({
name:s,
y:i(e,s)
})),g=h(g).add(1,"days").format(p);
});
var f=D[m.index];
"new_user"===m.index&&(f=f+"-"+m.sourceText),y.series=[{
name:n(m.drawBeginDate,m.drawEndDate)+f,
yAxis:0,
dataFormat:1,
data:r
},{
name:n(m.beginCompareDate,m.endCompareDate)+f,
yAxis:0,
dataFormat:1,
data:u
}],y.categories=t,o.createChart(y),s();
}
function n(a,e){
var t="MM.DD";
return h(a).format(t)+"至"+h(e).format(t);
}
function i(a,e){
var t,s;
return a[e]?(s=a[e],t=s[m.index],s.isPatch&&0===t&&(t=null),t):null;
}
a("statistics/chart/jquery-chart.js");
var u=jQuery,m=a("statistics/user_stat/summary/summary-state.js"),e={},o=u("#js_msg_chart"),c=a("statistics/user_stat/common.js"),d=(c.log,
c.loopDay),h=a("biz_common/moment.js"),p="YYYY-MM-DD",y={
autoStep:!0,
chartType:"area",
title:"",
height:300,
isCompareSeries:0,
dataFormat:"1",
labelFormat:0,
enableLegend:!0,
theme:"wechat",
chartOptions:{
yAxis:{
min:null
}
}
};
e.draw=function(){
m.needCompare?(y.isCompareSeries=1,r()):(y.isCompareSeries=0,t());
};
var D={
new_user:"新关注人数",
cancel_user:"取消关注人数",
netgain_user:"净增关注人数",
cumulate_user:"累积关注人数"
};
return e;
});define("statistics/user_stat/summary/summary-state.js",[],function(){
"use strict";
return{};
});define("common/wx/report_util.js",["common/wx/Tips.js","biz_common/moment.js","biz_web/lib/highcharts.js","biz_web/ui/dropdown.js","biz_web/ui/dateRange.js","common/wx/pagebar.js"],function(t){
"use strict";
function e(t){
var t=$.extend(!0,{
begintime:l().add("d",-7).format("YYYY-MM-DD"),
endtime:u,
dateDom:null,
dropDom:null,
callback:$.noop
},t),e=d({
container:t.dateDom,
isTodayValid:t.isTodayValid||!1,
startDate:t.begintime,
endDate:t.endtime,
defaultText:" 至 ",
theme:"ta",
success:function(o){
var a=o.startDate,n=o.endDate;
$("#"+e.inputId).html(a+" 至 "+n),t.callback(a,n);
}
});
$("#"+e.inputId+",#"+e.mOpts.inputTrigger).click(function(){
$(".jsDropdownList").hide();
});
var o="最近7天",a="最近15天",n="最近30天",r={};
r[u+"_"+l().add("d",-30).format("YYYY-MM-DD")]=n,r[u+"_"+l().add("d",-15).format("YYYY-MM-DD")]=a,
r[u+"_"+l().add("d",-7).format("YYYY-MM-DD")]=o;
{
var i=r[t.endtime+"_"+t.begintime]||"最近7天";
new c({
container:t.dropDom,
label:i,
data:[{
name:o,
value:7
},{
name:a,
value:15
},{
name:n,
value:30
}],
callback:function(o){
if(o){
var a=o,n=l().add("d",-a).format("YYYY-MM-DD"),r=l();
t.isTodayValid||r.add("d",-1),r=r.format("YYYY-MM-DD"),$("#"+e.inputId).html(n+" 至 "+r),
t.callback(n,r);
}
}
});
}
}
function o(t){
if(t=$.extend(!0,{
count:10,
container:"#js_pagebar",
currentPage:1,
total_count:0
},t),t.total_count>t.count){
new m({
container:t.container,
perPage:t.count,
initShowPage:t.currentPage,
totalItemsNum:t.total_count,
first:!1,
last:!1,
isSimple:!0,
callback:function(e){
var o=e.currentPage;
t.callback&&t.callback(o);
}
});
}else $(t.container).html("");
}
function a(t){
t=$.extend(!0,{
container:".js_rankFlag",
sortkey:0,
sorttype:0,
asc:1,
desc:0
},t);
var e=(t.sorttype,t.sortkey,t.asc),o=t.desc;
$(t.container).on("click",function(){
var a=$(this).attr("sortkey");
t.sortkey==a?t.sorttype=t.sorttype==e?o:e:(t.sortkey=a,t.sorttype=t.desc);
var n=$(t.container).removeClass("single_up").removeClass("single_down"),r=n.filter("[sortkey="+t.sortkey+"]");
r.addClass(t.sorttype==e?"single_up":"single_down"),t.callback&&t.callback(t,t);
});
}
function n(t){
t=$.extend(!0,{
container:".js_rankFlag",
callback:$.noop,
sortkey:0,
sorttype:0,
asc:1,
desc:0,
up_class:"single_up",
down_class:"single_down"
},t);
var e=(t.sorttype,t.sortkey,t.asc),o=t.desc;
$(t.container).on("click",function(){
var a=$(this).attr("sortkey");
t.sortkey==a?t.sorttype=t.sorttype==e?o:e:(t.sortkey=a,t.sorttype=t.desc),t.callback&&t.callback(t,t);
});
var a=$(t.container).removeClass(t.up_class).removeClass(t.down_class),n=a.filter("[sortkey="+t.sortkey+"]");
n.addClass(t.sorttype==e?t.up_class:t.down_class);
}
function r(t){
t=$.extend({
domId:null,
data:[]
},t),new s.Chart({
chart:{
renderTo:t.domId,
zoomType:"xy",
type:"area",
marginLeft:50,
marginRight:50,
backgroundColor:"#FFFFFF",
height:400
},
plotOptions:{
series:{
fillColor:"rgba(135, 179, 212, 0.05)"
}
},
title:{
text:""
},
credits:{
enabled:!1
},
xAxis:[{
labels:{
formatter:function(){
return this.value;
},
style:{
color:"#8D8D8D"
},
step:Math.ceil(length/7)
},
title:{
text:"",
style:{
color:"#7eafdd"
}
},
categories:[],
tickmarkPlacement:"on",
lineColor:"#C6C6C6",
lineWidth:2
}],
yAxis:[{
title:{
text:""
},
labels:{
formatter:function(){
return this.value>0?this.value:"";
},
style:{
color:"#8D8D8D",
fontFamily:"Microsoft yahei"
}
},
gridLineColor:"#F2F3F4",
allowDecimals:!1
}],
legend:{
enabled:!1
},
tooltip:{
backgroundColor:"#555556",
borderRadius:0,
borderWidth:0,
shadow:!1,
style:{
color:"#fff"
},
headerFormat:"",
pointFormat:'<b style="font-family:Microsoft yahei">{point.y}<br/>{point.date}</b>'
},
series:[{
name:"",
color:"#44B549",
lineWidth:2,
marker:{
radius:5,
lineWidth:3,
lineColor:"#fff"
},
states:{
hover:{
enabled:!0,
lineWidth:2
}
},
data:t.data
}],
exporting:{
enabled:!1
}
});
}
function i(t){
t=$.extend({
domId:null,
data:[]
},t),new s.Chart({
chart:{
renderTo:t.domId,
type:"bar",
zoomType:"xy",
height:150,
backgroundColor:"#FFFFFF"
},
title:{
text:"",
style:{
color:"#3E576F",
fontWeight:"bold",
fontFamily:"Microsoft yahei"
}
},
colors:["#EBCB6B","#7FB887","#7FAEDF"],
xAxis:{
categories:[0],
tickmarkPlacement:"on",
lineWidth:1
},
yAxis:{
labels:{
formatter:function(){
return this.value>0?this.value:"";
},
style:{
color:"#3E576F",
fontFamily:"Microsoft yahei"
}
},
min:0,
title:{
text:""
},
gridLineColor:"#F2F3F4",
allowDecimals:!1,
stackLabels:{
enabled:!0,
style:{
fontWeight:"bold",
color:"gray"
}
}
},
legend:{
enabled:!0,
backgroundColor:"#FFFFFF"
},
plotOptions:{
series:{
fillColor:"rgba(135, 179, 212, 0.05)",
stacking:"normal"
}
},
credits:{
enabled:!1
},
tooltip:{
backgroundColor:"#555556",
borderRadius:0,
borderWidth:0,
shadow:!1,
style:{
color:"#fff"
},
headerFormat:"",
pointFormat:'<b style="font-family:Microsoft yahei">{series.name}：{point.y}<br/>{point.date}</b>'
},
exporting:{
enabled:!1
},
series:t.data
});
}
{
var l=(t("common/wx/Tips.js"),t("biz_common/moment.js")),s=t("biz_web/lib/highcharts.js"),c=t("biz_web/ui/dropdown.js"),d=t("biz_web/ui/dateRange.js"),m=t("common/wx/pagebar.js"),u=l().add("d",-1).format("YYYY-MM-DD");
l().format("YYYY-MM-DD");
}
return{
initDateRange:e,
initPager:o,
initRankOnce:a,
initChart:r,
initRank:n,
initBarChart:i
};
});define("statistics/components/date-range.js",["tpl/statistics/date-range.tpl.js","statistics/components/event-emitter.js","biz_web/ui/dateRange.js","biz_common/moment.js"],function(t){
"use strict";
function e(t){
i.call(this),t=this.settings=$.extend({},h,t),this.dateObj={
needCompare:!1,
startDate:t.startDate,
endDate:t.endDate,
startCompareDate:t.startCompareDate||"",
endCompareDate:t.endCompareDate||""
},this.cid=d++,this.isEmit=!0,this.el=s(),this.$el=$(this.el),this._initTabToDateMap(),
this._listenActions();
var e=this;
setTimeout(function(){
e._initDateRange(),e._initState();
});
}
function a(t){
var e=r().subtract(1,"days").format(o),a=r().subtract(parseInt(t),"days").format(o),s={
startDate:a,
endDate:e
};
return s;
}
var s=template.compile(t("tpl/statistics/date-range.tpl.js")),i=t("statistics/components/event-emitter.js"),n=t("biz_web/ui/dateRange.js"),r=t("biz_common/moment.js"),o="YYYY-MM-DD",d=0,c="js_date_container",h={},l=$.extend(e.prototype,i.prototype);
return l._initTabToDateMap=function(){
var t=this;
this.tabsMap={},this.$el.find(".btn_default").each(function(e,s){
var i=$(s),n=i.attr("range"),r=a(n);
t.tabsMap[r.startDate+r.endDate]=e;
});
},l._initState=l.resetState=function(){
var t={
startDate:this.settings.startDate,
endDate:this.settings.endDate
};
this.setCompareDateWithDate(t),this._selectTabByMyDate(),this.clearCompare(),this.settings.single&&this.$el.find(".btn_default").remove();
},l.setDate=function(t){
this.setCompareDateWithDate(t),this._selectTabByMyDate();
},l.setCompareDateWithDate=function(t){
this.dateObj=t;
var e=r(t.startDate),a=r(t.endDate),s=parseInt(a.format("DDDD"))-parseInt(e.format("DDDD")),i=r(t.startDate).subtract(1,"days"),n=r(t.startDate).subtract(s+1,"days");
this.dateRange.setDate({
startDate:e.format(o),
endDate:a.format(o),
startCompareDate:n.format(o),
endCompareDate:i.format(o)
});
},l.emitDate=function(){
this.emit("date-change",this.dateObj);
},l.clearCompare=function(){
this._showSelection(),this.dateObj.needCompare=!1,this._enableCompareBtn();
},l._showSelection=function(){
this.$el.find(".btn_default").show();
},l._hideSelection=function(){
this.$el.find(".btn_default").hide();
},l._enableCompareBtn=function(){
this.$el.find("#"+this.compareBtnId).text("按时间对比");
},l._disableCompareBtn=function(){
this.$el.find("#"+this.compareBtnId).text("取消对比");
},l._listenActions=function(){
var t=this;
this.$el.find(".btn_default").on("click",function(){
var e=$(this),s=e.attr("range");
if(t.range!==s){
t.range=s;
var i=a(s);
t.setCompareDateWithDate(i),t.emitDate(),e.addClass("selected").siblings().removeClass("selected");
}
}),this.$el.find("#btn_compare").on("click",function(){
t.dateObj.needCompare=!t.dateObj.needCompare,t.dateObj.needCompare?t._hideSelection():t._showSelection();
});
},l._initDateRange=function(){
var t=this,e=c+this.cid,a="js_compare_btn"+this.cid;
if(this.compareBtnId=a,this.$el.find(".td_data_container").eq(0).attr("id",e),this.$el.find(".btn_primary").eq(0).attr("id",a),
this.settings.single)var s=!0;
this.dateRange=n({
container:"#"+e,
stopToday:!0,
isTodayValid:!1,
startDate:this.dateObj.startDate||"",
endDate:this.dateObj.endDate||"",
singleCompare:this.settings.single,
autoSubmit:s,
startCompareDate:this.dateObj.startCompareDate||"",
endCompareDate:this.dateObj.endCompareDate||"",
needCompare:1,
theme:"ta",
monthRangeMax:120,
compareTrigger:"compare_trigger_"+this.cid,
replaceBtn:a,
success:function(e){
e.needCompare=t.dateObj.needCompare,t.dateObj.needCompare||t.setCompareDateWithDate(e),
t.dateObj=e,t.emitDate(),t._selectTabByMyDate();
}
});
},l._selectTabByMyDate=function(){
var t=this._getTabIndexByDate(this.dateObj),e=-1;
t!==e?this._selectTab(t):(this.range=null,this._deselectAll());
},l._selectTab=function(t){
this.$el.find(".btn_default").eq(t).addClass("selected").siblings().removeClass("selected");
},l._deselectAll=function(){
this.$el.find(".btn_default").removeClass("selected");
},l._getTabIndexByDate=function(t){
var e=t.startDate+t.endDate;
for(var a in this.tabsMap)if(a===e)return this.tabsMap[a];
return-1;
},e;
});define("statistics/components/tab-bar.js",["tpl/statistics/tab-bar.tpl.js","statistics/components/event-emitter.js"],function(t){
"use strict";
function s(t){
e.call(this);
var t=t||{};
this.settings=$.extend({},n,t),this.el=null,this.$el=null,this.render(),this.listenActions(),
this.activate(0);
}
var i=template.compile(t("tpl/statistics/tab-bar.tpl.js")),e=t("statistics/components/event-emitter.js"),n={
name:"Untitled Tab Bar",
tabs:[{}]
},a=$.extend(s.prototype,e.prototype);
return a.render=function(){
this.el=i(this.settings),this.$el=$(this.el);
},a.listenActions=function(){
var t=this;
this.$el.find("div.tabs a").each(function(s,i){
var e=$(i);
e.on("click",function(i){
i.stopPropagation(),t.activate(s);
});
});
},a.activate=function(t,s){
this.$el.find("div.tabs a.current").removeClass("current"),this.$el.find("div.tabs a").eq(t).addClass("current");
var i=this.settings.tabs[t];
s||this.emit("tab-selected",t,i);
},s;
});