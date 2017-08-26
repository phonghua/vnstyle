import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { Observable } from 'rxjs/Observable';
import { SettingsService } from './settings.service';


import 'rxjs/Rx';
@Injectable()
export class FlowerService {

  constructor(private httpService: HttpService, private settingService: SettingsService) { }

  getFlowers(): Observable<any> {
    return this.httpService.get(this.settingService.portal + `webadmin/flowers`)
      .map(res => res.json())
      .catch(err => Observable.throw(err));
  }

}
