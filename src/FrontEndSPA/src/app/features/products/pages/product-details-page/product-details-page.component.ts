import {Component, OnInit} from '@angular/core';
import {ProductDetailsModel} from "../../models/product-details-model";
import {ProductsTabPanelModel} from "../../../homepage/models/products-tab-panel-model";
import {IAlbum, Lightbox, LIGHTBOX_EVENT, LightboxEvent, LightboxModule} from "ngx-lightbox";
import {Subscription} from "rxjs";

@Component({
  selector: 'app-product-details-page',
  templateUrl: './product-details-page.component.html',
  styleUrls: ['./product-details-page.component.scss']
})
export class ProductDetailsPageComponent implements OnInit {
  relatedProducts: ProductsTabPanelModel;
  productDetails: ProductDetailsModel;

  isPhotoOverlayVisible: boolean = false;
  selectedPhotoIndex: number = 0;
  newSelectedPhotoIndexAfterLighboxClose: number = 0;

  photoAlbum: Array<IAlbum> = [];
  private _subscription: Subscription = new Subscription();

  get selectedPhotoUrl(): string {
    return this.productDetails.imagesUrl[this.selectedPhotoIndex];
  }

  constructor(private _lightBox: Lightbox,
              private _lightboxEvent: LightboxEvent) {


    this.relatedProducts = {
      selectedPanelIndex: 0,
      title: "Top creatie",
      hasBanner: false,
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
      ],

    }

    this.productDetails = {
      name: 'Product name',
      description: 'Some textual gibberish about the product',
      category: 'IT',
      price: 100,
      reviewCount: 3,
      specifications: [],
      imagesUrl: ['https://m.media-amazon.com/images/I/71lgFxKFdGL._AC_SL1500_.jpg', 'https://m.media-amazon.com/images/I/81ZlT77N4SL._AC_SL1500_.jpg,' +
      'https://m.media-amazon.com/images/I/71DtXWHz77L._AC_SL1500_.jpg', 'https://m.media-amazon.com/images/I/81Kg2QzeCDL._AC_SL1500_.jpg',
        'https://m.media-amazon.com/images/I/61CWY3FHIAL._AC_SL1500_.jpg', 'https://m.media-amazon.com/images/I/71LJ+DpZhwL._AC_SL1500_.jpg']
    }
  }

  ngOnInit(): void {
    this.photoAlbum = this.productDetails.imagesUrl.map((imageUrl, index) => {
      return {
        src: imageUrl,
        thumb: imageUrl,
        caption: `Product ${index}`
      }
    })
  }



  public changeSelectedPhoto(destinationIndex: number) {
    this.selectedPhotoIndex = destinationIndex;
  }

  openPhotoLightbox() {
    this._lightBox.open(this.photoAlbum, this.selectedPhotoIndex);

    this._subscription = this._lightboxEvent.lightboxEvent$
      .subscribe(event => this._onReceivedEvent(event));
  }

  private _onReceivedEvent(event: any): void {
    // remember to unsubscribe the event when lightbox is closed
    if (event.id === LIGHTBOX_EVENT.CLOSE) {
      // event CLOSED is fired
      this._subscription.unsubscribe();

      this.selectedPhotoIndex = this.newSelectedPhotoIndexAfterLighboxClose;
    }

    if (event.id === LIGHTBOX_EVENT.OPEN) {
      // event OPEN is fired
    }

    if (event.id === LIGHTBOX_EVENT.CHANGE_PAGE) {
      // event change page is fired
      console.log(event.data); // -> image index that lightbox is switched to

      this.newSelectedPhotoIndexAfterLighboxClose = event.data;
    }
  }
}

