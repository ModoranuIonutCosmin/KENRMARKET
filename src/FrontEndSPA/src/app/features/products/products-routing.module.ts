import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {ProductsBrowserPageComponent} from "./pages/products-browser-page/products-browser-page.component";
import {ProductDetailsPageComponent} from "./pages/product-details-page/product-details-page.component";

const routes: Routes = [
  {
    path: '',
    component: ProductsBrowserPageComponent,
  },
  {
    path: 'product',
    component: ProductDetailsPageComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProductsRoutingModule { }
