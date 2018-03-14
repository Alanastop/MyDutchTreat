import * as _ from "lodash";

export class Order {
        orderId: number;
        orderDate: Date = new Date();
        orderNumber: string;
        items: Array <OrderItem> = new Array<OrderItem>();
        user: User = new User();

        get subtotal(): number {
            return _.sum(_.map(this.items, i => i.unitPrice * i.quantity));
        };
    }

export class OrderItem {
    id: number;
    quantity: number;
    unitPrice: number;
    productId: number;
    productCategory: string;
    productSize: string;
    productTitle: string;
    productArtist: string;
    productArtId: string;
}


export class User {
    firstName: string;
    lastName: string;
    id: string;
    userName: string;
    normalizedUserName: string;
    email: string;
    normalizedEmail: string;
    emailConfirmed: boolean;
    passwordHash: string;
    securityStamp: string;
    concurrencyStamp: string;
    phoneNumber?: any;
    phoneNumberConfirmed: boolean;
    twoFactorEnabled: boolean;
    lockoutEnd?: any;
    lockoutEnabled: boolean;
    accessFailedCount: number;
}


