import { Injectable, EventEmitter } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import { tokenNotExpired, JwtHelper, AuthHttp } from 'angular2-jwt';

import { environment } from '../../environments/environment';
import { Router } from '@angular/router';

@Injectable()
export class AuthService {

  userLoadededEvent: EventEmitter<any> = new EventEmitter<any>();
  loggedInEvent: EventEmitter<any> = new EventEmitter<any>();

  private checkTokenExpireTimeout: any;
  private helper = new JwtHelper();
  get currentUser(): any {
    //return this._currentUser;
    var userItem = JSON.parse(localStorage.getItem("rickoauthtoken"));
    if (userItem != null) {
      userItem.profile = {
        name: userItem.userName
      };
    }
    return userItem;
  }

  get loggedIn(): boolean {
    return this.currentUser != null;
  }
  authHeaders: Headers;


  constructor(private http: Http, private router: Router) {
    this.loggedInEvent.subscribe((oauth) => {

      if(this.loggedIn){
        if (this.checkTokenExpireTimeout) {
          clearTimeout(this.checkTokenExpireTimeout);
        }
        this.checkTokenExpireTimeout = setTimeout(() => {
          this.logout();
        }, this.currentUser.expires_in * 1000);
      }
      

      localStorage.setItem('rickoauthtoken', JSON.stringify(oauth));
      this.router.navigate(["/"]);
    });

    // if (this.currentUser != null && (new Date(this.currentUser['.expires'])).getTime() <= (new Date()).getTime()) {
    //   this.logout();
    // }


    if (this.loggedIn) {
      const expDate = (new Date(this.currentUser['.expires']));
      const currentDate = new Date();
      const timing = expDate.getTime() - currentDate.getTime();

      if (this.checkTokenExpireTimeout){
        clearTimeout(this.checkTokenExpireTimeout);
      } 
      this.checkTokenExpireTimeout = setTimeout(() => {
        this.logout();
      }, timing);
    }



  }

  logout() {
    this.startSignoutMainWindow();
    // this.token = null;
    // this.redirectUrl = '';
    // this.userLoggedOutEvent.emit({});
    // this.router.navigate(['login']);
  }

  startSigninMainWindow() {
    this.router.navigate(["/auth/login"]);
  }

  endSigninMainWindow() {

  }

  startSignoutMainWindow() {
    localStorage.removeItem("rickoauthtoken");
    this.router.navigate(["/auth/login"]);
  }


  login(userName, password): Observable<any> {
    var headers = new Headers();
    headers.append('Content-Type', 'application/x-www-form-urlencoded');
    headers.append('Accept', 'application/json');
    const body = `username=${userName}&password=${password}&grant_type=password&client_id=${settings.client_id}&client_secret=${settings.client_secret}`;
    return this.http.post(settings.authority, body, { headers: headers })
      .map(res => {
        var data = res.json();
        this.loggedInEvent.emit(data);
      })
      .catch(err => {
        return Observable.throw(err);
      });
  }
}

const settings: any = {
  authority: 'http://localhost:56847/oauth/token',
  client_id: 'fe3429036f404047865a48a5f8739c94',
  client_secret: '67b4b438cc37427792a2b1521f10cba4'

};
