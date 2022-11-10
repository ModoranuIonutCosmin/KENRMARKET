import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CategoriesRowComponent } from './categories-row.component';

describe('CategoriesRowComponent', () => {
  let component: CategoriesRowComponent;
  let fixture: ComponentFixture<CategoriesRowComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CategoriesRowComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CategoriesRowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
