import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpModule } from "@angular/http";


import { AppComponent } from './app.component';
import { ProductList } from './shop/productList.component';
import { Cart } from "./shop/cart.component";
import { Shop } from "./shop/shop.component";
import { Checkout } from "./checkout/checkout.component";
import { Login } from "./login/login.component";
import { OrderList } from "./orderList/orderList.component";

import { DataService } from "./shared/dataService";

import { RouterModule } from "@angular/router";
import { FormsModule } from "@angular/forms";

let routes = [
    { path: "", component: Shop },
    { path: "checkout", component: Checkout },
    { path: "login", component: Login },
    { path: "orderHistory", component: OrderList }
];

@NgModule({
  declarations: [
      AppComponent,
      ProductList,
      Cart,
      Shop,
      Checkout,
      Login,
      OrderList
  ],
  imports: [
      BrowserModule,
      HttpModule,
      FormsModule,
      RouterModule.forRoot(routes, {
          useHash: true,
          enableTracing: true //for debugging of the routes
      })
  ],
  providers: [
    DataService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
