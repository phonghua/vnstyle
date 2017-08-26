import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { Observable } from 'rxjs/Observable';
import { SettingsService } from './settings.service';
import { AuthService } from './auth.service';
import 'rxjs/Rx';


@Injectable()
export class MarkupService {

  constructor(private httpService: HttpService, private settingService: SettingsService,
    private authService: AuthService) { }

  getMarkups(): Observable<any> {
    return this.httpService.get(this.settingService.portal + `webadmin/markups`)
      .map(res => res.json())
      .catch(err => Observable.throw(err));
  }

  getMarkup(markupId):Observable<any>{
    return this.httpService.get(this.settingService.portal + `webadmin/markups/${markupId}`)
      .map(res => res.json())
      .catch(err => Observable.throw(err));
  }

  getSlides(markupId) : Observable<any>{
    return this.httpService.get(this.settingService.portal + `webadmin/markups/${markupId}/slide`)
      .map(res => res.json())
      .catch(err => Observable.throw(err));
  }

  createMarkup(data): Observable<any> {
    return this.httpService.post(this.settingService.portal + `webadmin/markups`, data)
      .map(res => res.json())
      .catch(err => Observable.throw(err));
  }

  deleteMarkup(markupId): Observable<any> {
    return this.httpService.delete(this.settingService.portal + `webadmin/markups/${markupId}`)
      .catch(err => Observable.throw(err));
  }

  createFoodyMarkup(data): Observable<any> {
    return this.httpService.post(this.settingService.portal + "webadmin/markups/foody", data)
      .map(res => res.json())
      .catch(err => Observable.throw(err));
  }

  createMarkupService(markupId, data): Observable<any> {
    return this.httpService.post(this.settingService.portal + `webadmin/markups/${markupId}/service`, data)
      .map(res => res.json())
      .catch(err => Observable.throw(err));
  }

  removeMarkupService(markupId, serviceId): Observable<any> {
    return this.httpService.delete(this.settingService.portal + `webadmin/markups/${markupId}/service/${serviceId}`)
      .map(res => res.json())
      .catch(err => Observable.throw(err));
  }

  addMarkupService(markupId, serviceId): Observable<any> {
    return this.httpService.put(this.settingService.portal + `webadmin/markups/${markupId}/service/${serviceId}`)
      .map(res => res.json())
      .catch(err => Observable.throw(err));
  }

  activeMarkupService(markupId, serviceId, active) {
    return this.httpService.put(this.settingService.portal + `webadmin/markups/${markupId}/service/${serviceId}/active/${active}`)
      .map(res => res.json())
      .catch(err => Observable.throw(err));
  }




}
