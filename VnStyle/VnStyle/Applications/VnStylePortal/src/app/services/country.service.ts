import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { Observable } from 'rxjs/Observable';
import { SettingsService } from './settings.service';


import 'rxjs/Rx';
@Injectable()
export class CountryService {

  constructor(private httpService: HttpService, private settingService: SettingsService) { }

  getCountries(): Observable<any> {
    return this.httpService.get(this.settingService.portal + `webadmin/countries`)
      .map(res => res.json())
      .catch(err => Observable.throw(err));
  }

}
