using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeiXinPF.Common;

namespace WeiXinPF.Web.weixin.wqiche
{
    /// <summary>
    /// cldata 的摘要说明
    /// </summary>
    public class cldata : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/json";
            string action = MXRequest.GetQueryString("myact");

            #region 根据品牌得到车系
            if (action == "chexi")
            {
                BLL.wx_wq_chexi cxBll = new BLL.wx_wq_chexi();
                int pid = MyCommFun.RequestInt("pid");
                int wid = MyCommFun.RequestInt("wid");
                List<Model.wx_wq_chexi> cxList = cxBll.GetModelList(string.Format(" wid={0} and pid={1}", wid, pid));
                context.Response.Write(MyCommFun.SwitchToJson<Model.wx_wq_chexi>(cxList));
                return;
            }
            #endregion

            #region 根据车系得到车型
            if (action == "chexing")
            {
                BLL.wx_wq_chexing cxBll = new BLL.wx_wq_chexing();
                int xid = MyCommFun.RequestInt("xid");
                int wid = MyCommFun.RequestInt("wid");
                List<Model.wx_wq_chexing> cxList = cxBll.GetModelList(string.Format(" wid={0} and xid={1}", wid, xid));
                context.Response.Write(MyCommFun.SwitchToJson<Model.wx_wq_chexing>(cxList));
                return;
            }
            #endregion


            #region 添加预约订单
            if (action == "addorder")
            {
                Dictionary<string, string> jsondict = new Dictionary<string, string>();
                BLL.wx_wq_yyOrder yoBll = new BLL.wx_wq_yyOrder();
                Model.wx_wq_yyOrder yoModel = new Model.wx_wq_yyOrder();
                try
                {
                    int wid = MyCommFun.RequestInt("wid");
                    string name = MyCommFun.QueryString("truename");
                    string openid = MyCommFun.QueryString("openid");
                    string tel = MyCommFun.QueryString("tel");
                    string date = MyCommFun.QueryString("dateline");
                    string part = MyCommFun.QueryString("timepart");
                    string xid = MyCommFun.QueryString("xid");
                    string pid = MyCommFun.QueryString("pid");
                    int type = MyCommFun.RequestInt("type");
                    string carnum = MyCommFun.QueryString("carnum");
                    string km = MyCommFun.QueryString("km");
                    string remark = MyCommFun.QueryString("info");
                    yoModel.Name = name;
                    yoModel.remark = remark;
                    yoModel.sort_id = 99;
                    yoModel.telephone = tel;
                    yoModel.xid = MyCommFun.Str2Int(xid);
                    yoModel.pid = MyCommFun.Str2Int(pid);
                    yoModel.wid = wid;
                    yoModel.yid = type;
                    yoModel.km = MyCommFun.Str2Decimal(km);
                    yoModel.carnum = carnum;
                    yoModel.yydate = MyCommFun.Obj2DateTime(date);
                    yoModel.yytime = part;
                    yoModel.openid = openid;
                    yoModel.ddstatus = "待回复";
                    yoModel.createdate = DateTime.Now;
                    int res = yoBll.Add(yoModel);
                    if (res < 1)
                    {
                        jsondict.Add("errno", "1");
                        context.Response.Write(MyCommFun.getJsonStr(jsondict));
                        return;
                    }
                    jsondict.Add("errno", "0");
                    context.Response.Write(MyCommFun.getJsonStr(jsondict));
                    return;
                }
                catch (Exception)
                {
                    jsondict.Add("errno", "1");
                    context.Response.Write(MyCommFun.getJsonStr(jsondict));
                    return;
                }
            }
            #endregion

            #region 删除订单
            if (action == "delorder")
            {
                Dictionary<string, string> jsondict = new Dictionary<string, string>();
                int id = MyCommFun.RequestInt("id");
                int wid = MyCommFun.RequestInt("wid");
                BLL.wx_wq_yyOrder yoBll = new BLL.wx_wq_yyOrder();
                yoBll.Delete(id);

                return;
            }
            #endregion

            if (action == "addChezhu")
            {
                Dictionary<string, string> jsondict = new Dictionary<string, string>();
                BLL.wx_wq_chezhu czBll = new BLL.wx_wq_chezhu();
                try
                {
                    int pid = MyCommFun.RequestInt("car_model");
                    int wid = MyCommFun.RequestInt("wid");
                    int xid = MyCommFun.RequestInt("car_type");
                    int xingid = MyCommFun.RequestInt("car_xing");
                    string car_no = MyCommFun.QueryString("car_no");
                    string tel = MyCommFun.QueryString("tel");
                    string car_userName = MyCommFun.QueryString("car_userName");
                    string car_startTime = MyCommFun.QueryString("car_startTime");
                    string car_buyTime = MyCommFun.QueryString("car_buyTime");
                    string openid = MyCommFun.QueryString("openid");
                    int res = czBll.GetRecordCount(" openid='" + openid + "' and wid=" + wid);

                    Model.wx_wq_chezhu czModel = new Model.wx_wq_chezhu();
                    if (res > 0)
                        czModel = czBll.GetModelList(" openid='" + openid + "' and wid=" + wid)[0];
                    czModel.cpNum = car_no;
                    czModel.cxingid = xingid;
                    czModel.gcdate = MyCommFun.Obj2DateTime(car_buyTime);
                    czModel.Name = car_userName;
                    czModel.ppid = pid;
                    czModel.cxid = xid;
                    czModel.cxingid = xingid;
                    czModel.spdate = MyCommFun.Obj2DateTime(car_startTime);
                    czModel.teltephone = tel;
                    czModel.sort_id = 99;
                    czModel.wid = wid;
                    czModel.createdate = DateTime.Now;
                    czModel.openid = openid;
                    if (res > 0)
                    {
                        czBll.Update(czModel);
                        jsondict.Add("content", "修改车主信息成功");
                        context.Response.Write(MyCommFun.getJsonStr(jsondict));
                        return;
                    }
                    else
                    {
                        czBll.Add(czModel);
                        jsondict.Add("content", "添加车主信息成功");
                        context.Response.Write(MyCommFun.getJsonStr(jsondict));
                        return;
                    }
                }
                catch (Exception)
                {
                    jsondict.Add("content", "操作失败!");
                    context.Response.Write(MyCommFun.getJsonStr(jsondict));
                    return;
                } 
            } 
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}