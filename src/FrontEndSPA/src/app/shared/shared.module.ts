import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {ProductsGridViewComponent} from "./components/products-grid-view/products-grid-view.component";
import {ProductCardComponent} from "./components/product-card/product-card.component";
import {
  ProductsTabPanelCarouselComponent
} from "./components/products-tab-panel-carousel/products-tab-panel-carousel.component";
import {CarouselModule} from "ngx-owl-carousel-o";



@NgModule({
  declarations: [
    ProductsGridViewComponent,
    ProductCardComponent,
    ProductsTabPanelCarouselComponent
  ],
  imports: [
    CommonModule,
    CarouselModule,

  ],
  exports: [
    ProductsGridViewComponent,
    ProductCardComponent,
    ProductsTabPanelCarouselComponent

  ]
})
export class SharedModule { }
