import { Component, OnInit } from "@angular/core";
import { DataService } from '../../shared/dataService';
import { Product } from "../../shared/product";

@Component({
    selector: "product-list",
    templateUrl: "productList.component.html",
    styleUrls: [ "productList.component.css"]
})
export class ProductList implements OnInit{
    
    constructor(private data: DataService) {
    }
    public products: Product[] = [];

    ngOnInit(): void {
        this.data.loadProducts()
            .subscribe(success => {
                if (success) {
                    this.products = this.data.products;
                    console.log(this.products)
                }
            });
    }

    addProduct(product: Product) {
        this.data.addToOrder(product);
    }

    onDelete(product: Product) {
        let deletedProductIndex = this.products.findIndex(p => p.id === product.id);
        this.products.splice(deletedProductIndex, 1);
        console.log(product, this.products);
    }
}
