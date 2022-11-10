import {Component, Input, OnInit} from '@angular/core';
import {OwlOptions} from "ngx-owl-carousel-o";
import {BrandsModel} from "../../models/brands-model";

@Component({
  selector: 'app-brands-row',
  templateUrl: './brands-row.component.html',
  styleUrls: ['./brands-row.component.scss']
})
export class BrandsRowComponent implements OnInit {

  @Input() brandsDataSource: BrandsModel;

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
    this.brandsDataSource = {
      brands: [
      ]
    }
  }

  ngOnInit(): void {
  }

}
