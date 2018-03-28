"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var _ = require("lodash");
var Order = /** @class */ (function () {
    function Order() {
        this.orderDate = new Date();
        this.items = new Array();
        this.user = new User();
    }
    Object.defineProperty(Order.prototype, "subtotal", {
        get: function () {
            return _.sum(_.map(this.items, function (i) { return i.unitPrice * i.quantity; }));
        },
        enumerable: true,
        configurable: true
    });
    ;
    return Order;
}());
exports.Order = Order;
var OrderItem = /** @class */ (function () {
    function OrderItem() {
    }
    return OrderItem;
}());
exports.OrderItem = OrderItem;
var User = /** @class */ (function () {
    function User() {
    }
    return User;
}());
exports.User = User;
//# sourceMappingURL=order.js.map