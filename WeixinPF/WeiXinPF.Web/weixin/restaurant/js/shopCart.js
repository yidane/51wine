
var OAK = OAK || {};
OAK.Dom = {};
OAK.Shop = {};
OAK.Util = {};
OAK.Dom.setAttributes = function (el, prop) {
    for (var i in prop) {
        el.setAttribute(i, prop[i]);
    }
    return el;
}
OAK.Util.setProps = function (s, prop) {
    for (var i in prop) {
        s[i] = prop[i];
    }
    return s;
}
OAK.Util.isEqualInConditions = function (o, conditions) {
    for (var i in conditions) {
        if (o[i] != conditions[i]) {
            return false;
        }
    }
    return true;
}
OAK.Util.copy = function (o) {
    var res = new Object();
    for (var i in o) {
        res[i] = o[i];
    }
    return res;
}
OAK.Util.setParam = function (name, value) {
    localStorage.setItem(name, value);
}
OAK.Util.getParam = function (name) {
    return localStorage.getItem(name);
}
OAK.Shop.Product = function (prop) {
    var prod = {
        id: 0,
        name: "",
        specId: 0,
        price: 0.00,
        number: 0,
        categoryId: 0
    };
    return new OAK.Util.setProps(prod, prop);
}
OAK.Shop.Cart = function (shopId) {
    if (typeof shopId !== "undefined" && shopId !== 0 && typeof OAK.Shop.Cart.single_instance === "undefined") {
        this._totalNumber = 0;
        this._totalAmount = 0.00;
        this._products = [];
        this._shoppingCartId = "ShoppingdiancaiCart" + shopId;
        this.onBeforeAdd = null;
        this.onAfterAdd = null;
        this.onBeforeUpdate = null;
        this.onAfterUpdate = null;
        this.onBeforeDelete = null;
        this.onAfterDelete = null;
        OAK.Shop.Cart.single_instance = this;
    }
    return OAK.Shop.Cart.single_instance;
}
OAK.Shop.Cart.prototype = {    
    saveToCache: function () {
        OAK.Util.setParam(this._shoppingCartId, JSON.stringify(this));
    },
    getFromCache: function () {
        var shoppingCart = OAK.Util.getParam(this._shoppingCartId);
        if (shoppingCart != null && shoppingCart != "") {
            OAK.Util.setProps(this, JSON.parse(shoppingCart));
        }
    },
    clear: function () {
        OAK.Util.setParam(this._shoppingCartId, null);
        this._totalNumber = 0;
        this._totalAmount = 0.00;
        this._products = [];
    },
    addProduct: function (p, conditions) {
        this.onBeforeAdd !== null && this.onBeforeAdd(this, p, conditions);
        var _conditions = conditions || { id: p.id, specId: p.specId, ref: true };
        var alreadyExistProduct = this.getProduct(_conditions);

        if (alreadyExistProduct !== null) {
            alreadyExistProduct.number += p.number;
        }
        else
            this._products.push(p);
        this._totalNumber += p.number;
        this._totalAmount += p.number * p.price;
        this.onAfterAdd !== null && this.onAfterAdd(this, alreadyExistProduct ? alreadyExistProduct.number : p.number, _conditions);
    },
    getQuantity: function () {
        return { totalNumber: this._totalNumber, totalAmount: this._totalAmount };
    },
    updateNumber: function (num, conditions) {
        this.onBeforeUpdate !== null && this.onBeforeUpdate(this, num, conditions);
        conditions.ref = true;
        var alreadyExistProduct = this.getProduct(conditions);
        if (alreadyExistProduct !== null) {
            this._totalNumber += (parseInt(num) - parseInt(alreadyExistProduct.number));
            this._totalAmount += ((parseInt(num) * parseFloat(alreadyExistProduct.price)) - parseInt(alreadyExistProduct.number) * parseFloat(alreadyExistProduct.price));
            alreadyExistProduct.number = num;
        }
        this.onAfterUpdate !== null && this.onAfterUpdate(this, alreadyExistProduct ? alreadyExistProduct.number : 0, conditions);
    },
    //获取购物车中的所有商品
    getProductList: function () {
        return this._products;
    },
    getProduct: function (conditions) {
        var ref = conditions.ref;
        delete conditions.ref;
        for (var i in this._products) {
            if (OAK.Util.isEqualInConditions(this._products[i], conditions))
                return ref ? this._products[i] : OAK.Util.copy(this._products[i]);
        }
        return null;
    },
    getProductNumber: function (conditions) {
        for (var i in this._products) {
            if (OAK.Util.isEqualInConditions(this._products[i], conditions))
                return this._products[i].number;
        }
        return null;
    },
    existProduct: function (conditions) {
        for (var i in this._products) {
            if (OAK.Util.isEqualInConditions(this._products[i], conditions))
                return true;
        }
        return false;
    },
    deleteProduct: function (conditions) {
        this.onBeforeDelete !== null && this.onBeforeDelete(this, conditions);
        for (var i in this._products) {
            if (OAK.Util.isEqualInConditions(this._products[i], conditions)) {
                this._totalNumber -= parseInt(this._products[i].number);
                this._totalAmount -= parseInt(this._products[i].number) * parseFloat(this._products[i].price);
                this._products.splice(i, 1);
                break;
            }
        }
        this.onAfterDelete !== null && this.onAfterDelete(this, conditions);
    },
    sortAsc: function (a, b) {
        if (a.categoryId > b.categoryId) {
            return 1;
        } else if (a.categoryId == b.categoryId) {
            if (a.id > b.id)
                return 1;
            else if (a.id == b.id)
                return a.specId > b.specId ? 1 : -1;
            return -1;
        }
        return -1;
    }
}