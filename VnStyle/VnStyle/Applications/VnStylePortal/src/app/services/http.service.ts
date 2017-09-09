import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { Observable } from 'rxjs/Observable';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import {SettingsService} from './settings.service';

@Injectable()
export class HttpService {
  //authHeaders: Headers;

  constructor(private authService: AuthService, private http: Http , private settingService : SettingsService) { }

  baseHeader() {
    //let headerSetting = new Headers();
    //let body = JSON.stringify(data);
    let headers = new Headers({ 'Content-Type': 'application/json' });
    headers.append('Authorization', this.authService.currentUser.token_type + " " + this.authService.currentUser.access_token);
    return headers
  }

  post(url, data?): Observable<any> {
    let body = JSON.stringify(data);
    let headers = this.baseHeader();
    let options = new RequestOptions({ headers: headers });

    return this.http.post(url, body, options);
  }

  get(url): Observable<any> {
    let headers = this.baseHeader();
    let options = new RequestOptions({ headers: headers });

    return this.http.get(url, options);
  }

  delete(url): Observable<any> {
    let headers = this.baseHeader();
    let options = new RequestOptions({ headers: headers });
    return this.http.delete(url, options);
  }

  put(url, data?): Observable<any> {
    let body = JSON.stringify(data);
    let headers = this.baseHeader();
    let options = new RequestOptions({ headers: headers });

    return this.http.put(url, body, options);
  }


  postGeneralFile(files, options?) {
    let formData: FormData = new FormData(),
      xhr: XMLHttpRequest = new XMLHttpRequest();

    for (let i = 0; i < files.length; i++) {
      formData.append("uploads[]", files[i], files[i].name);
    }

    xhr.onreadystatechange = () => {
      if (xhr.readyState === 4) {
        if (xhr.status === 200) {
          if (options && options.onFinished) options.onFinished(JSON.parse(xhr.response));
        } else {
          // console.log("Upload Error");
          if (options && options.error) options.error();
        }
      }
    };

    xhr.upload.onprogress = (event) => {
      // this.progress = Math.round(event.loaded / event.total * 100);
      // this.progressObserver.next(this.progress);

      var progress = Math.round(event.loaded / event.total * 100);
      console.log("onprogress", progress);

      if (options && options.onProgress) options.onProgress(progress);

    };

    xhr.open('POST', this.settingService.portal + "api/media/photo/", true);
    xhr.setRequestHeader("Authorization", this.authService.currentUser.token_type + " " + this.authService.currentUser.access_token);
    xhr.send(formData);
  }


  postGalleryPhoto(cateId, files, options?) {
    let formData: FormData = new FormData(),
      xhr: XMLHttpRequest = new XMLHttpRequest();

    for (let i = 0; i < files.length; i++) {
      formData.append("uploads[]", files[i], files[i].name);
    }

    xhr.onreadystatechange = () => {
      if (xhr.readyState === 4) {
        if (xhr.status === 200) {
          if (options && options.onFinished) options.onFinished(JSON.parse(xhr.response));
        } else {
          // console.log("Upload Error");
          if (options && options.error) options.error();
        }
      }
    };

    xhr.upload.onprogress = (event) => {   
      var progress = Math.round(event.loaded / event.total * 100);
      console.log("onprogress", progress);

      if (options && options.onProgress) options.onProgress(progress);

    };

    xhr.open('POST', this.settingService.portal + `api/media/gallery-photo/${cateId}`, true);
    xhr.setRequestHeader("Authorization", this.authService.currentUser.token_type + " " + this.authService.currentUser.access_token);
    xhr.send(formData);
  }
 
}
