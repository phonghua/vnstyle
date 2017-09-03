import { Injectable } from "@angular/core";
import { HttpService } from "./http.service";
import { Observable } from "rxjs/Observable";
import { SettingsService } from "./settings.service";
@Injectable()
export class CategoryService {
  constructor(
    private httpService: HttpService,
    private settingService: SettingsService
  ) {}

  getCategories(rootCateId): Observable<any> {
    return this.httpService.get(this.settingService.portal + `api/categories/${rootCateId}/query`)
    .map(res => res.json())
    .catch(err => Observable.throw(err));
  }
}