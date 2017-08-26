import { Injectable, EventEmitter } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { Observable } from 'rxjs/Rx';

import { UserManager, Log, MetadataService, User } from 'oidc-client';
import { environment } from '../../environments/environment';
import { Router } from '@angular/router';

@Injectable()
export class AuthService {
  
  userLoadededEvent: EventEmitter<User> = new EventEmitter<User>();

  mgr: UserManager = new UserManager(settings);
  currentUser: User;
  loggedIn: boolean = false;

  authHeaders: Headers;


  constructor(private http: Http, private router: Router) {
    this.mgr.getUser()
      .then((user) => {
        if (user) {
          this.loggedIn = true;
          this.currentUser = user;
          this.userLoadededEvent.emit(user);
        }
        else {
          this.loggedIn = false;
        }
      })
      .catch((err) => {
        this.loggedIn = false;
      });
    this.mgr.events.addUserUnloaded((e) => {
      if (!environment.production) {
        console.log("user unloaded");
      }
      this.loggedIn = false;
    });
  }
  clearState() {
    this.mgr.clearStaleState().then(function () {
      console.log("clearStateState success");
    }).catch(function (e) {
      console.log("clearStateState error", e.message);
    });
  }

  getUser() {
    this.mgr.getUser().then((user) => {
      console.log("got user", user);
      this.userLoadededEvent.emit(user);
    }).catch(function (err) {
      console.log(err);
    });
  }

  removeUser() {
    this.mgr.removeUser().then(() => {
      this.userLoadededEvent.emit(null);
      console.log("user removed");
    }).catch(function (err) {
      console.log(err);
    });
  }

  startSigninMainWindow() {
    // this.mgr.signinRedirect({ data: 'some data' }).then(function () {
    //   console.log("signinRedirect done");
    // }).catch(function (err) {
    //   console.log(err);
    // });

    this.router.navigate(["/login"]);

  }
  endSigninMainWindow(callback?) {
    this.mgr.signinRedirectCallback().then((user) => {
      console.log("signed in", user);
      if (callback && typeof (callback) == 'function') callback(user);
    }).catch(function (err) {
      console.log(err);
    });
  }

  startSignoutMainWindow() {
    this.mgr.signoutRedirect().then(function (resp) {
      console.log("signed out", resp);
      setTimeout(5000, () => {
        console.log("testing to see if fired...");

      })
    }).catch(function (err) {
      console.log(err);
    });
  };

  endSignoutMainWindow() {
    this.mgr.signoutRedirectCallback().then(function (resp) {
      console.log("signed out", resp);
    }).catch(function (err) {
      console.log(err);
    });
  };


  login(userName, password): Observable<any> {
    var headers = new Headers();
    headers.append('Content-Type', 'application/x-www-form-urlencoded');
    headers.append('Accept', 'application/json');
    const body = `username=${userName}&password=${password}&grant_type=password&client_id=${settings.client_id}&client_secret=${settings.client_secret}`;

    return this.http.post(settings.authority, body, { headers: headers })
      .map(res => {
        var data = res.json();
        // this.token = data.access_token;
        // localStorage.setItem('id_token', data.access_token);
        // this.loadTenants();
      })
      .catch(err => {
        return Observable.throw(err.json());
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

  authority: 'http://localhost:51050/oauth/token',
  client_id: 'efae79c9c6c84623907f57ad32bff42a',
  client_secret: 'a7257fd3e64b418db230f475e8efe0c8'

};
