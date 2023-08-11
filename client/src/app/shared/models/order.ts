import { Address } from './user';

export interface OrderToCreate {
  basketId: string;
  deliveryMethod: number;
  shipToAddress: Address;
}

export interface Order {
  productId: number;
  productName: string;
  pictureUrl: string;
  price: number;
  quantity: number;
}

export interface Root {
  id: number;
  buyerEmail: string;
  orderDate: string;
  shipToAddress: Address;
  deliveryMethod: string;
  shippingPrice: number;
  orderItems: Order[];
  subtotal: number;
  total: number;
  status: string;
}
