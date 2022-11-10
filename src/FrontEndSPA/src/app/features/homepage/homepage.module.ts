import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomepageRoutingModule } from './homepage-routing.module';
import { HomepageComponent } from './pages/homepage/homepage.component';
import {CoreModule} from "../../core/core.module";
import {CarouselModule} from "ngx-owl-carousel-o";
import { GuaranteesComponent } from './components/guarantees/guarantees.component';
import { CategoriesRowComponent } from './components/categories-row/categories-row.component';
import { BrandsRowComponent } from './components/brands-row/brands-row.component';
import { BannerSmallComponent } from './components/banner-small/banner-small.component';
import { DealsCarouselComponent } from './components/deals-carousel/deals-carousel.component';
import {CarouselModule as CoreUICarouselModule} from "@coreui/angular";
import {SharedModule} from "../../shared/shared.module";


@NgModule({
    declarations: [
        HomepageComponent,
        GuaranteesComponent,
        CategoriesRowComponent,
        BrandsRowComponent,
        BannerSmallComponent,
        DealsCarouselComponent
    ],
    imports: [
        CommonModule,
        CoreModule,
        HomepageRoutingModule,
        CoreUICarouselModule,
        CarouselModule,
        SharedModule
    ]
})
export class HomepageModule { }
