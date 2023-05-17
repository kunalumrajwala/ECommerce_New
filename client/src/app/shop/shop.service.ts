import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Pagination } from '../shared/models/pagination';
import { Product } from '../shared/models/product';
import { Observable } from 'rxjs';
import { Brand } from '../shared/models/brand';
import { Type } from '../shared/models/types';
import { shopParams } from '../shared/models/shopParams';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api/';
  constructor(private httpClient: HttpClient) {}

  getProducts(shopParams: shopParams): Observable<Pagination<Product[]>> {
    var params = new HttpParams();
    if (shopParams.brandId > 0)
      params = params.append('brandId', shopParams.brandId);
    if (shopParams.typeId > 0)
      params = params.append('typeId', shopParams.typeId);
    params = params.append('sort', shopParams.sort);
    params = params.append('pageIndex', shopParams.pageNumber);
    params = params.append('pageSize', shopParams.pageSize);
    if (shopParams.search) params = params.append('Search', shopParams.search);

    return this.httpClient.get<Pagination<Product[]>>(
      this.baseUrl + 'product',
      { params }
    );
  }

  getBrands(): Observable<Brand[]> {
    return this.httpClient.get<Brand[]>(this.baseUrl + 'Product/brands');
  }

  getType(): Observable<Type[]> {
    return this.httpClient.get<Type[]>(this.baseUrl + 'Product/types');
  }
}
