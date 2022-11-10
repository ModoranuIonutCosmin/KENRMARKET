import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductsPaginatedGridViewComponent } from './products-paginated-grid-view.component';

describe('ProductsPaginatedGridViewComponent', () => {
  let component: ProductsPaginatedGridViewComponent;
  let fixture: ComponentFixture<ProductsPaginatedGridViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProductsPaginatedGridViewComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProductsPaginatedGridViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
