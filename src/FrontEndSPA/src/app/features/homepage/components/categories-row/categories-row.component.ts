import {Component, Input, OnInit} from '@angular/core';
import {CategoriesModel} from "../../models/categories-model";

@Component({
  selector: 'app-categories-row',
  templateUrl: './categories-row.component.html',
  styleUrls: ['./categories-row.component.scss']
})
export class CategoriesRowComponent implements OnInit {

  @Input() categoriesDataSource: CategoriesModel;

  constructor() {
    this.categoriesDataSource = {
      categories: [],
      title: 'Explore popular categories'
    }
  }

  ngOnInit(): void {
  }

}
