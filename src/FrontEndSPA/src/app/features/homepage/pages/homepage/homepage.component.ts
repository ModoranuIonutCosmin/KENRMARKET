import {Component, OnInit} from '@angular/core';
import {ProductsTabPanelModel} from "../../models/products-tab-panel-model";
import {ProductsGridViewModel} from "../../models/products-grid-view-model";
import {GuaranteesModel} from "../../models/guarantees-model";
import {CategoriesModel} from "../../models/categories-model";
import {BrandsModel} from "../../models/brands-model";
import {BannerSmallModel} from "../../models/banner-small-model";
import {DealsModel} from "../../models/deals-model";

@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.scss']
})
export class HomepageComponent implements OnInit {
  topTabPanelsDataSource: ProductsTabPanelModel;
  newArrivalsTabPanelsDataSource: ProductsTabPanelModel;
  recommendationsDataSource: ProductsGridViewModel;
  guaranteesDataSource: GuaranteesModel;
  categoriesDataSource: CategoriesModel;
  brandsDataSource: BrandsModel;
  smallBannerDataSource: BannerSmallModel;
  dealsDataSource: DealsModel;

  constructor() {

    this.dealsDataSource = {
      deals: [
        {
          actionButtonDestinationUrl: 'http://www.google.com',
          title: 'Beats by DRE',
          actionButtonText: 'Shop now',
          priceLabel: '$342.99',
          priceValue: '$299.99',
          type: 'Deals and promotions',
          bannerImgUrl: 'https://storage.pixteller.com/designs/designs-images/2020-12-21/06/headphones-sales-banner-1-5fe0c6a0c06bc.png'
        },
        {
          actionButtonDestinationUrl: 'google.com',
          title: 'New Arrival',
          actionButtonText: 'Shop now',
          priceLabel: '$999.99',
          priceValue: '$299.99',
          type: 'Deals and promotions',
          bannerImgUrl: 'https://thumbs.dreamstime.com/b/white-wireless-headphones-yellow-background-flat-lay-banner-copy-space-top-view-technology-hipster-accessory-perfect-song-227089974.jpg'
        }
      ]
    }
    this.smallBannerDataSource = {
      productImgUrl: "https://png2.cleanpng.com/sh/35b68c00c47c737640546a6004009597/L0KzQYm3WMI1N5DnipH0aYP2gLBuTfdweKN0RdpucnA4PbPzgfNsNZh0iOR4LXjogrE8TgNme6Ruh9C2Z3BzgrE0iPVzd145RddtaYSwg732jf8ufpppfdH8LXb1f760hB9xepCyh9C2eXB4gn7wkPhwdpYyj9t9aD3wPYbpWMA1a5U3S9c5NUSzPoKCUsIxOGI3Sac8NUG2RIq6UMI2PGIziNDw/kisspng-gopro-hero5-black-gopro-hero5-session-gopro-hero-4-edit-slomo-videos-from-gopro-on-your-iphone-with-m-5b804cd23e0540.1922001215351349302541.png",
      backgroundImgUrl: "https://namstare.ro/wp-content/uploads/2021/09/news_banner_gopro_hero10.jpg",
      title: 'Shop Today\' deals',
      description: 'Awesome made easy. HERO7 Black',
      actionName: 'Shop Now - $429.99',
      actionDestionationUrl: 'https://www.emag.ro/camera-video-sport-gopro-hero7-4k-gps-black-edition-chdhx-701-rw/pd/DJNZ0VBBM/'
    }

    this.newArrivalsTabPanelsDataSource = {
      selectedPanelIndex: 0,
      title: "New arrivals",
      hasBanner: false,
      productTabs: [
        {
          name: 'ALL',

          products: [
            {
              name: 'Bose - SoundSport  wireless headphones',
              rating: 5,
              tags: ["top"],
              price: 200,
              categoryName: "Headphones",
              reviewsCount: 4,
              imageUrl: "https://img.freepik.com/free-photo/pink-headphones-wireless-digital-device_53876-96804.jpg?w=2000"
            },
            {
              name: 'Bose - SoundSport  wireless headphones2',
              rating: 4,
              tags: ["top"],
              price: 150,
              categoryName: "Headphones",
              reviewsCount: 11,
              imageUrl: "https://img.freepik.com/free-photo/pink-headphones-wireless-digital-device_53876-96804.jpg?w=2000"
            },

            {
              name: 'Bose - SoundSport  wireless headphones3',
              rating: 2,
              tags: ["top"],
              price: 111,
              categoryName: "Headphones",
              reviewsCount: 110,
              imageUrl: "https://img.freepik.com/free-photo/pink-headphones-wireless-digital-device_53876-96804.jpg?w=2000"
            },
          ]
        },
        {
          name: 'TV',

          products: [
            {
              name: 'Bose - SoundSport  wireless headphones',
              rating: 5,
              tags: ["top"],
              price: 200,
              categoryName: "Headphones",
              reviewsCount: 4,
              imageUrl: "https://img.freepik.com/free-photo/pink-headphones-wireless-digital-device_53876-96804.jpg?w=2000"
            },
            {
              name: 'Bose - SoundSport  wireless headphones2',
              rating: 4,
              tags: ["top"],
              price: 150,
              categoryName: "Headphones",
              reviewsCount: 11,
              imageUrl: "https://img.freepik.com/free-photo/pink-headphones-wireless-digital-device_53876-96804.jpg?w=2000"
            },

            {
              name: 'Bose - SoundSport  wireless headphones3',
              rating: 2,
              tags: ["top"],
              price: 111,
              categoryName: "Headphones",
              reviewsCount: 110,
              imageUrl: "https://img.freepik.com/free-photo/pink-headphones-wireless-digital-device_53876-96804.jpg?w=2000"
            },
          ]
        },
        {
          name: 'COMPUTERS',

          products: [
            {
              name: 'Bose - SoundSport  wireless headphones',
              rating: 5,
              tags: ["top"],
              price: 200,
              categoryName: "Headphones",
              reviewsCount: 4,
              imageUrl: "https://img.freepik.com/free-photo/pink-headphones-wireless-digital-device_53876-96804.jpg?w=2000"
            },
            {
              name: 'Bose - SoundSport  wireless headphones2',
              rating: 4,
              tags: ["top"],
              price: 150,
              categoryName: "Headphones",
              reviewsCount: 11,
              imageUrl: "https://img.freepik.com/free-photo/pink-headphones-wireless-digital-device_53876-96804.jpg?w=2000"
            },

            {
              name: 'Bose - SoundSport  wireless headphones3',
              rating: 2,
              tags: ["top"],
              price: 111,
              categoryName: "Headphones",
              reviewsCount: 110,
              imageUrl: "https://img.freepik.com/free-photo/pink-headphones-wireless-digital-device_53876-96804.jpg?w=2000"
            },
          ]
        },
        {
          name: 'TABLETS',

          products: [
            {
              name: 'Bose - SoundSport  wireless headphones',
              rating: 5,
              tags: ["top"],
              price: 200,
              categoryName: "Headphones",
              reviewsCount: 4,
              imageUrl: "https://img.freepik.com/free-photo/pink-headphones-wireless-digital-device_53876-96804.jpg?w=2000"
            },
            {
              name: 'Bose - SoundSport  wireless headphones2',
              rating: 4,
              tags: ["top"],
              price: 150,
              categoryName: "Headphones",
              reviewsCount: 11,
              imageUrl: "https://img.freepik.com/free-photo/pink-headphones-wireless-digital-device_53876-96804.jpg?w=2000"
            },

            {
              name: 'Bose - SoundSport  wireless headphones3',
              rating: 2,
              tags: ["top"],
              price: 111,
              categoryName: "Headphones",
              reviewsCount: 110,
              imageUrl: "https://img.freepik.com/free-photo/pink-headphones-wireless-digital-device_53876-96804.jpg?w=2000"
            },
          ]
        },
        {
          name: 'SMARTWATCHES',

          products: [
            {
              name: 'Bose - SoundSport  wireless headphones',
              rating: 5,
              tags: ["top"],
              price: 200,
              categoryName: "Headphones",
              reviewsCount: 4,
              imageUrl: "https://img.freepik.com/free-photo/pink-headphones-wireless-digital-device_53876-96804.jpg?w=2000"
            },
            {
              name: 'Bose - SoundSport  wireless headphones2',
              rating: 4,
              tags: ["top"],
              price: 150,
              categoryName: "Headphones",
              reviewsCount: 11,
              imageUrl: "https://img.freepik.com/free-photo/pink-headphones-wireless-digital-device_53876-96804.jpg?w=2000"
            },

            {
              name: 'Bose - SoundSport  wireless headphones3',
              rating: 2,
              tags: ["top"],
              price: 111,
              categoryName: "Headphones",
              reviewsCount: 110,
              imageUrl: "https://img.freepik.com/free-photo/pink-headphones-wireless-digital-device_53876-96804.jpg?w=2000"
            },
          ]
        },
        {
          name: 'ACCESSORIES',

          products: [
            {
              name: 'Bose - SoundSport  wireless headphones',
              rating: 5,
              tags: ["top"],
              price: 200,
              categoryName: "Headphones",
              reviewsCount: 4,
              imageUrl: "https://img.freepik.com/free-photo/pink-headphones-wireless-digital-device_53876-96804.jpg?w=2000"
            },
            {
              name: 'Bose - SoundSport  wireless headphones2',
              rating: 4,
              tags: ["top"],
              price: 150,
              categoryName: "Headphones",
              reviewsCount: 11,
              imageUrl: "https://img.freepik.com/free-photo/pink-headphones-wireless-digital-device_53876-96804.jpg?w=2000"
            },

            {
              name: 'Bose - SoundSport  wireless headphones3',
              rating: 2,
              tags: ["top"],
              price: 111,
              categoryName: "Headphones",
              reviewsCount: 110,
              imageUrl: "https://img.freepik.com/free-photo/pink-headphones-wireless-digital-device_53876-96804.jpg?w=2000"
            },
          ]
        },
      ]
    }
    this.topTabPanelsDataSource = {
      selectedPanelIndex: 0,
      title: "Top creatie",
      hasBanner: true,
      bannerUrl: "https://images.samsung.com/is/image/samsung/assets/us/a-assets/09192022/Post-DSE-Fall-Banner-MB-768x1400.jpg?$FB_TYPE_A_MO_JPG$",
      bannerDestinationUrl: "www.samsung.ro",
      productTabs: [
        {
          name: 'Explore',

          products: [
            {
              name: 'Bose - SoundSport  wireless headphones',
              rating: 5,
              tags: ["top"],
              price: 200,
              categoryName: "Headphones",
              reviewsCount: 4,
              imageUrl: "https://img.freepik.com/free-photo/pink-headphones-wireless-digital-device_53876-96804.jpg?w=2000"
            },
            {
              name: 'Bose - SoundSport  wireless headphones2',
              rating: 4,
              tags: ["top"],
              price: 150,
              categoryName: "Headphones",
              reviewsCount: 11,
              imageUrl: "https://img.freepik.com/free-photo/pink-headphones-wireless-digital-device_53876-96804.jpg?w=2000"
            },

            {
              name: 'Bose - SoundSport  wireless headphones3',
              rating: 2,
              tags: ["top"],
              price: 111,
              categoryName: "Headphones",
              reviewsCount: 110,
              imageUrl: "https://img.freepik.com/free-photo/pink-headphones-wireless-digital-device_53876-96804.jpg?w=2000"
            },
          ]
        },

        {
          name: 'Next',
          products: [
            {
              name: 'Bose - SoundSport  wireless headphones',
              rating: 5,
              tags: ["top"],
              price: 200,
              categoryName: "Headphones",
              reviewsCount: 4,
              imageUrl: "https://img.freepik.com/free-photo/pink-headphones-wireless-digital-device_53876-96804.jpg?w=2000"
            },
            {
              name: 'Bose - SoundSport  wireless headphones2',
              rating: 4,
              tags: ["top"],
              price: 150,
              categoryName: "Headphones",
              reviewsCount: 11,
              imageUrl: "https://img.freepik.com/free-photo/pink-headphones-wireless-digital-device_53876-96804.jpg?w=2000"
            },

            {
              name: 'Bose - SoundSport  wireless headphones3',
              rating: 2,
              tags: ["top"],
              price: 111,
              categoryName: "Headphones",
              reviewsCount: 110,
              imageUrl: "https://img.freepik.com/free-photo/pink-headphones-wireless-digital-device_53876-96804.jpg?w=2000"
            },
          ]
        },

        {
          name: 'Next11',
          products: [
            {
              name: 'Bose - SoundSport  wireless headphones',
              rating: 5,
              tags: ["top"],
              price: 200,
              categoryName: "Headphones",
              reviewsCount: 4,
              imageUrl: "https://img.freepik.com/free-photo/pink-headphones-wireless-digital-device_53876-96804.jpg?w=2000"
            },
            {
              name: 'Bose - SoundSport  wireless headphones2',
              rating: 4,
              tags: ["top"],
              price: 150,
              categoryName: "Headphones",
              reviewsCount: 11,
              imageUrl: "https://img.freepik.com/free-photo/pink-headphones-wireless-digital-device_53876-96804.jpg?w=2000"
            },

            {
              name: 'Bose - SoundSport  wireless headphones3',
              rating: 2,
              tags: ["top"],
              price: 111,
              categoryName: "Headphones",
              reviewsCount: 110,
              imageUrl: "https://img.freepik.com/free-photo/pink-headphones-wireless-digital-device_53876-96804.jpg?w=2000"
            },

            {
              name: 'Bose - SoundSport  wireless headphones4',
              rating: 2,
              tags: ["top"],
              price: 111,
              categoryName: "Headphones",
              reviewsCount: 110,
              imageUrl: "https://img.freepik.com/free-photo/pink-headphones-wireless-digital-device_53876-96804.jpg?w=2000"
            },

            {
              name: 'Bose - SoundSport  wireless headphones5',
              rating: 2,
              tags: ["top"],
              price: 111,
              categoryName: "Headphones",
              reviewsCount: 110,
              imageUrl: "https://img.freepik.com/free-photo/pink-headphones-wireless-digital-device_53876-96804.jpg?w=2000"
            },
          ]
        },

      ],

    }

    this.guaranteesDataSource = {
      guarantess: [
        {
          name: 'Free shipping',
          description: 'Orders $50 or more',
          icon_name: 'icon-rocket'
        },
        {
          name: 'Free returns',
          description: 'Within 30 days',
          icon_name: 'icon-rotate-left'
        },
        {
          name: 'Get 20% Off 1 item',
          description: 'when you sign up',
          icon_name: 'icon-info-circle'
        },
        {
          name: '24/7 support',
          description: 'Contact us about any issue',
          icon_name: 'icon-life-ring'
        }
      ]
    }


    this.recommendationsDataSource = {
      title: 'Recommendation For You',
      actionName: 'View All Recommendations',
      actionUrl: '/',
      products: [
        {
          name: 'Bose - SoundSport  wireless headphones',
          rating: 5,
          tags: ["top"],
          price: 200,
          categoryName: "Headphones",
          reviewsCount: 4,
          imageUrl: "https://img.freepik.com/free-photo/pink-headphones-wireless-digital-device_53876-96804.jpg?w=2000"
        },
        {
          name: 'Bose - SoundSport  wireless headphones',
          rating: 5,
          tags: ["top"],
          price: 200,
          categoryName: "Headphones",
          reviewsCount: 4,
          imageUrl: "https://img.freepik.com/free-photo/pink-headphones-wireless-digital-device_53876-96804.jpg?w=2000"
        },
        {
          name: 'Bose - SoundSport  wireless headphones',
          rating: 5,
          tags: ["top"],
          price: 200,
          categoryName: "Headphones",
          reviewsCount: 4,
          imageUrl: "https://img.freepik.com/free-photo/pink-headphones-wireless-digital-device_53876-96804.jpg?w=2000"
        },
        {
          name: 'Bose - SoundSport  wireless headphones',
          rating: 5,
          tags: ["top"],
          price: 200,
          categoryName: "Headphones",
          reviewsCount: 4,
          imageUrl: "https://img.freepik.com/free-photo/pink-headphones-wireless-digital-device_53876-96804.jpg?w=2000"
        },
        {
          name: 'Bose - SoundSport  wireless headphones',
          rating: 5,
          tags: ["top"],
          price: 200,
          categoryName: "Headphones",
          reviewsCount: 4,
          imageUrl: "https://img.freepik.com/free-photo/pink-headphones-wireless-digital-device_53876-96804.jpg?w=2000"
        },
        {
          name: 'Bose - SoundSport  wireless headphones',
          rating: 5,
          tags: ["top"],
          price: 200,
          categoryName: "Headphones",
          reviewsCount: 4,
          imageUrl: "https://img.freepik.com/free-photo/pink-headphones-wireless-digital-device_53876-96804.jpg?w=2000"
        },
        {
          name: 'Bose - SoundSport  wireless headphones',
          rating: 5,
          tags: ["top"],
          price: 200,
          categoryName: "Headphones",
          reviewsCount: 4,
          imageUrl: "https://img.freepik.com/free-photo/pink-headphones-wireless-digital-device_53876-96804.jpg?w=2000"
        },
        {
          name: 'Bose - SoundSport  wireless headphones',
          rating: 5,
          tags: ["top"],
          price: 200,
          categoryName: "Headphones",
          reviewsCount: 4,
          imageUrl: "https://img.freepik.com/free-photo/pink-headphones-wireless-digital-device_53876-96804.jpg?w=2000"
        },
      ]
    }

    this.categoriesDataSource = {
      title: 'Popular categories',
      categories: [
        {
          title: 'Computer & Laptop',
          imageUrl: "https://5.grgs.ro/images/products/1/1323/2115522/normal/133-macbook-air-13-with-retina-true-tone-apple-m1-chip-8-core-cpu-8gb-512gb-ssd-apple-m1-8-core-gpu-macos-big-sur-gold-int-keyboard-late-2020-b4c0766452a5cefdc6050ca9b8ac5c0b.jpg",
          actionClickDestinationUrl: '/'
        },
        {
          title: 'Computer & Laptop',
          imageUrl: "https://5.grgs.ro/images/products/1/1323/2115522/normal/133-macbook-air-13-with-retina-true-tone-apple-m1-chip-8-core-cpu-8gb-512gb-ssd-apple-m1-8-core-gpu-macos-big-sur-gold-int-keyboard-late-2020-b4c0766452a5cefdc6050ca9b8ac5c0b.jpg",
          actionClickDestinationUrl: '/'
        },
        {
          title: 'Computer & Laptop',
          imageUrl: "https://5.grgs.ro/images/products/1/1323/2115522/normal/133-macbook-air-13-with-retina-true-tone-apple-m1-chip-8-core-cpu-8gb-512gb-ssd-apple-m1-8-core-gpu-macos-big-sur-gold-int-keyboard-late-2020-b4c0766452a5cefdc6050ca9b8ac5c0b.jpg",
          actionClickDestinationUrl: '/'
        },
        {
          title: 'Computer & Laptop',
          imageUrl: "https://5.grgs.ro/images/products/1/1323/2115522/normal/133-macbook-air-13-with-retina-true-tone-apple-m1-chip-8-core-cpu-8gb-512gb-ssd-apple-m1-8-core-gpu-macos-big-sur-gold-int-keyboard-late-2020-b4c0766452a5cefdc6050ca9b8ac5c0b.jpg",
          actionClickDestinationUrl: '/'
        },
        {
          title: 'Computer & Laptop',
          imageUrl: "https://5.grgs.ro/images/products/1/1323/2115522/normal/133-macbook-air-13-with-retina-true-tone-apple-m1-chip-8-core-cpu-8gb-512gb-ssd-apple-m1-8-core-gpu-macos-big-sur-gold-int-keyboard-late-2020-b4c0766452a5cefdc6050ca9b8ac5c0b.jpg",
          actionClickDestinationUrl: '/'
        },
      ]
    }

    this.brandsDataSource = {
      brands: [
        {
          name: 'Le barrel',
          imgUrl: 'https://img.freepik.com/free-vector/vintage-monochrome-brewing-company-logotype_225004-1219.jpg?w=2000',
          clickActionDestinationUrl: 'https://www.pilsnerurquell.com/'
        },
        {
          name: 'Le barrel',
          imgUrl: 'https://img.freepik.com/free-vector/vintage-monochrome-brewing-company-logotype_225004-1219.jpg?w=2000',
          clickActionDestinationUrl: 'https://www.pilsnerurquell.com/'
        },
        {
          name: 'Le barrel',
          imgUrl: 'https://img.freepik.com/free-vector/vintage-monochrome-brewing-company-logotype_225004-1219.jpg?w=2000',
          clickActionDestinationUrl: 'https://www.pilsnerurquell.com/'
        },
        {
          name: 'Le barrel',
          imgUrl: 'https://img.freepik.com/free-vector/vintage-monochrome-brewing-company-logotype_225004-1219.jpg?w=2000',
          clickActionDestinationUrl: 'https://www.pilsnerurquell.com/'
        },
      ]
    }

  }

  ngOnInit(): void {
  }

}
