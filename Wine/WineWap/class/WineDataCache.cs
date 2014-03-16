using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;

namespace WineWap
{
    public class WineDataCache
    {
        private static List<int> jingpinIDList = new List<int>();
        private static List<WineInstanceList> m_wineInstanceList = new List<WineInstanceList>();

        public void InitData()
        {
            object obj = new object();
            lock (obj)
            {
                //清理掉原始数据
                jingpinIDList.Clear();
                m_wineInstanceList.Clear();

                InitJingpin();
                //白酒
                InitWineData(1);
                //葡萄酒
                InitWineData(6);
                //啤酒
                InitWineData(16);
                //红酒
                InitWineData(12);
                //保健酒
                InitWineData(17);

                var getJP = System.Configuration.ConfigurationManager.AppSettings["GetJP"];
                bool getJPBool = false;
                if (bool.TryParse(getJP, out getJPBool))
                {
                    m_wineInstanceList.ForEach(delegate(WineInstanceList wineInstanceList)
                    {
                        var wineList = wineInstanceList.WineList.GetRange(0, wineInstanceList.WineList.Count);
                        wineInstanceList.WineList.Clear();
                        wineList.ForEach(delegate(WineInstance wine)
                        {
                            if (jingpinIDList.Contains(wine.GetDetailID()))
                            {
                                wineInstanceList.AppendWine(wine);
                            }
                        });
                    });
                }

                //特价专区
                InitWineData(20);
                //新品上架
                InitWineData(21);
                //精品推荐
                InitWineData(22);
                //热门产品
                InitWineData(23);
            }
        }

        public string GetHtml(int wineType, bool isMore, int currentPage)
        {
            var wineInstanceList = m_wineInstanceList.Find(wineInstance => wineInstance.TypeID == wineType);
            var count = 0;
            var title = string.Empty;
            var outStringBuilder = new StringBuilder();

            if (wineInstanceList != null)
            {
                var wineList = wineInstanceList.WineList.GetRange(currentPage * 10, (currentPage + 1) * 10 > wineInstanceList.WineList.Count ? wineInstanceList.WineList.Count - currentPage * 10 : 10);
                title = wineInstanceList.Title;
                if (wineList != null && wineList.Count > 0)
                {
                    count = wineList.Count;
                    wineList.ForEach(wine => outStringBuilder.Append(wine.ToString()));
                }
            }

            var result = new
            {
                Title = title,
                Data = outStringBuilder.ToString(),
                IsOver = count == 10 ? false : true
            };

            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }

        private void InitJingpin()
        {
            jingpinIDList = new List<int>();
            var title = string.Empty;
            var data = GetData(-1, out title);

            data.ForEach(delegate(string html)
            {
                var detail = new WineInstance(html);
                jingpinIDList.Add(detail.GetDetailID());
            });
        }

        private void InitWineData(int wineTypeID)
        {
            var title = string.Empty;
            var wineList = GetData(wineTypeID, out title);
            var wineListCache = m_wineInstanceList.Find(wine => wine.TypeID == wineTypeID);
            if (wineListCache == null)
            {
                wineListCache = new WineInstanceList();
                wineListCache.Title = title;
                wineListCache.TypeID = wineTypeID;
                m_wineInstanceList.Add(wineListCache);
            }
            wineList.ForEach(detail => wineListCache.AppendWine(new WineInstance(detail)));
        }

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        private List<string> GetData(int id, out string title, string className = "goodsItem")
        {
            var isMore = false;
            WineManager oWineManager = null;
            int getCount = 0;
            var data = new List<string>();
            do
            {
                oWineManager = new WineManager(id, isMore, getCount + 1);
                title = oWineManager.WineType.Title;
                data.AddRange(oWineManager.GetData("goodsItem"));
                isMore = true;
                getCount++;
            }
            while (data.Count >= getCount * 16);

            return data;
        }
    }


    public class WineInstanceList
    {
        public int TypeID { get; set; }
        public string Title { get; set; }
        public List<WineInstance> WineList { get; private set; }

        public WineInstanceList()
        {
            WineList = new List<WineInstance>();
        }

