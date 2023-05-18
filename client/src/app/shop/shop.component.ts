import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ShopService } from './shop.service';
import { Product } from '../shared/models/product';
import { Brand } from '../shared/models/brand';
import { Type } from '../shared/models/types';
import { shopParams } from '../shared/models/shopParams';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss'],
})
export class ShopComponent implements OnInit {
  @ViewChild('search') search?: ElementRef;
  products: Product[] = [];
  brands: Brand[] = [];
  types: Type[] = [];

  shopParams = new shopParams();
  totalCount = 0;

  sortOptions = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price: Low to High', value: 'priceAsc' },
    { name: 'Price: High to Low', value: 'priceDesc' },
  ];

  constructor(private shopService: ShopService) {}

  ngOnInit(): void {
    this.getallProducts();
    this.getAllBrand();
    this.getAllTypes();
  }

  getallProducts() {
    this.shopService.getProducts(this.shopParams).subscribe({
      next: (response) => {
        this.products = response.data;
        this.shopParams.pageNumber = response.pageIndex;
        this.shopParams.pageSize = response.pageSize;
        this.totalCount = response.count;
      },
      error: (error) => console.log(error),
      complete: () => {
        console.log('Product Data loaded');
      },
    });
  }

  getAllBrand() {
    this.shopService.getBrands().subscribe({
      next: (response) => (this.brands = [{ id: 0, name: 'All' }, ...response]),
      error: (error) => console.log(error),
    });
  }

  getAllTypes() {
    this.shopService.getType().subscribe({
      next: (response) => (this.types = [{ id: 0, name: 'All' }, ...response]),
      error: (error) => console.log(error),
    });
  }

  onBrandIdSelected(brandId: number) {
    this.shopParams.brandId = brandId;
    this.shopParams.pageNumber = 1;
    this.getallProducts();
  }

  onTypeIdSelected(typeId: number) {
    this.shopParams.typeId = typeId;
    this.shopParams.pageNumber = 1;
    this.getallProducts();
  }

  onSortSelected(event: any) {
    this.shopParams.sort = event.target.value;
    this.shopParams.pageNumber = 1;
    this.getallProducts();
  }

  onPageChange(event: any) {
    if (this.shopParams.pageNumber != event) this.shopParams.pageNumber = event;
    this.getallProducts();
  }

  onSearch() {
    this.shopParams.search = this.search?.nativeElement.value;
    this.getallProducts();
  }

  onReset() {
    if (this.search) this.search.nativeElement.value = '';
    this.shopParams = new shopParams();
    this.getallProducts();
  }
}
