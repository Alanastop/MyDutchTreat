import { Http, Response } from "@angular/http";
import { Observable } from "rxjs";
import { Product } from "./product";
import { Order, OrderItem } from "./order";
import { Injectable } from "@angular/core";
import 'rxjs/add/operator/map';


@Injectable()
export class DataService {

    constructor(private http: Http) {

    }

    public order: Order = new Order();

    public products: Product[] = [];

    public getProducts(): Observable<Product[]> {
        return this.http.get("http://localhost:50939/api/products")
            .map((result: Response) => this.products = result.json());            
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
}