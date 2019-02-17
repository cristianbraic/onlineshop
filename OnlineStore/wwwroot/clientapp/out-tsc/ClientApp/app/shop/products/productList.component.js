import * as tslib_1 from "tslib";
import { Component } from "@angular/core";
import { DataService } from '../../shared/dataService';
var ProductList = /** @class */ (function () {
    function ProductList(data) {
        this.data = data;
        this.products = [];
    }
    ProductList.prototype.ngOnInit = function () {
        var _this = this;
        this.data.loadProducts()
            .subscribe(function (success) {
            if (success) {
                _this.products = _this.data.products;
                console.log(_this.products);
            }
        });
    };
    ProductList.prototype.addProduct = function (product) {
        this.data.addToOrder(product);
    };
    ProductList.prototype.onDelete = function (product) {
        var deletedProductIndex = this.products.findIndex(function (p) { return p.id === product.id; });
        this.products.splice(deletedProductIndex, 1);
        console.log(product, this.products);
    };
    ProductList = tslib_1.__decorate([
        Component({
            selector: "product-list",
            templateUrl: "productList.component.html",
            styleUrls: ["productList.component.css"]
        }),
        tslib_1.__metadata("design:paramtypes", [DataService])
    ], ProductList);
    return ProductList;
}());
export { ProductList };
//# sourceMappingURL=productList.component.js.map