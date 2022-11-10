import {Component, Input, OnInit} from '@angular/core';
import {GuaranteesModel} from "../../models/guarantees-model";

@Component({
  selector: 'app-guarantees',
  templateUrl: './guarantees.component.html',
  styleUrls: ['./guarantees.component.scss']
})
export class GuaranteesComponent implements OnInit {

  @Input() guaranteesDataSource: GuaranteesModel;

  constructor() {
    this.guaranteesDataSource = {
      guarantess: [
      ]
    }
  }

  ngOnInit(): void {
  }

}
