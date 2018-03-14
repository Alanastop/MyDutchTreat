import { Component, OnInit } from "@angular/core";
import { DataService } from "../shared/dataService";
import { Router } from "@angular/router";
import { Order } from "../shared/order";

@Component({
    selector: "order-list",
    templateUrl: "orderList.component.html",
    styleUrls: ["orderList.component.css"]
})
export class OrderList implements OnInit{
    public orders: Order[];

    constructor(public data: DataService, public router: Router) {
    }

    ngOnInit(): void {
        this.data.getOrders()
            .subscribe(() => this.orders = this.data.orders);
        debugger;
    }
}