import * as tslib_1 from "tslib";
import { Component } from "@angular/core";
import { DataService } from "../shared/dataService";
import { Router } from "@angular/router";
var Cart = /** @class */ (function () {
    function Cart(data, router) {
        this.data = data;
        this.router = router;
    }
    Cart.prototype.onCheckout = function () {
        if (this.data.loginRequired) {
            //force login
            this.router.navigate(["login"]);
        }
        else {
            //go to checkout
            this.router.navigate(["checkout"]);
        }
    };
    var _a;
    Cart = tslib_1.__decorate([
        Component({
            selector: "the-cart",
            templateUrl: "cart.component.html",
            styleUrls: []
        }),
        tslib_1.__metadata("design:paramtypes", [DataService, typeof (_a = typeof Router !== "undefined" && Router) === "function" ? _a : Object])
    ], Cart);
    return Cart;
}());
export { Cart };
//# sourceMappingURL=cart.component.js.map