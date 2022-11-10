import { Component, OnInit } from '@angular/core';
import {ProductsGridViewModel} from "../../../homepage/models/products-grid-view-model";

@Component({
  selector: 'app-products-browser-page',
  templateUrl: './products-browser-page.component.html',
  styleUrls: ['./products-browser-page.component.scss']
})
export class ProductsBrowserPageComponent implements OnInit {

  productsBrowserDataSource: ProductsGridViewModel;

  constructor() {

    var products = [
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
      }]

    this.productsBrowserDataSource = {
      products: products,
      title: 'Products browser',
      actionName: 'Browse ->',
      actionUrl: "https://google.com"
    }

  }

  ngOnInit(): void {
  }

}
