import { Http, Response, Headers } from "@angular/http";
import { Observable } from "rxjs";
import { Product } from "./product";
import { Order, OrderItem } from "./order";
import { Injectable } from "@angular/core";
import 'rxjs/add/operator/map';



@Injectable()
export class DataService {

    constructor(private http: Http) {

    }

    private token: string = "";
    private tokenExpiration: Date;
    public credentials;
    public order: Order = new Order();

    public products: Product[] = [];
    public orders: Order[] = [];

    public getProducts(): Observable<Product[]> {
        return this.http.get("http://localhost:50939/api/products")
            .map((result: Response) => this.products = result.json());            
    }

    public get loginRequired(): boolean {
        return this.token.length == 0 || this.tokenExpiration > new Date();
    }

    public login(creds) {
        return this.http.post("/account/createtoken", creds)
            .map(response => {
                let tokenInfo = response.json();
                this.token = tokenInfo.token;
                this.tokenExpiration = tokenInfo.expiration;
                return true;
            });
    }

    public checkout() {       
        if (!this.order.orderNumber) {
            this.order.orderNumber = this.order.orderDate.getFullYear().toString() + this.order.orderDate.getTime().toString();
        }

        return this.http.post("http://localhost:50939/api/orders", this.order, {
            headers: new Headers({ "Authorization": "Bearer " + this.token })
        })
            .map(response => {
                this.order = new Order();               
                return true;
            });
    }

    public getOrders(): Observable<Order[]> {
        if (this.token) {
            return this.http.get("http://localhost:50939/api/orders", {
                headers: new Headers({ "Authorization": "Bearer " + this.token })
            })
                .map((result: Response) => this.orders = result.json());
        }
    }

    public AddToOrder(product: Product) {

        let item: OrderItem = this.order.items.find(item => item.productId == product.id);

        if (item) {

            item.quantity++;

        } else {
            item = new OrderItem();
            item.productId = product.id;
            item.productArtist = product.artist;
            item.productCategory = product.category;
            item.productArtId = product.artId;
            item.productTitle = product.title;
            item.productSize = product.size;
            item.unitPrice = product.price;
            item.quantity = 1;

            this.order.items.push(item);
        }
    }

    public deleteOrder(order: Order) {
        if (this.token && this.token !== "") {
            return this.http.post("http://localhost:50939/api/orders/delete", order, {
                headers: new Headers({ "Authorization": "Bearer " + this.token })
            })
                .map(succes => succes.status);
        }         
    }
}