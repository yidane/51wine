<%@ Page Language="C#" MasterPageFile="Restaurant.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="WeiXinPF.Web.weixin.restaurant.index" %>

<asp:Content ID="h" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta name="viewport" content="width=device-width,height=device-height,inital-scale=1.0,maximum-scale=1.0,user-scalable=no;">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
    <title><%=hotelName %></title>
    <link href="css/diancai.css?20140325" rel="stylesheet" type="text/css" />
    <style>
        .pic-loading {
            background: url(images/loading.gif) no-repeat 50% 50% #f5f5f5;
            background-size: 50px 50px;
        }

    </style>
</asp:Content>
<asp:Content ID="c" ContentPlaceHolderID="content" runat="server" class="mode_webapp">
    <input type="hidden" name="formhash" id="formhash" value="52ebc03e" />
    <div id="mcover" onclick="document.getElementById('mcover').style.display='';">
        <div id="Popup">
            <div class="imgPopup">
                <img id="picsrc" class="pic-loading" src=""><h3 id="h3title"></h3>
                <p class="jianjie" id="jianjie">
            </div>
        </div>
        <a class="close" onclick="document.getElementById('mcover').style.display='';">X</a>
    </div>
    <script>
        function htmlit(url, title, fid) {
            $.ajax({
                url: "diancai_login.ashx",
                type: "post",
                dataType: "html",
                async: false,
                data: { productId: fid, myact:'productDetail'},
                success: function (result) {                    
                    if (typeof result === "undefined") {
                        alert("无法获取此商品的详情");
                        return;
                    }
                    
                    document.getElementById('mcover').style.display = 'block';
                    document.getElementById('Popup').style.display = 'block';
                    document.getElementById("picsrc").src = url;
                    document.getElementById("picsrc").className = '';
                    document.getElementById("h3title").innerHTML = title;
                    document.getElementById("jianjie").innerHTML = result;
                },
                error: function (error) {
                    alert("无法获取此商品的详情");
                }
            });            
        }
    </script>
    <div style="background-color: #FFF">
        <div id="navBar">
            <dl>
                <dt>分类</dt>
                <%=cateString %>
            </dl>
        </div>

        <div id="infoSection">
            <section class="menu">
                <section class="list listimg">
                    <dl>

                        <div class="ccbg">
                            <%=manageString %>
                        </div>

                    </dl>
                </section>
                <div class="copyright"></div>
            </section>
        </div>

        <div class="clr"></div>
    </div>

    <script src="js/shopCart.js" type="text/javascript"></script>
    <script src="js/BuyProducts.js" type="text/javascript"></script>
    <script>
        var cart = new OAK.Shop.Cart(<%=shopid%>);

        window.onload = function () {
            cart.getFromCache();
            showProductsInShopCart();
            setHeight();
        }
        
        function showProducts(categoryName) {
            showAll();
            var dts = document.getElementsByTagName("dt");
            for (var i in dts) {
                if (dts[i].innerText != null && dts[i].innerText != categoryName) {
                    dts[i].style.display = "none";
                    var dd = dts[i].nextElementSibling;
                    while (dd != null && dd.tagName != 'DT') {
                        dd.style.display = "none";
                        dd = dd.nextElementSibling;
                    }
                }
            }
        }

        function showAll() {
            var dts = document.getElementsByTagName("dt");
            for (var i in dts) {
                if (dts[i].innerText != null) {
                    dts[i].style.display = "";
                    var dd = dts[i].nextElementSibling;
                    while (dd != null && dd.tagName != 'DT') {
                        dd.style.display = "";
                        dd = dd.nextElementSibling;
                    }
                }
            }
            showMenu(false);
        }

        function showMenu(is_show) {
            if (typeof (is_show) == "undefined") {
                if (hasClass(g("menu"), "sort_on"))
                    removeClass(g("menu"), "sort_on");
                else
                    addClass(g("menu"), "sort_on");
            } else {
                if (is_show) {
                    addClass(g("menu"), "sort_on");
                } else {
                    removeClass(g("menu"), "sort_on");
                }
            }
        }

        function hasClass(obj, cls) {
            return obj.className.match(new RegExp('(\\s|^)' + cls + '(\\s|$)'));
        }

        function addClass(obj, cls) {
            if (!this.hasClass(obj, cls)) obj.className += " " + cls;
        }

        function removeClass(obj, cls) {
            if (hasClass(obj, cls)) {
                var reg = new RegExp('(\\s|^)' + cls + '(\\s|$)');
                obj.className = obj.className.replace(reg, ' ');
            }
        }        

        function showProductsInShopCart() {
            var products = cart.getProductList();
            for (var i in products) {
                var product_id = products[i].id;
                var spec_id = products[i].specId;

                if (products[i].categoryId == <%=categoryid%>) {
                    showProductNumberWithBought(product_id, spec_id, cart.getProductNumber({ id: product_id, specId: spec_id }) || 0);
            }
        }
        showShopCartProductsNumber();
        };

        cart.onAfterAdd = function (obj, num, conditions) {
            showProductNumberWithBought(conditions.id, conditions.specId, num);
            showShopCartProductsNumber();
            cart.saveToCache();
        };
        cart.onAfterUpdate = function (obj, num, conditions) {
            showProductNumberWithBought(conditions.id, conditions.specId, num);
            showShopCartProductsNumber();
            cart.saveToCache();
        };
        cart.onAfterDelete = function (obj, conditions) {
            showProductNumberWithBought(conditions.id, conditions.specId, 0);
            showShopCartProductsNumber();
            cart.saveToCache();
        };
    
    </script>
    <script type="text/javascript">
        function setHeight() {
            var cHeight;
            cHeight = document.documentElement.clientHeight;
            cHeight = cHeight + "px";
            document.getElementById("navBar").style.height = cHeight;
            document.getElementById("infoSection").style.height = cHeight;
        }

        window.onresize = function () { setHeight(); }
    </script>
    <script type="text/javascript">
        document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
            //WeixinJSBridge.call('hideToolbar');
        });
    </script>
</asp:Content>
