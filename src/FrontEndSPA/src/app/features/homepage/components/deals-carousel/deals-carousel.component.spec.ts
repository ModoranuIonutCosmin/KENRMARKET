import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DealsCarouselComponent } from './deals-carousel.component';

describe('DealsCarouselComponent', () => {
  let component: DealsCarouselComponent;
  let fixture: ComponentFixture<DealsCarouselComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DealsCarouselComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DealsCarouselComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
