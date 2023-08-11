import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { enviornment } from 'src/enviornments/enviornment';
import { DeliveryMethod } from '../shared/models/deliveryMethod';
import { map } from 'rxjs';
import { Order, OrderToCreate } from '../shared/models/order';

@Injectable({
  providedIn: 'root',
})
export class CheckoutService {
  baseUrl = enviornment.apiUrl;

  constructor(private http: HttpClient) {}

  createOrder(order: OrderToCreate) {
    return this.http.post<Order>(this.baseUrl + 'orders', order);
  }

  getDeliverMethod() {
    return this.http
      .get<DeliveryMethod[]>(this.baseUrl + 'Orders/deliveryMethods')
      .pipe(
        map((dm) => {
          return dm.sort((a, b) => b.price - a.price);
        })
      );
  }
}
