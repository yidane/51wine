<%@ Page Language="C#" MasterPageFile="Restaurant.Master" AutoEventWireup="true" CodeBehind="diancai_shoppingCart.aspx.cs" Inherits="WeiXinPF.Web.weixin.restaurant.diancai_shoppingCart" %>

<asp:Content ID="h" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta name="viewport" content="width=device-width,height=device-height,inital-scale=1.0,maximum-scale=1.0,user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
    <link href="css/diancai.css" rel="stylesheet" type="text/css" />
    <script src="js/alert.js" type="text/javascript"></script>
    <script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
</asp:Content>

<asp:Content ID="c" ContentPlaceHolderID="content" runat="server">

    <div id="mcover" onclick="document.getElementById('mcover').style.display='';">

        <div class="textPopup">
            <h2>是否清空菜单？</h2>
            <div>
                <a class="two ok" id="ok" href="javascript:void(0)">确定</a>
                <a class="two" href="javascript:void(0)">取消</a>
            </div>
            <a class="x" onclick="document.getElementById('mcover').style.display='';">X</a>
        </div>
    </div>

    <div class="header">
        <span class="pCount"></span>
        <label><i>共计：</i><b id="totalPrice" class="duiqi">0</b><b class="duiqi">元</b></label>
    </div>

    <div class="biaodan">
        <h2>我的订单
            <button id="clearBtn" class="btn_add emptyIt" onclick="clearCache();">清空</button>
            <a href="index.aspx?shopid=<%=shopid %>&openid=<%=openid %>&wid=<%=wid %>">
                <button class="btn_add">继续选购</button></a>
        </h2>


        <section class="order">
            <div class="orderlist">
                <ul id="ullist">
                </ul>
            </div>
        </section>


    </div>

    <form name="infoForm" id="infoForm" method="post" action="">
        <input type="hidden" name="issubmit" value="0">
        <input type="hidden" name="goodsData" id="goodsData" value="">

        <input type="hidden" name="formhash" id="formhash" value="52ebc03e" />
        <div class="contact-info" id="contact_info" runat="server">

            <ul>
                <li class="title">联系信息</li>
                <li>
                    <table style="padding: 0; margin: 0; width: 100%;">
                        <tbody>
                            <tr>
                                <td width="80px">
                                    <label for="name" class="ui-input-text">联系人：</label></td>
                                <td>
                                    <div class="ui-input-text">
                                        <input type="text" id="name" name="name" value="<%=name %>" placeholder="" class="ui-input-text">
                                    </div>
                                </td>
                            </tr>
                            <tr id="nameinfo-layout" style="display: none;">
                                <td width="80px"></td>
                                <td colspan="2" id="nameinfo" class="cart-editalertinfo"></td>
                            </tr>
                            <tr>
                                <td width="80px">
                                    <label for="phone" class="ui-input-text">联系电话：</label></td>
                                <td>
                                    <div class="ui-input-text">
                                        <input type="tel" id="phone" name="phone" placeholder="联系人电话" value="<%=phone %>" class="ui-input-text">
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </li>
            </ul>
        </div>

        <div class="footReturn" style="margin-bottom: 70px;">
            <a id="showcard" class="submit" href="javascript:submitOrder();" runat="server">确定提交</a>
        </div>
    </form>

    <script src="js/shopCart.js" type="text/javascript"></script>
    <script src="js/BuyProducts.js" type="text/javascript"></script>
    <script>
        var cart = new OAK.Shop.Cart(<%=shopid %>);
        window.onload = function () {
            cart.getFromCache();
            showShopCart();
        }
        function clearCache() {
            cart.clear();
            showShopCart();
        }

        function gotoorder() {
            window.location.href = window.location.href;
            //window.location.href='http://www.apiwx.com/?ac=diancaionline-list&c=o99epjjmjWhMPNzoQbo9r6DAEYds&shopid=783&id=33473&g=1';
        }

        function showShopCart() {
            var categories = <%=string.IsNullOrEmpty(categories) ? "[]" : categories%>;
            var products = cart.getProductList();
            var orderlist = g("ullist");
            products && products.sort(cart.sortAsc);
            var liststr = "";
            var currentCategory = 0;
            for (var i in products) {
                if (currentCategory != products[i].categoryId) {
                    liststr += "<dt>" + categories[products[i].categoryId] + "</dt>";
                    currentCategory = products[i].categoryId;

                }
                liststr += "<li class=\"ccbg2\" id=\"li_" + products[i].id + "_" + products[i].specId + "\">" +
                    "<div class=\"orderdish\"><span class=\"\">" + products[i].name + "</span><p><span class=\"price\" id=\"v_0\">" + products[i].price + "</span><span class=\"price\">元</span></p></div>" +
                    "<div class=\"orderchange\">" +
                    "<a href=\"javascript:addProduct(" + products[i].id + "," + products[i].specId + ",\'" + products[i].name + "\'," + products[i].price + "," + products[i].categoryId + ",1" + ")\" class=\"increase\"><b class=\"ico_increase\">加一份</b></a>" +
                    "<span class=\"count\" id=\"num_" + products[i].id + "_" + products[i].specId + "\">" + products[i].number + "</span>" +
                    "<a href=\"javascript:reduceProduct(" + products[i].id + "," + products[i].specId + ",1)\" class=\"reduce\"><b class=\"ico_reduce\">减一份</b></a>" +
                    "</div>" +
                    "</li>";

            }
            liststr += "<li class=\"ccbg2\" id='notEnoughLi' style='display: none;'>必须要满1元才能下单哦</li>" +
                "<li class=\"ccbg2\" id='emptyLi' style='display: none;'>购物车为空哦，快去挑选吧！</li>";

            orderlist.innerHTML = liststr;
            showShopCartTotalProce();
            showShopCartProductsNumber();
            showShopCartProductsDetail();
        };
        cart.onAfterAdd = function (obj, num, conditions) {
            processProductsInShopCart(conditions.id, conditions.specId, num);
            showShopCartTotalProce();
            showShopCartProductsNumber();
            showShopCartProductsDetail();
            cart.saveToCache();
        };
        cart.onAfterUpdate = function (obj, num, conditions) {
            processProductsInShopCart(conditions.id, conditions.specId, num);
            showShopCartTotalProce();
            showShopCartProductsNumber();
            showShopCartProductsDetail();
            cart.saveToCache();
        };
        cart.onAfterDelete = function (obj, conditions) {
            processProductsInShopCart(conditions.id, conditions.specId, 0);
            showShopCartTotalProce();
            showShopCartProductsNumber();
            showShopCartProductsDetail();
            cart.saveToCache();
        };
    </script>
    <script>
        function afterPaySuccess(prepayid) {
            $.ajax({
                url: "PayService.asmx/AfterPaySuccess",
                type: "post",
                dataType: "json",
                async: false,
                data: { prepayid: prepayid },
                success: function (result) {
                    document.location.href = "diancai_OrderList.aspx?openid=<%=openid %>&type=pay";
                },
                error: function (error) {
                }
            });
            }

            function afterPayFail(prepayid) {
                alert("afterPayFail:"+prepayid);
            }
        
            function afterPayComplete(prepayid) {
                alert("afterPayComplete:"+prepayid);
            }
        
            function afterPayCancel(prepayid) {
                alert("afterPayCancel:"+prepayid);
            }

            function submitOrder() {
                vailReSubmit();
           
                if (valiForm()) {
                    return;
                }
            
                var goodsData = '';
                var goodsDescription = '';
            
                var goodsList = cart.getProductList();
                goodsList && goodsList.sort(cart.sortAsc);
                for (var i in goodsList) {
                    goodsData += goodsList[i].id + ',' + goodsList[i].number + ',' + goodsList[i].price + ';';
                    goodsDescription += goodsList[i].name + ' X' + goodsList[i].number + '/r/n';
                }
                //菜品ID，数量，单价
                g('goodsData').value = goodsData;
                var submitData = {            
                    goodsData:g('goodsData').value,
                    name:g('name').value,
                    phone:g('phone').value,
                    address:'',
                    oderRemark:'',  
                    deskNumber:'',  
                    myact: "addcaidan"
                };
           
                $.post('diancai_login.ashx?openid=<%=openid%>&shopid=<%=shopid%>&wid=<%=wid%>', submitData,
                function (result) {
                    if (result.IsSuccess) {
                        document.location.href = result.Data;
                    } else {
                        alert(data.content); }
                },
                "json");

                document.infoForm.issubmit.value = 1;//不能再提交
            }
        
            function valiForm() {
                var phonePattern = /^((\(\d{3}\))|(\d{3}\-))?(\(0\d{2,3}\)|0\d{2,3}-)?[1-9]\d{6,7}$/;
                var mobilePattern = /^1\d{10}$/;
                var flag = false;
                if (g("name").value.length < 1) {
                    //  g("name").className = "textinput alertinput";
                    alert("联系人不能为空");
                    return true;
                }
                if (!(phonePattern.test(g("phone").value) || mobilePattern.test(g("phone").value))) {
                    alert("亲，您的联系电话格式有误！");
                    return true;
                }

                return flag;
            }

            function vailReSubmit() {
                if (document.infoForm.issubmit.value == 0) {
                    return true;
                }
                else {
                    alert(' 按一次就够了，请勿重复提交！请耐心等待！谢谢合作！');
                    return false;
                }
            }       
    </script>
    <script type="text/javascript">
        document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
            WeixinJSBridge.call('hideToolbar');
        });
    </script>

</asp:Content>
