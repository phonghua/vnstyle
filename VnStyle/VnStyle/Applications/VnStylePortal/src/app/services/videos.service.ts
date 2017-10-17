import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { Observable } from 'rxjs/Observable';
import { SettingsService } from './settings.service';


import 'rxjs/Rx';
@Injectable()
export class VideosService {

  constructor(private httpService: HttpService, private settingService: SettingsService) { }

  getVideos(): Observable<any> {
    return this.httpService.get(this.settingService.portal + `api/videos`)
      .map(res => res.json())
      .catch(err => Observable.throw(err));
  }

  postVideo(video): Observable<any> {
    return this.httpService.post(this.settingService.portal + `api/videos`, video)
      .catch(err => Observable.throw(err));
  }

  putVideo(video) : Observable<any>{
    return this.httpService.post(this.settingService.portal + `api/videos/update`, video)
    .catch(err => Observable.throw(err));
  }

  deleteVideo(id) : Observable<any>{
    return this.httpService.post(this.settingService.portal + `api/videos/${id}/delete`)
    .catch(err => Observable.throw(err));
  }
}
