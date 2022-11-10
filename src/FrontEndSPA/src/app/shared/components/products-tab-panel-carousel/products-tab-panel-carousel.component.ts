import {Component, Input, OnInit} from '@angular/core';
import {ProductsTabPanelModel} from "../../../features/homepage/models/products-tab-panel-model";
import {OwlOptions} from "ngx-owl-carousel-o";

@Component({
  selector: 'app-products-tab-panel-carousel',
  templateUrl: './products-tab-panel-carousel.component.html',
  styleUrls: ['./products-tab-panel-carousel.component.scss']
})
export class ProductsTabPanelCarouselComponent implements OnInit {

  @Input() tabPanelDataSource: ProductsTabPanelModel;

  customOptions: OwlOptions = {
    nav: true,
    dots: false,
    margin: 20,
    loop: false,
    responsive: {
      0: {
        items:1
      },
      480: {
        items:2
      },
      768: {
        items:3
      },
      992: {
        items:4
      },
      1280: {
        items:5
      }
    }
  }

  constructor() {

    this.tabPanelDataSource = {
      title: "",
      selectedPanelIndex: 0,
      productTabs: [],
      hasBanner: false
    }
  }

  ngOnInit(): void {
  }

}
