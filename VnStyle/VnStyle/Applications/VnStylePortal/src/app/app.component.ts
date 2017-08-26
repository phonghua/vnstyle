import { Component, OnInit, ViewChild } from '@angular/core';
import { RouterOutlet } from '@angular/router';

import { AuthService } from './services/auth.service';
import { AuthCallbackComponent } from './components/auth-callback/auth-callback.component';
import { IComponent, Breadcrumb, Action } from "app/interfaces/component.interface";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  _user: any;
  loadedUserSub: any;
  private breadcrumbs: Breadcrumb[] = [];
  private actions: Action[] = [];

  @ViewChild(RouterOutlet) routerOutlet: RouterOutlet;

  constructor(private authService: AuthService) {

  }
  ngOnInit(): void {
    console.log("ngOnInit");

    // this.loadedUserSub = this.authService.userLoadededEvent
    //   .subscribe(user => {
    //     console.log("loadedUserSub", user);
    //     this._user = user;
    //     this.authService.loggedIn = true;

    //     console.log("current user", this.authService.currentUser);
    //   });
  }





  onActivate(event) {}

  private isIComponent(arg: any): arg is IComponent {
    //return arg.getBreadcrumb !== undefined;
    return false;
  }

  private callAction($event, action) {
    $event.preventDefault();
    if (action.func && typeof (action.func) == 'function') action.func();
    
    //this.routerOutlet.component[action.func()]();
  }

  onDeactivate(event) {

  }
}
