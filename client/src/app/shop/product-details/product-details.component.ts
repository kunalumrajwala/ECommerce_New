import { Component, OnInit } from '@angular/core';
import { ShopService } from '../shop.service';
import { Product } from 'src/app/shared/models/product';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss'],
})
export class ProductDetailsComponent implements OnInit {
  _product!: Product;

  constructor(
    private shopService: ShopService,
    private activatedRoute: ActivatedRoute,
    private bcService: BreadcrumbService
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
        },
        error: (error) => console.log(error),
        complete: () => {
          console.log('Product load successfully');
        },
      });
  }
}
