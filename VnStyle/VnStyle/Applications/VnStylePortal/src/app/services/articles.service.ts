import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { Observable } from 'rxjs/Observable';
import { SettingsService } from './settings.service';


import 'rxjs/Rx';
@Injectable()
export class ArticleService {

  constructor(private httpService: HttpService, private settingService: SettingsService) { }

  getArticles(): Observable<any> {
    return this.httpService.get(this.settingService.portal + `api/articles`)
      .map(res => res.json())
      .catch(err => Observable.throw(err));
  }

}