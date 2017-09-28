import { Component, OnInit } from '@angular/core';
import { AuthService } from './../../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  public loginModel: any = {
    userName: "",
    password: "",
  };

  constructor(private authService : AuthService) { }

  ngOnInit() {
  }

  submitForm() {
    console.log(this.loginModel);
    this.authService.login(this.loginModel.userName, this.loginModel.password).subscribe(data => {

    });

  }

}
