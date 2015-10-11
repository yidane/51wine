function g(id) {
    return document.getElementById(id);
}

function addProduct(productId, specId, name, price, categoryId, addnum) {
    cart.addProduct(OAK.Shop.Product({ id: productId, specId: specId, number: addnum, price: price, name: name, categoryId: categoryId }));
}

function reduceProduct(productId, specId, num) {
    var oldnum = cart.getProductNumber({ id: productId, specId: specId });
    if (oldnum !== null) {
        if (oldnum - num > 0) {
            cart.updateNumber(oldnum - num, { id: productId, specId: specId });
        } else {
            cart.deleteProduct({ id: productId, specId: specId });
        }
    }
}

function processProductsInShopCart (productId, specId, num) {
    if (num > 0) {
        g("num_" + productId + "_" + specId).innerHTML = parseInt(num);
    } else {
        var curNode = g("li_" + productId + "_" + specId);
        var nextNode = getNextElement(curNode);
        if (!nextNode || nextNode.nodeName != 'LI' || nextNode.id == 'notEnoughLi' || nextNode.id == 'emptyLi') {
            var previousNode = getPreviousElement(curNode);
            if (previousNode && previousNode.nodeName == 'DT') {
                previousNode.parentNode.removeChild(previousNode);
            }
        }
        curNode.parentNode.removeChild(curNode);
    }
}

function showShopCartTotalProce() {
    var quant = cart.getQuantity();

    if (typeof quant.totalNumber === "undefined") {
        g("totalPrice").innerHTML = "0";
    } else {
        g("totalPrice").innerHTML = quant.totalAmount.toFixed(2);
    }
}

function showShopCartProductsNumber () {
    var quant = cart.getQuantity();

    if (typeof quant.totalNumber === "undefined") {
        g("cartN2").innerHTML = "0";
    } else {
        g("cartN2").innerHTML = "" + quant.totalNumber;
    }
};

function showProductNumberWithBought(productId, specId, num) {

    if (num > 0) {
        g("num_" + productId + "_" + specId).className = "count";
        g("del_" + productId + "_" + specId).style.display = "";
    } else {
        g("num_" + productId + "_" + specId).className = "count_zero";
        g("del_" + productId + "_" + specId).style.display = "none";
    }
    g("num_" + productId + "_" + specId).innerHTML = parseInt(num);
}

function showShopCartProductsDetail() {
    var quant = cart.getQuantity();
    
    if (quant.totalAmount > 0) {
        g('infoForm').style.display = "";
        g('notEnoughLi').style.display = "none";
        g('emptyLi').style.display = "none";
    } else {
        g('infoForm').style.display = "none";
        g('emptyLi').style.display = "";
        g('notEnoughLi').style.display = "none";
    }
}

function getNextElement(node) {
    if (node.nextSibling.nodeType == 1) {    //判断下一个节点类型为1则是"元素"节点
        return node.nextSibling;
    }
    if (node.nextSibling.nodeType == 3) {      //判断下一个节点类型为3则是"文本"节点  ，回调自身函数
        return getNextElement(node.nextSibling);
    }
    return null;
}
function getPreviousElement(node) {
    if (node.previousSibling.nodeType == 1) {    //判断下一个节点类型为1则是"元素"节点
        return node.previousSibling;
    }
    if (node.previousSibling.nodeType == 3) {      //判断下一个节点类型为3则是"文本"节点  ，回调自身函数
        return getPreviousElement(node.previousSibling);
    }
    return null;
}