import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import 'rxjs/add/operator/map';

@Injectable()
export class DataService {

    constructor(private http: HttpClient) {

    }

    public products = [];

    getProducts() {
        return this.http.get("http://localhost:50939/api/products")
            .map((data: any[]) => {
                this.products = data;
                return true;
            });
    }
}