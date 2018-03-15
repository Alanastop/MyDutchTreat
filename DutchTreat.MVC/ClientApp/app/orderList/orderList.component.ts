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

    errorMessage: string = "";

    ngOnInit(): void {
        this.data.getOrders()
            .subscribe(() => this.orders = this.data.orders);      
    }

    deleteOrder(order: Order) {
        this.data.deleteOrder(order)
            .subscribe(success => {
                if (success) {
                    this.router.navigate(["/"]);
                }
            }, err => this.errorMessage = "Failed to save order");
    }
}