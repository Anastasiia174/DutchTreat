import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { map } from "rxjs/operators";

@Injectable()
export class DataService {

    constructor(private http: HttpClient) {

    }
    public products = []

    loadProducts() {
        return this.http.get("/api/products")
            .pipe(
                map((data: any) => {
                    this.products = data;
                    return true;
                }));
    }
}