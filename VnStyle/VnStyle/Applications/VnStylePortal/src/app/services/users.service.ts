import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { Observable } from 'rxjs/Observable';
import { SettingsService } from './settings.service';


import 'rxjs/Rx';
@Injectable()
export class UsersService {

  constructor(private httpService: HttpService, private settingService: SettingsService) { }

  getUsers(): Observable<any> {
    return this.httpService.get(this.settingService.portal + `api/users`)
      .map(res => res.json())
      .catch(err => Observable.throw(err));
  }

  createUser(user): Observable<any> {
    return this.httpService.post(this.settingService.portal + `api/users`, user)
      .catch(err => Observable.throw(err));
  }

  resetPassword(id, model): Observable<any> {
    return this.httpService.put(this.settingService.portal + `api/users/${id}/reset-password`, model)
      .catch(err => Observable.throw(err));
  }

  // putVideo(video) : Observable<any>{
  //   return this.httpService.put(this.settingService.portal + `api/videos`, video)
  //   .catch(err => Observable.throw(err));
  // }

  deleteUser(id) : Observable<any>{
    return this.httpService.delete(this.settingService.portal + `api/users/${id}`)
    .catch(err => Observable.throw(err));
  }
}
