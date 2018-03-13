import { Http, Response } from "@angular/http";
import { Observable } from "rxjs";
import { Product } from "./product";
import { Injectable } from "@angular/core";
import 'rxjs/add/operator/map';


@Injectable()
export class DataService {

    constructor(private http: Http) {

    }

    public products: Product[] = [];

    public getProducts(): Observable<Product[]> {
        return this.http.get("http://localhost:50939/api/products")
            .map((result: Response) => this.products = result.json());            
    }
}