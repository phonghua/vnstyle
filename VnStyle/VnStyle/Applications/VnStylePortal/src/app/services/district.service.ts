import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { Observable } from 'rxjs/Observable';
import { SettingsService } from './settings.service';


import 'rxjs/Rx';
@Injectable()
export class DistrictService {

  constructor(private httpService: HttpService, private settingService: SettingsService) { }

  getDistricts(): Observable<any> {
    return this.httpService.get(this.settingService.portal + `webadmin/districts`)
      .map(res => res.json())
      .catch(err => Observable.throw(err));
  }

}
