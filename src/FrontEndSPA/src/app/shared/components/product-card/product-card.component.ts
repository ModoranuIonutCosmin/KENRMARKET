import {Component, Input, OnInit} from '@angular/core';
import {ProductModel} from "../../../features/homepage/models/productModel";

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.scss']
})
export class ProductCardComponent implements OnInit {

  @Input() productInfo: ProductModel;

  constructor() {
    this.productInfo = {
      name: "Loading",
      price: 9999,
      categoryName: "",
      reviewsCount: 0,
      tags: [],
      imageUrl: "dummy.png",
      rating: 5
    }
  }

  ngOnInit(): void {
  }

}
