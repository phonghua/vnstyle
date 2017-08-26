import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { Observable } from 'rxjs/Observable';
import { SettingsService } from './settings.service';
import 'rxjs/Rx';

@Injectable()
export class MarkupPromotionService {

  constructor(private httpService: HttpService, private settingService: SettingsService) { }

  // initializeMarkupPage(): Observable<any> {
    
  //   var data = { query: "query { " + query.join(",") + " }" };
  //   return this.httpService.post(this.settingService.portal + "webapi/graph/BizApp", data)
  //     .map(res => res.json())
  //     .catch(err => Observable.throw(err));
  // }

  

}
