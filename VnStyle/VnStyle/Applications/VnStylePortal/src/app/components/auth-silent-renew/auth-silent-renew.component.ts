import { Component, OnInit } from '@angular/core';
import { AuthService } from './../../services/auth.service';

@Component({
  selector: 'app-auth-silent-renew',
  templateUrl: './auth-silent-renew.component.html',
  styleUrls: ['./auth-silent-renew.component.css']
})
export class AuthSilentRenewComponent implements OnInit {

  constructor(private authService: AuthService) { }

  ngOnInit() {
    // console.log("start silent renew");
    // this.authService.mgr.signinSilentCallback();
    // console.log("end silent renew");
  }

}
