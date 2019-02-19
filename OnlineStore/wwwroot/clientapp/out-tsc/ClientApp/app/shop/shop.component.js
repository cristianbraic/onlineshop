import * as tslib_1 from "tslib";
import { Component, ViewChild, ElementRef } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { DataService } from '../shared/dataService';
var Shop = /** @class */ (function () {
    function Shop(data) {
        this.data = data;
        this.fileToUpload = null;
        this.formAddProduct = new FormGroup({
            image: new FormControl('', Validators.required),
            name: new FormControl('', Validators.required),
            price: new FormControl('', Validators.required),
            description: new FormControl('', Validators.required)
        });
    }
    Shop.prototype.onFileChange = function (event) {
        var files = event.target.files;
        var file = this.formAddProduct.value.image;
        console.log(event);
        this.labelImport.nativeElement.innerText = Array.from(files)
            .map(function (f) { return f.name; })
            .join(', ');
        this.fileToUpload = files.item(0);
    };
    Shop.prototype.onSave = function () {
        var newProduct = {
            title: this.formAddProduct.value.name,
            price: this.formAddProduct.value.price,
            artDescription: this.formAddProduct.value.description,
            artId: this.labelImport.nativeElement.innerText.split('.')[0]
        };
        console.log(newProduct);
        this.data.addNewProduct(newProduct)
            .subscribe(function (success) {
            console.log(success);
        });
    };
    tslib_1.__decorate([
        ViewChild('labelImport'),
        tslib_1.__metadata("design:type", ElementRef)
    ], Shop.prototype, "labelImport", void 0);
    Shop = tslib_1.__decorate([
        Component({
            selector: "the-shop",
            templateUrl: "shop.component.html",
            styleUrls: ["shop.component.css"]
        }),
        tslib_1.__metadata("design:paramtypes", [DataService])
    ], Shop);
    return Shop;
}());
export { Shop };
//# sourceMappingURL=shop.component.js.map