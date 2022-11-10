import {ProductsTabModel} from "./products-tab-model";

export interface ProductsTabPanelModel {
  title: string,
  selectedPanelIndex: number,
  hasBanner: boolean,
  bannerDestinationUrl?: string
  bannerUrl?: string,
  productTabs: Array<ProductsTabModel>
}
