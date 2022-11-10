export interface BrandModel {
  name: string,
  imgUrl: string,
  clickActionDestinationUrl: string
}

export interface BrandsModel {
  brands: Array<BrandModel>
}
