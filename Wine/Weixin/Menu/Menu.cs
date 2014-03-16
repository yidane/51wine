using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Newtonsoft.Json;

namespace Weixin.Menu
{
    public class Menu
    {
        public string name { get; set; }  //是  菜单标题，不超过16个字节，子菜单不超过40个字节  

        public static string CreateTestMenu()
        {
            string menuConfigPath = AppDomain.CurrentDomain.BaseDirectory + @"weixin\Menu\menu.xml";
            if (File.Exists(menuConfigPath))
            {
                XmlDocument menuXml = new XmlDocument();
                menuXml.Load(menuConfigPath);
                var menuList = menuXml.SelectNodes("MenuList/Menu");
                if (menuList != null)
                {
                    var rtnMenuList = new List<Menu>();
                    foreach (XmlNode menuNod in menuList)
                    {
                        rtnMenuList.Add(GetMenu(menuNod, null));
                    }

                    var button = new
                    {
                        button = rtnMenuList
                    };
                    return JsonConvert.SerializeObject(button);
                }
                return "";
            }
            return "";
        }

        private static Menu GetMenu(XmlNode menuNod, Menu menu)
        {
            var typeNode = menuNod.SelectSingleNode("type");
            var nameNode = menuNod.SelectSingleNode("name");
            var keyNode = menuNod.SelectSingleNode("key");
            var urlNode = menuNod.SelectSingleNode("url");
            if (typeNode != null && !string.IsNullOrEmpty(typeNode.InnerText))
            {
                //解读配置节点
                if (!(nameNode != null && !string.IsNullOrEmpty(nameNode.InnerText)))
                {
                    throw new Exception("菜单名称不能为空");
                }
                switch (typeNode.InnerText.ToLower())
                {
                    case "view":
                        menu = menu ?? new ViewMenu();
                        menu.name = nameNode.InnerText;
                        if (urlNode != null && !string.IsNullOrEmpty(urlNode.InnerText))
                        {
                            ((ViewMenu)menu).url = urlNode.InnerText;
                        }
                        break;
                    case "click":
                        menu = menu ?? new ClickMenu();
                        menu.name = nameNode.InnerText;
                        if (keyNode != null && !string.IsNullOrEmpty(keyNode.InnerText))
                        {
                            ((ClickMenu)menu).key = keyNode.InnerText;
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                //二级菜单
                menu = menu ?? new ParentMenu();
                menu.name = nameNode.InnerText;
                var sub_buttonNodeList = menuNod.SelectNodes("sub_button/Menu");
                if (sub_buttonNodeList != null && sub_buttonNodeList.Count > 0)
                {
                    ((ParentMenu)menu).sub_button = ((ParentMenu)menu).sub_button ?? new List<Menu>();
                    foreach (XmlNode subNode in sub_buttonNodeList)
                    {
                        ((ParentMenu)menu).sub_button.Add(GetMenu(subNode, null));
                    }
                }
            }

            return menu;
        }
    }

    public class ViewMenu : Menu
    {
        public string type
        {
            get
            {
                return "view";
            }
        }  //是  菜单的响应动作类型，目前有click、view两种类型  
        public string url { get; set; }  //view类型必须  网页链接，用户点击菜单可打开链接，不超过256字节  
    }

    public class ClickMenu : Menu
    {
        public string type
        {
            get
            {
                return "click";
            }
        }  //是  菜单的响应动作类型，目前有click、view两种类型  
        public string key { get; set; }  //click类型必须  菜单KEY值，用于消息接口推送，不超过128字节  
    }

    public class ParentMenu : Menu
    {
        public List<Menu> sub_button { get; set; }  //否  二级菜单数组，个数应为1~5个  
    }
}