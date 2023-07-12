import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { BasketComponent } from './basket.component';

const router: Routes = [
  {
    path: '',
    component: BasketComponent,
  },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(router), CommonModule],
  exports: [RouterModule],
})
export class BasketRoutingModule {}
