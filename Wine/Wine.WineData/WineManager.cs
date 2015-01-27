using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Xml;
using Wine.Infrastructure.Model.Commodity;
using Wine.WebFacade.Commodity;
using System.IO;

namespace Wine.Data
{
    public class WineManager
    {

        private List<GoodsType> AllGoodsType = null;
        private List<SpecialPriceType> AllSpecialPriceType = null;
        public WineManager()
        {
            AllGoodsType = new GoodsTypeFacade().QueryAllGoodsType();
            AllSpecialPriceType = new SpecialPriceTypeFacade().GetAllSpecialPriceType();
        }

        public void DownloadData()
        {
            //同步商品信息
            var goodsFacade = new GoodsFacade();
            var goodsList = new List<Goods>();
            if (AllGoodsType != null && AllGoodsType.Count > 0)
            {
                AllGoodsType.ForEach(delegate(GoodsType goodsType)
                {
                    var allHtml = GetData(goodsType);
                    allHtml.ForEach(delegate(string html)
                    {
                        var newGoods = ToGoods(html);
                        newGoods.GoodsTypeID = goodsType.TypeID;
                        goodsList.Add(newGoods);
                    });

                });
            }

            goodsFacade.ResetGoodsData(goodsList);

            //同步特价信息
            var specialPriceFacade = new SpecialPriceTypeFacade();
            if (AllSpecialPriceType != null && AllSpecialPriceType.Count > 0)
            {
                AllSpecialPriceType.ForEach(delegate(SpecialPriceType specialPriceType)
                {
                    var allHtml = GetData(specialPriceType);
                    allHtml.ForEach(delegate(string html)
                    {
                        var newGoods = ToGoods(html);
                        specialPriceType.GoodsList.Add(newGoods.GoodsID);
                    });
                });
            }

            specialPriceFacade.ResetSpecialPriceTypeMapping(AllSpecialPriceType);
        }

        private Goods ToGoods(string html)
        {
            Goods rtnGoods = new Goods();
            XmlDocument oXmlDocument = new XmlDocument();
            oXmlDocument.LoadXml(html);

            var detailNod = oXmlDocument.FirstChild.SelectSingleNode("a");
            var detailUrl = detailNod.Attributes["href"].Value;
            rtnGoods.GoodsID = Convert.ToInt32(detailUrl.Remove(0, detailUrl.LastIndexOf('=') + 1));

            var imageNod = detailNod.SelectSingleNode("img");
            rtnGoods.Pictureurl = imageNod.Attributes["src"].Value;

            var nameNod = oXmlDocument.FirstChild.SelectSingleNode("p/a");
            rtnGoods.GoodsName = string.IsNullOrEmpty(nameNod.Attributes["title"].Value) ? nameNod.InnerText : nameNod.Attributes["title"].Value;

            var priceNodList = oXmlDocument.GetElementsByTagName("font");
            foreach (XmlNode nod in priceNodList)
            {
                if (string.Equals(nod.Attributes["class"].Value, "market_s"))
                {
                    rtnGoods.HistoryPrice = Convert.ToDecimal(nod.InnerText.Replace("￥", "").Trim());
                }
                if (string.Equals(nod.Attributes["class"].Value, "shop_s"))
                {
                    rtnGoods.CurrentPrice = Convert.ToDecimal(nod.InnerText.Replace("￥", "").Trim());
                }
            }

            return rtnGoods;
        }

        #region 下载商品数据
        private List<string> GetData(GoodsType goodsType, string className = "goodsItem")
        {
            int page = 1;
            var data = new List<string>();
            do
            {
                data.AddRange(GetDataByPage(page, goodsType, className));
                page++;
            }
            while (data.Count == 16 * (page - 1));//51ds上每页是16个数据，如果刚好是16的倍数，说明下面可能还有。

            return data;
        }

        //[Obsolete("分析应该是51网站有什么特别的设置。在没有检测到从自身页面跳转时候，会自己跳转一次。")]
        private List<string> GetDataByPage(int page, GoodsType goodsType, string className)
        {
            WebClient oWebClient = new WebClient();
            string realUrl = page == 1 ? goodsType.OriginalUrl : string.Format(goodsType.MoreDataUrl, page);
            byte[] data = oWebClient.DownloadData(realUrl);
            string html = Encoding.UTF8.GetString(data);
            var htmlData = GrapManager.GetElementsByClass(html, className);
            var htmlOutput = new List<string>();
            htmlData.ForEach(delegate(string divData)
            {
                htmlOutput.Add(GrapManager.TransUrl(realUrl, divData));
            });

            return htmlOutput;
        }

        private List<string> GetDataByPageNew(int page, GoodsType goodsType, string className)
        {
            //先请求一次
            var httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(goodsType.MoreDataUrl);
            httpWebRequest.Referer = goodsType.MoreDataUrl;
            httpWebRequest.Method = "GET";
            httpWebRequest.AllowAutoRedirect = true;
            httpWebRequest.CookieContainer = new CookieContainer();
            var response = (HttpWebResponse)httpWebRequest.GetResponse();
            var stream = response.GetResponseStream();

            

            Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader streamReader = new StreamReader(stream, encode);

            var cc = httpWebRequest.CookieContainer.GetCookies(response.ResponseUri);

            string html = streamReader.ReadToEnd();
            var htmlData = GrapManager.GetElementsByClass(html, className);
            var htmlOutput = new List<string>();
            htmlData.ForEach(delegate(string divData)
            {
                htmlOutput.Add(GrapManager.TransUrl("", divData));
            });

            return htmlOutput;


        }
        #endregion
    }
}