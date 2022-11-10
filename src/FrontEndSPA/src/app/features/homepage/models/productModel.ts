export interface ProductModel {
  name: string,
  categoryName: string,
  imageUrl: string,
  price: number,
  rating: number,
  reviewsCount: number,
  tags: Array<string>
}
