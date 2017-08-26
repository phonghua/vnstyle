import { Component, OnInit, ViewChild } from '@angular/core';
import { RouterOutlet } from '@angular/router';

import { AuthService } from './../../services/auth.service';
//import { AuthCallbackComponent } from './components/auth-callback/auth-callback.component';
import { IComponent, Breadcrumb, Action } from "app/interfaces/component.interface";

@Component({
  selector: 'app-main-layout-container',
  templateUrl: './main-layout-container.component.html',
  styleUrls: ['./main-layout-container.component.css']
})
export class MainLayoutContainerComponent implements OnInit {
_user: any;
  loadedUserSub: any;
  private breadcrumbs: Breadcrumb[] = [];
  private actions: Action[] = [];

  @ViewChild(RouterOutlet) routerOutlet: RouterOutlet;
  constructor() { }

  ngOnInit() {
  }
  onActivate(event) {

    // Load breadcrumb
    // Load actions

    this.breadcrumbs = [new Breadcrumb("Home", "/")];
    this.actions = [];
    if (this.routerOutlet != undefined && this.routerOutlet.isActivated && this.routerOutlet.component != null && this.isIComponent(this.routerOutlet.component)) {
      // this.menus = (this.el.component as IComponent).getActions();
      // this.title = (this.el.component as IComponent).getTitle();
      this.breadcrumbs = (this.routerOutlet.component as IComponent).getBreadcrumb();
      this.actions = (this.routerOutlet.component as IComponent).getActions();
    }
  }

  private isIComponent(arg: any): arg is IComponent {
    return arg.getBreadcrumb !== undefined;
  }

  private callAction($event, action) {
    $event.preventDefault();
    if (action.func && typeof (action.func) == 'function') action.func();
    
    //this.routerOutlet.component[action.func()]();
  }

  onDeactivate(event) {

  }
}
