import { Component, Input, OnInit } from '@angular/core';
import { CheckoutService } from '../checkout.service';
import { FormGroup } from '@angular/forms';
import { DeliveryMethod } from 'src/app/shared/models/deliveryMethod';
import { BasketService } from 'src/app/basket/basket.service';

@Component({
  selector: 'app-checkout-delivery',
  templateUrl: './checkout-delivery.component.html',
  styleUrls: ['./checkout-delivery.component.scss'],
})
export class CheckoutDeliveryComponent implements OnInit {
  @Input() checkOutForm?: FormGroup;
  deliveryMethod: DeliveryMethod[] = [];

  constructor(
    private checkOutService: CheckoutService,
    private basketService: BasketService
  ) {}

  ngOnInit(): void {
    this.getDeliveryMethod();
  }

  getDeliveryMethod() {
    this.checkOutService.getDeliverMethod().subscribe({
      next: (dm) => (this.deliveryMethod = dm),
    });
  }

  setShippingPrice(deliveryMethod: DeliveryMethod) {
    this.basketService.setShippingPrice(deliveryMethod);
  }
}
