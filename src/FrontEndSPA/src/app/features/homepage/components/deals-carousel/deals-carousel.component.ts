import {Component, Input, OnInit} from '@angular/core';
import {DealsModel} from "../../models/deals-model";

@Component({
  selector: 'app-deals-carousel',
  templateUrl: './deals-carousel.component.html',
  styleUrls: ['./deals-carousel.component.scss']
})
export class DealsCarouselComponent implements OnInit {

  @Input() dealsDataSource: DealsModel;

  constructor() {
    this.dealsDataSource = {
      deals: [
      ]
    }
  }

  ngOnInit() {

  }


}
