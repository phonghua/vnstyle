import { Component, OnInit } from '@angular/core';
import { AuthService } from './../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-auth-callback',
  templateUrl: './auth-callback.component.html',
  styleUrls: ['./auth-callback.component.css']
})
export class AuthCallbackComponent implements OnInit {

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit() {
    // this.authService.endSigninMainWindow((user) => {
    //   var uri = sessionStorage.getItem("currentUri");
    //   console.log("endSigninMainWindow", user, this.authService.loggedIn, this.authService.currentUser, uri);
    //   this.router.navigate(["/"]);
    // });
    // var uri = sessionStorage.getItem("currentUri");
    // console.log("callback", uri);
    // if (uri && uri.length > 0) this.router.navigateByUrl(uri);
    // else this.router.navigateByUrl("/");

  }

}