        public void AppendWine(WineInstance wine)
        {
            WineList.Add(wine);
        }

        public string ToString(int id, int page)
        {
            var selectWine = WineList.FindAll(wine => wine.ID == id);
            var outputWine = selectWine.GetRange((page - 1) * 16, 16);

            var outputString = new StringBuilder();
            outputWine.ForEach(wine => outputString.Append(wine.ToString()));

            return outputString.ToString();
        }
    }

    public class WineInstance
    {
        public int ID { get; private set; }
        public string detailUrl { get; set; }
        public string imageUrl { get; set; }
        public string name { get; set; }
        public string price { get; set; }
        public string marketPrice { get; set; }
        public string reducePercent
        {
            get
            {
                float pricef = 0;
                float marketPricef = 0;
                if (float.TryParse(price, out pricef) && float.TryParse(marketPrice, out marketPricef))
                {
                    return System.Math.Round((float)10 * (float.Parse(price.Replace("￥", "")) / float.Parse(marketPrice.Replace("￥", ""))), 1).ToString() + "折";
                }
                return "";
            }
        }
        /// <summary>
        /// 获取ID
        /// </summary>
        /// <returns></returns>
        public int GetDetailID()
        {
            if (string.IsNullOrEmpty(detailUrl))
                return 0;

            Regex oRegex = new Regex(@"id=\d+", RegexOptions.IgnoreCase);
            Match m = oRegex.Match(detailUrl);
            while (m.Success)
            {
                return int.Parse(m.Value.ToLower().Replace("id=", ""));
            }
            return 0;
        }

        public WineInstance(string html)
        {
            //解析51网Html
            XmlDocument oXmlDocument = new XmlDocument();
            oXmlDocument.LoadXml(html);

            var detailNod = oXmlDocument.FirstChild.SelectSingleNode("a");
            detailUrl = detailNod.Attributes["href"].Value;

            var imageNod = detailNod.SelectSingleNode("img");
            imageUrl = imageNod.Attributes["src"].Value;

            var nameNod = oXmlDocument.FirstChild.SelectSingleNode("p/a");
            name = string.IsNullOrEmpty(nameNod.Attributes["title"].Value) ? nameNod.InnerText : nameNod.Attributes["title"].Value;

            var priceNodList = oXmlDocument.GetElementsByTagName("font");
            foreach (XmlNode nod in priceNodList)
            {
                if (string.Equals(nod.Attributes["class"].Value, "market_s"))
                {
                    marketPrice = nod.InnerText;
                }
                if (string.Equals(nod.Attributes["class"].Value, "shop_s"))
                {
                    price = nod.InnerText;
                }
            }
        }

        public override string ToString()
        {
            StringBuilder outHtmlBuilder = new StringBuilder();
            outHtmlBuilder.Append("<li>");
            outHtmlBuilder.Append("<a class=\"img\" href=\"{0}\"></a>");
            outHtmlBuilder.Append("<span class=\"frame\"><img src=\"{1}\" /></span>");
            outHtmlBuilder.Append("<div class=\"info\">");
            outHtmlBuilder.Append("<div class=\"title\">");
            outHtmlBuilder.Append("<span class=\"name\"><a href=\"{0}\">{2}</a></span>");
            outHtmlBuilder.Append("</div>");
            outHtmlBuilder.Append("<div class=\"price clearfix\">");
            outHtmlBuilder.Append("<p class='jx-price'>{3}</p>");
            outHtmlBuilder.Append("<p class=\"market-price\"><S>{4}</S></p>");
            outHtmlBuilder.Append("<p class=\"count\">{5}</p>");
            outHtmlBuilder.Append("</div>");
            //outHtmlBuilder.Append("<div class=\"judge\">");
            //outHtmlBuilder.Append("<span class=\"score\">5分</span><span class=\"article\">0人评论</span>");
            //outHtmlBuilder.Append("</div>");
            outHtmlBuilder.Append("</div>");
            outHtmlBuilder.Append("</div>");
            return string.Format(outHtmlBuilder.ToString(), detailUrl, imageUrl, name, price, marketPrice, reducePercent);
        }
    }
}