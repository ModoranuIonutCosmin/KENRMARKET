import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductsBrowserPageComponent } from './products-browser-page.component';

describe('ProductsBrowserPageComponent', () => {
  let component: ProductsBrowserPageComponent;
  let fixture: ComponentFixture<ProductsBrowserPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProductsBrowserPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProductsBrowserPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
