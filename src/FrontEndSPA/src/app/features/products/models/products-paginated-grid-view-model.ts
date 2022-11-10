import {ProductModel} from "../../homepage/models/productModel";

export interface ProductsPaginatedGridViewModel {
  title: string,
  actionName: string,
  actionUrl: string,
  pages: Array<Array<ProductModel>>
}
