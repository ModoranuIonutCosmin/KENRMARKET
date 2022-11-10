import {Component, Input, OnInit} from '@angular/core';
import {ProductsGridViewModel} from "../../../homepage/models/products-grid-view-model";
import {ProductsPaginatedGridViewModel} from "../../models/products-paginated-grid-view-model";

@Component({
  selector: 'app-products-paginated-grid-view',
  templateUrl: './products-paginated-grid-view.component.html',
  styleUrls: ['./products-paginated-grid-view.component.scss']
})
export class ProductsPaginatedGridViewComponent implements OnInit {

  currentPageIndex: number = 0;
  @Input() productPages: ProductsGridViewModel;

  constructor() {
    this.productPages = {
      actionName: "", actionUrl: "", title: "",
      products: []
    }
  }

  ngOnInit(): void {
  }

}
