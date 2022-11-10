import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BannerSmallComponent } from './banner-small.component';

describe('BannerSmallComponent', () => {
  let component: BannerSmallComponent;
  let fixture: ComponentFixture<BannerSmallComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BannerSmallComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BannerSmallComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
