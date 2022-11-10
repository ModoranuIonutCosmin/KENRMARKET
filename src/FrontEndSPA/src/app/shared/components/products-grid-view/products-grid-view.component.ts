import {Component, Input, OnInit} from '@angular/core';
import {ProductsGridViewModel} from "../../../features/homepage/models/products-grid-view-model";

@Component({
  selector: 'app-products-grid-view',
  templateUrl: './products-grid-view.component.html',
  styleUrls: ['./products-grid-view.component.scss']
})
export class ProductsGridViewComponent implements OnInit {
  @Input() productsGridViewModel: ProductsGridViewModel;

  constructor() {
    this.productsGridViewModel = {
      title: 'Recommendation For You',
      actionName: 'View All Recommendations',
      actionUrl: '/',
      products: []
    }

  }

  ngOnInit(): void {
  }

}
