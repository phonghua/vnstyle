import { Component, OnInit, ViewChild } from '@angular/core';
import { RouterOutlet } from '@angular/router';

import { AuthService } from './../../services/auth.service';
import { IComponent, Breadcrumb, Action } from "app/interfaces/component.interface";
import { Observable } from 'rxjs/Observable';
import { LanguageService, CategoryService, AppService, ArtistService } from '../../services';

@Component({
  selector: 'app-main-layout-container',
  templateUrl: './main-layout-container.component.html',
  styleUrls: ['./main-layout-container.component.css']
})
export class MainLayoutContainerComponent implements OnInit {

  private breadcrumbs: Breadcrumb[] = [];
  private actions: Action[] = [];
  private loading: boolean = true;

  @ViewChild(RouterOutlet) routerOutlet: RouterOutlet;
  constructor(private languageService: LanguageService, private appService: AppService, private categoryService: CategoryService,
  private artistService : ArtistService) { }

  ngOnInit() {
    Observable.forkJoin([
      this.languageService.getLanguages(),
      this.categoryService.getArticleCategories(),
      // this.categoryService.getGalleryPhotoCategories(),
      this.artistService.getArtists()
    ], ).subscribe(resp => {
      this.loading = false;
      this.appService.raiseAppInitlizedEvent({
        languages: resp[0],
        articleCategories: resp[1],
        // galleryPhotoCategories: resp[2],
        artists : resp[2]
      });

    });
  }
  onActivate(event) {

    // Load breadcrumb
    // Load actions

    this.breadcrumbs = [new Breadcrumb("Home", "/")];
    this.actions = [];
    if (this.routerOutlet != undefined && this.routerOutlet.isActivated && this.routerOutlet.component != null && this.isIComponent(this.routerOutlet.component)) {
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


  }

  onDeactivate(event) {

  }
}
