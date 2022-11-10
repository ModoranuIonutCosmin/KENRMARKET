import {ProductModel} from "./productModel";

export interface ProductsGridViewModel {
  title: string,
  actionName: string,
  actionUrl: string,
  products: Array<ProductModel>,
}
