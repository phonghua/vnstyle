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

  private breadcrumbs: Breadcrumb[] = [];
  private actions: Action[] = [];
  private loading: boolean = true;

  @ViewChild(RouterOutlet) routerOutlet: RouterOutlet;


  constructor(private authService: AuthService) {

  }

  ngOnInit(): void {
  }

  onActivate(event) { }



  onDeactivate(event) {

  }
}
