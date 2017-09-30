import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { Observable } from 'rxjs/Observable';
import { SettingsService } from './settings.service';


import 'rxjs/Rx';
@Injectable()
export class ArtistService {

  constructor(private httpService: HttpService, private settingService: SettingsService) { }

  getArtists(): Observable<any> {
    return this.httpService.get(this.settingService.portal + `api/artists`)
      .map(res => res.json())
      .catch(err => Observable.throw(err));
  }

  createArtist(artist): Observable<any> {
    return this.httpService.post(this.settingService.portal + 'api/artists', artist)
      .catch(err => Observable.throw(err));
  }

  updateArtist(artistId, artist): Observable<any> {
    return this.httpService.put(this.settingService.portal + `api/artists/${artistId}`, artist)
      .catch(err => Observable.throw(err));
  }


  deleteArtist(artistId): Observable<any> {
    return this.httpService.delete(this.settingService.portal + `api/artists/${artistId}`).catch(err => Observable.throw(err));
  }

  getPhoto(artistId): Observable<any> {
    return this.httpService.get(this.settingService.portal + `api/artists/${artistId}/photo`)
      .map(res => res.json())
      .catch(err => Observable.throw(err));
  }

  deletePhoto(artistId, photoId) : Observable<any>{
    return this.httpService.delete(this.settingService.portal + `api/artists/${artistId}/photo/${photoId}`)
    .map(res => res.json())
    .catch(err => Observable.throw(err));
  }
}
