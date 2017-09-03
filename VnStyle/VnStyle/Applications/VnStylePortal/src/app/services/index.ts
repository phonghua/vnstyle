import { ShopService } from "./shop.service";
import { FlowerService } from "./flower.service";
import { MarkupService } from "./markup.service";
import { CountryService } from "./country.service";
import { CityService } from "./city.service";
import { DistrictService } from "./district.service";
import { HttpService } from "./http.service";
import { ArticleService } from "./articles.service";
import { LanguageService } from "./language.service";
import { CategoryService } from "./category.service";

export const SHARED_SERVICES = [
  ShopService,
  FlowerService,
  MarkupService,
  CountryService,
  CityService,
  DistrictService,
  HttpService,
  ArticleService,
  LanguageService,
  CategoryService

];

export * from "./shop.service";
export * from "./flower.service";
export * from "./markup.service";
export * from "./country.service";
export * from "./city.service";
export * from "./district.service";
export * from "./http.service";
export * from "./articles.service";
export * from "./language.service";
export * from "./category.service";