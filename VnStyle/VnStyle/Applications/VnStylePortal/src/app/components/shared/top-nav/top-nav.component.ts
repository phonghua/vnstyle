import { Component, OnInit } from '@angular/core';
import { AuthService } from './../../../services/auth.service';

@Component({
  selector: 'app-top-nav',
  templateUrl: './top-nav.component.html',
  styleUrls: ['./top-nav.component.css']
})
export class TopNavComponent implements OnInit {

  get loggedInUserName(){
    if(this.authService.loggedIn == false) return "Anonymous";
    return this.authService.currentUser.profile.name;
  }
  constructor(private authService : AuthService) { }

  ngOnInit() {
  }

  logout(event){
    event.preventDefault();
    this.authService.startSignoutMainWindow();
  }
}
