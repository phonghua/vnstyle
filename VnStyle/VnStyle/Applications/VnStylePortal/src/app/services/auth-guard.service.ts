import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';

import { AuthService } from './auth.service';

@Injectable()
export class AuthGuard implements CanActivate {

    constructor(private authService: AuthService, private router: Router) { }
    canActivate() {
        console.log("AuthGuard");
        if (this.authService.loggedIn) { return true; }
        
        if (!this.router.url.indexOf("auth/callback")) {
            sessionStorage.setItem("currentUri", this.router.url);            
        }
        this.authService.startSigninMainWindow();

    }

}
