using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Xml;

namespace WineWap
{
    public class WineManager
    {
        private bool m_IsMore = false;
        public WineType WineType = null;

        public WineManager(int id, bool isMore, int currentPage)
        {
            WineType = new WineType(id, currentPage);
            m_IsMore = isMore;
        }

        public List<string> GetData(string className)
        {
            WebClient oWebClient = new WebClient();
            string realUrl = m_IsMore ? WineType.MoreUrl : WineType.OriginalUrl;
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
    }

    public class WineType
    {
        /// <summary>
        /// 精品
        /// </summary>
        private const string jingpin = "http://www.519ds.com/search.php?intro=best";
        private const string jingpinMore = "http://www.519ds.com/search.php?keywords=&category=0&brand=0&sort=goods_id&order=DESC&min_price=0&max_price=0&action=&intro=best&goods_type=0&sc_ds=0&outstock=0&page=2";

        /// <summary>
        /// 所有列表，所有放在一块的时候，没有更多选项
        /// </summary>
        private const string All = "http://www.519ds.com/category.php";
        /// <summary>
        /// 白酒
        /// </summary>
        private const string baijiuTitle = "白酒专区";
        private const string baijiu = "http://www.519ds.com/category.php?id=1";
        private const string baijiuMore = "http://www.519ds.com/category.php?id=1&price_min=0&price_max=0&page={0}&sort=goods_id&order=DESC";
        /// <summary>
        /// 啤酒
        /// </summary>
        private const string pijiuTitle = "啤酒专区";
        private const string pijiu = "http://www.519ds.com/category.php?id=16";
        private const string pijiuMore = "http://www.519ds.com/category.php?id=16&price_min=0&price_max=0&page={0}&sort=goods_id&order=DESC";
        /// <summary>
        /// 保健酒
        /// </summary>
        private const string baojianjiuTitle = "保健酒专区";
        private const string baojianjiu = "http://www.519ds.com/category.php?id=17";
        private const string baojianjiuMore = "http://www.519ds.com/category.php?id=17&price_min=0&price_max=0&page={0}&sort=goods_id&order=DESC";
        /// <summary>
        /// 葡萄酒
        /// </summary>
        private const string putaojiuTitle = "葡萄酒专区";
        private const string putaojiu = "http://www.519ds.com/category.php?id=6";
        private const string putaojiuMore = "http://www.519ds.com/category.php?id=6&price_min=0&price_max=0&page={0}&sort=goods_id&order=DESC";

        /// <summary>
        /// 红酒
        /// </summary>
        private const string hongjiuTitle = "红酒专区";
        private const string hongjiu = "http://www.519ds.com/category.php?id=12";
        private const string hongjiuMore = "http://www.519ds.com/category.php?id=12&price_min=0&price_max=0&page={0}&sort=goods_id&order=DESC";

        /// <summary>
        /// 特价专区
        /// </summary>
        private const string promotionTitle = "特价专区";
        private const string promotion = "http://www.519ds.com/search.php?intro=promotion";
        private const string promotionMore = "";

        /// <summary>
        /// 新品上架
        /// </summary>
        private const string newWineTitle = "新品上架";
        private const string newWine = "http://www.519ds.com/search.php?intro=new";
        private const string newWineMore = "";

        /// <summary>
        /// 精品推荐
        /// </summary>
        private const string bestTitle = "精品推荐";
        private const string best = "http://www.519ds.com/search.php?intro=best";
        private const string bestMore = "http://www.519ds.com/search.php?keywords=&category=0&brand=0&sort=goods_id&order=DESC&min_price=0&max_price=0&action=&intro=best&goods_type=0&sc_ds=0&outstock=0&page={0}";

        /// <summary>
        /// 热门产品
        /// </summary>
        private const string hotTitle = "热门产品";
        private const string hot = "http://www.519ds.com/search.php?intro=hot";
        private const string hotMore = "http://www.519ds.com/search.php?keywords=&category=0&brand=0&sort=goods_id&order=DESC&min_price=0&max_price=0&action=&intro=best&goods_type=0&sc_ds=0&outstock=0&page={0}";

        public string OriginalUrl = string.Empty;
        public string MoreUrl = string.Empty;
        public string Title = string.Empty;

        public WineType(int id, int currentPage)
        {
            switch (id)
            {
                case -1:
                    OriginalUrl = jingpin;
                    MoreUrl = jingpinMore;
                    break;
                case 0:
                    OriginalUrl = All;
                    break;
                case 1:
                    Title = baijiuTitle;
                    OriginalUrl = baijiu;
                    MoreUrl = string.Format(baijiuMore, currentPage);
                    break;
                case 6:
                    Title = putaojiuTitle;
                    OriginalUrl = putaojiu;
                    MoreUrl = string.Format(putaojiuMore, currentPage);
                    break;
                case 12:
                    Title = hongjiuTitle;
                    OriginalUrl = hongjiu;
                    MoreUrl = hongjiuMore;
                    break;
                case 16:
                    Title = pijiuTitle;
                    OriginalUrl = pijiu;
                    MoreUrl = string.Format(pijiuMore, currentPage);
                    break;
                case 17:
                    Title = baojianjiuTitle;
                    OriginalUrl = baojianjiu;
                    MoreUrl = string.Format(baojianjiuMore, currentPage);
                    break;
                case 20:
                    Title = promotionTitle;
                    OriginalUrl = promotion;
                    MoreUrl = promotionMore;
                    break;
                case 21:
                    Title = newWineTitle;
                    OriginalUrl = newWine;
                    MoreUrl = newWineMore;
                    break;
                case 22:
                    Title = bestTitle;
                    OriginalUrl = best;
                    MoreUrl = string.Format(bestMore, currentPage);
                    break;
                case 23:
                    Title = hotTitle;
                    OriginalUrl = hot;
                    MoreUrl = string.Format(hotMore, currentPage);
                    break;
            }
        }

        public static string GetTitleByID(int id)
        {
            string title = string.Empty;
            switch (id)
            {
                case 1:
                    title = baijiuTitle;
                    break;
                case 6:
                    title = putaojiuTitle;
                    break;
                case 12:
                    title = hongjiuTitle;
                    break;
                case 16:
                    title = pijiuTitle;
                    break;
                case 17:
                    title = baojianjiuTitle;
                    break;
            }
            return title;
        }
    }
}