export interface ProductDetailsModel {
  name: string,
  description: string,
  price: number,
  category: string,
  reviewCount: number,
  imagesUrl: Array<string>,
  specifications: Array<any>
}
