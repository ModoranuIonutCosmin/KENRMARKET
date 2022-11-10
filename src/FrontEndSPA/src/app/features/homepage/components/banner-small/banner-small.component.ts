import {Component, Input, OnInit} from '@angular/core';
import {BannerSmallModel} from "../../models/banner-small-model";

@Component({
  selector: 'app-banner-small',
  templateUrl: './banner-small.component.html',
  styleUrls: ['./banner-small.component.scss']
})
export class BannerSmallComponent implements OnInit {

  @Input() bannerSmallDataSource: BannerSmallModel;

  constructor() {
    this.bannerSmallDataSource = {
      productImgUrl: "https://www.pngfind.com/pngs/m/623-6236830_gopro-hero-7-black-gopro-hero-7-black.png",
      backgroundImgUrl: "https://namstare.ro/wp-content/uploads/2021/09/news_banner_gopro_hero10.jpg",
      title: 'Shop Today\' deals',
      description: 'Awesome made easy. HERO7 Black',
      actionName: 'Shop Now - $429.99',
      actionDestionationUrl: 'https://www.emag.ro/camera-video-sport-gopro-hero7-4k-gps-black-edition-chdhx-701-rw/pd/DJNZ0VBBM/'
    }
  }

  ngOnInit(): void {
  }

}
