import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductsRoutingModule } from './products-routing.module';
import { ProductsBrowserPageComponent } from './pages/products-browser-page/products-browser-page.component';
import {CoreModule} from "../../core/core.module";
import { ProductsPaginatedGridViewComponent } from './components/products-paginated-grid-view/products-paginated-grid-view.component';
import {SharedModule} from "../../shared/shared.module";
import {NgxPaginationModule} from "ngx-pagination";
import { ProductDetailsPageComponent } from './pages/product-details-page/product-details-page.component';
import {LightboxModule} from "ngx-lightbox";


@NgModule({
  declarations: [
    ProductsBrowserPageComponent,
    ProductsPaginatedGridViewComponent,
    ProductDetailsPageComponent
  ],
  imports: [
    CommonModule,
    ProductsRoutingModule,
    CoreModule,
    SharedModule,
    NgxPaginationModule,
    LightboxModule
  ]
})
export class ProductsModule { }
