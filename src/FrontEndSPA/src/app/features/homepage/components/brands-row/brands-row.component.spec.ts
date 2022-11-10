import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BrandsRowComponent } from './brands-row.component';

describe('BrandsRowComponent', () => {
  let component: BrandsRowComponent;
  let fixture: ComponentFixture<BrandsRowComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BrandsRowComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BrandsRowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
