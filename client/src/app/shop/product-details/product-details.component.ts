import { Component, OnInit } from '@angular/core';
import { ShopService } from '../shop.service';
import { Product } from 'src/app/shared/models/product';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { BreadcrumbService } from 'xng-breadcrumb';
import { BasketService } from 'src/app/basket/basket.service';
import { take } from 'rxjs';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss'],
})
export class ProductDetailsComponent implements OnInit {
  _product!: Product;
  quantity = 1;
  quantityInBasket = 0;

  constructor(
    private shopService: ShopService,
    private activatedRoute: ActivatedRoute,
    private bcService: BreadcrumbService,
    private basketService: BasketService
  ) {
    this._product = {
      name: '',
      description: '',
      id: 0,
      pictureUrl: '',
      price: 0,
      productBrand: '',
      productType: '',
      productBrandId: 0,
      productTypeId: 0,
    };

    this.bcService.set('@producDetails', ' ');
  }

  ngOnInit(): void {
    this.getProduct();
  }

  getProduct() {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if (id)
      this.shopService.getProduct(+id).subscribe({
        next: (response) => {
          this._product = response;
          this.bcService.set('@producDetails', response.name);
          this.basketService.basketSource$.pipe(take(1)).subscribe({
            next: (basket) => {
              const item = basket?.items.find((x) => x.id === +id);
              if (item) {
                this.quantity = item.quantity;
                this.quantityInBasket = item.quantity;
              }
            },
          });
        },
        error: (error) => console.log(error),
        complete: () => {
          console.log('Product load successfully');
        },
      });
  }

  incrementQuantity() {
    this.quantity++;
  }

  decrementQuantity() {
    this.quantity--;
  }

  updateBasket() {
    if (this._product) {
      if (this.quantity > this.quantityInBasket) {
        const itemToAdd = this.quantity - this.quantityInBasket;
        this.quantityInBasket += itemToAdd;
        this.basketService.addItemToBasket(this._product, itemToAdd);
      } else {
        const itemToRemove = this.quantityInBasket - this.quantity;
        this.quantityInBasket -= itemToRemove;
        this.basketService.removeItemFromBasket(this._product.id, itemToRemove);
      }
    }
  }

  get buttonText() {
    return this.quantityInBasket === 0 ? 'Add to basket' : 'Update basket';
  }
}
