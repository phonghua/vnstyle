import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { Observable } from 'rxjs/Observable';
import { SettingsService } from './settings.service';


import 'rxjs/Rx';
@Injectable()
export class CityService {

  constructor(private httpService: HttpService, private settingService: SettingsService) { }

  getCities(): Observable<any> {
    return this.httpService.get(this.settingService.portal + `webadmin/cities`)
      .map(res => res.json())
      .catch(err => Observable.throw(err));
  }

}
