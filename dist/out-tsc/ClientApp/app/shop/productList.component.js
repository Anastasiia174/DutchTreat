import { __decorate } from "tslib";
import { Component } from "@angular/core";
let ProductList = class ProductList {
    constructor() {
        this.products = [{
                title: "First Product",
                price: 19.99
            },
            {
                title: "Second Product",
                price: 9.99
            },
            {
                title: "Third Product",
                price: 14.99
            },];
    }
};
ProductList = __decorate([
    Component({
        selector: "product-list",
        templateUrl: "productList.component.html",
        styles: []
    })
], ProductList);
export { ProductList };
//# sourceMappingURL=productList.component.js.map