import { Component, ViewChild, ElementRef } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { DataService } from '../shared/dataService';

@Component({
    selector: "the-shop",
    templateUrl: "shop.component.html",
    styleUrls: ["shop.component.css"]
})

export class Shop {

    @ViewChild('labelImport')
    labelImport: ElementRef;

    formAddProduct: FormGroup;
    fileToUpload: File = null;

    constructor(private data: DataService) {
        this.formAddProduct = new FormGroup({
            image: new FormControl('', Validators.required),
            name: new FormControl('', Validators.required),
            price: new FormControl('', Validators.required),
            description: new FormControl('', Validators.required)
        });
    }

    onFileChange(event) {
        let files: FileList = event.target.files;
        let file = this.formAddProduct.value.image;
        console.log(event);

        this.labelImport.nativeElement.innerText = Array.from(files)
            .map(f => f.name)
            .join(', ');
        this.fileToUpload = files.item(0);
    }

    onSave() {
        let newProduct = {
            title: this.formAddProduct.value.name,
            price: this.formAddProduct.value.price,
            artDescription: this.formAddProduct.value.description,
            artId: this.labelImport.nativeElement.innerText.split('.')[0]
        };
        console.log(newProduct)

        this.data.addNewProduct(newProduct)
            .subscribe(success => {
                console.log(success)
            });
    }
}