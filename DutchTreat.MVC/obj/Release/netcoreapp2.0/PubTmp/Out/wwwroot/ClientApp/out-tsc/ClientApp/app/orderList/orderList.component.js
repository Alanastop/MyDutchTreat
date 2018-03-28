"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var dataService_1 = require("../shared/dataService");
var router_1 = require("@angular/router");
var OrderList = /** @class */ (function () {
    function OrderList(data, router) {
        this.data = data;
        this.router = router;
        this.errorMessage = "";
    }
    OrderList.prototype.ngOnInit = function () {
        var _this = this;
        this.data.getOrders()
            .subscribe(function () { return _this.orders = _this.data.orders; });
    };
    OrderList.prototype.deleteOrder = function (order) {
        var _this = this;
        this.data.deleteOrder(order)
            .subscribe(function (success) {
            if (success) {
                _this.router.navigate(["/"]);
            }
        }, function (err) { return _this.errorMessage = "Failed to save order"; });
    };
    OrderList = __decorate([
        core_1.Component({
            selector: "order-list",
            templateUrl: "orderList.component.html",
            styleUrls: ["orderList.component.css"]
        }),
        __metadata("design:paramtypes", [dataService_1.DataService, router_1.Router])
    ], OrderList);
    return OrderList;
}());
exports.OrderList = OrderList;
//# sourceMappingURL=orderList.component.js.map