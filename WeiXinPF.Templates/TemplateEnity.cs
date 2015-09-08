using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinPF.Templates
{
    public class TemplateEnity
    {

    }

    /// <summary>
    /// 模版类型 
    /// </summary>
    public enum TemplateType
    {
        /// <summary>
        /// 首页
        /// </summary>
        Index,
        /// <summary>
        /// 底部菜单部分
        /// </summary>
        Bottom,
        /// <summary>
        /// 列表
        /// </summary>
        Class,
        /// <summary>
        /// 详情
        /// </summary>
        News,
        /// <summary>
        /// 频道,二级页面
        /// </summary>
        Channel,
        /// <summary>
        /// 购物车页面
        /// </summary>
        Cart,
        /// <summary>
        /// 确定订单页面
        /// </summary>
        confirmOrder,
        /// <summary>
        /// 编辑地址
        /// </summary>
        editaddr,
        /// <summary>
        /// 用户中心
        /// </summary>
        userinfo,
        /// <summary>
        /// 下单成功页面
        /// </summary>
        orderSuccess,
        /// <summary>
        /// 订单详情
        /// </summary>
        orderDetail
    }

    


}
/**************************************
 *
 * author:李朴
 * company:上海沐雪网络科技有限公司
 * qq:23002807
 * website:http://uweixin.cn
 * taobao:https://item.taobao.com/item.htm?spm=686.1000925.0.0.5HYEHQ&id=520523216527  
 * createDate:2013-11-1
 * update:2014-12-30
 * 
 ***********************************/