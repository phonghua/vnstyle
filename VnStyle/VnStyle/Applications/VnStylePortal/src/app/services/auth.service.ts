import { Injectable, EventEmitter } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { Observable } from 'rxjs/Rx';

import { environment } from '../../environments/environment';
import { Router } from '@angular/router';

@Injectable()
export class AuthService {

  userLoadededEvent: EventEmitter<any> = new EventEmitter<any>();
  loggedInEvent: EventEmitter<any> = new EventEmitter<any>();


  // private _currentUser: {
  //   profile: {
  //     name: ""
  //   },
  //   // id_token: string,
  //   // session_state: any,
  //   access_token: string,
  //   token_type: string,
  //   scope: string,
  //   expires_at: number;
  //   state: any;
  //   expires_in: number;
  //   expired: boolean;
  //   scopes: string[];
  // };

  get currentUser(): any {
    //return this._currentUser;
    var userItem = JSON.parse(localStorage.getItem("rickoauthtoken"));
    if(userItem != null){
      userItem.profile = {
        name : userItem.userName
      };
    }
    return userItem;
  }

  get loggedIn(): boolean {
    //return (localStorage.getItem("rickoauthtoken") === null) ? false : true;

    return this.currentUser != null;
  }
  authHeaders: Headers;


  constructor(private http: Http, private router: Router) {
    this.loggedInEvent.subscribe((oauth) => {

      // this._currentUser.profile.name = oauth.userName;
      // this._currentUser.access_token = oauth.access_token;
      // this._currentUser.token_type = oauth.token_type;
      localStorage.setItem('rickoauthtoken', JSON.stringify(oauth));
      this.router.navigate(["/"]);
    });
  }


  startSigninMainWindow() {
    this.router.navigate(["/auth/login"]);
  }

  endSigninMainWindow() {

  }

  startSignoutMainWindow() {
    localStorage.removeItem("rickoauthtoken");
    this.router.navigate(["/"]);
  }


  login(userName, password): Observable<any> {
    var headers = new Headers();
    headers.append('Content-Type', 'application/x-www-form-urlencoded');
    headers.append('Accept', 'application/json');
    const body = `username=${userName}&password=${password}&grant_type=password&client_id=${settings.client_id}&client_secret=${settings.client_secret}`;
    return this.http.post(settings.authority, body, { headers: headers })
      .map(res => {
        var data = res.json();
        // this.currentUser.profile.name = data.userName;
        // this.currentUser.access_token = data.access_token;
        // this.currentUser.token_type = data.token_type;

        this.loggedInEvent.emit(data);
      })
      .catch(err => {
        return Observable.throw(err);
      });
  }
}

const settings: any = {
  // authority: 'https://localhost:44332/identity',
  // client_id: 'web_portal_admin',
  // // redirect_uri: 'http://localhost:4202/auth/callback',
  // // post_logout_redirect_uri: 'http://localhost:4202',

  // redirect_uri: 'http://localhost:4202/auth/callback',
  // post_logout_redirect_uri: 'http://localhost:4202',
  // response_type: 'id_token token',
  // scope: 'openid profile read write ricky_web_api', //read+write+openid+email+profile

  // // silent_redirect_uri: 'http://localhost:4202/auth/silent-renew',
  // silent_redirect_uri: 'http://localhost:4202/auth/silent-renew',
  // automaticSilentRenew: true,
  // //silentRequestTimeout:10000,

  // filterProtocolClaims: true,
  // loadUserInfo: true

  authority: 'http://localhost:55555/oauth/token',
  client_id: '1979D284-55DA-41A0-A13E-2D3778B83128',
  client_secret: '6217BEBB-0043-4C3E-A29D-1F25D0C490AF'

};
