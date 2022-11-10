import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductsTabPanelCarouselComponent } from './products-tab-panel-carousel.component';

describe('ProductsTabPanelCarouselComponent', () => {
  let component: ProductsTabPanelCarouselComponent;
  let fixture: ComponentFixture<ProductsTabPanelCarouselComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProductsTabPanelCarouselComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProductsTabPanelCarouselComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
