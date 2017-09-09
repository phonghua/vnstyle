import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { Observable } from 'rxjs/Observable';
import { SettingsService } from './settings.service';


import 'rxjs/Rx';
@Injectable()
export class GalleryService {

  constructor(private httpService: HttpService, private settingService: SettingsService) { }

  getPhoto(cateId): Observable<any> {
    return this.httpService.get(this.settingService.portal + `api/galleries/photo?cateId=${cateId}`)
      .map(res => res.json())
      .catch(err => Observable.throw(err));
  }

}
