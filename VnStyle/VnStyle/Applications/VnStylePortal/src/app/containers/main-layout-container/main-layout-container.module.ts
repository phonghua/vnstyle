import { NgModule } from '@angular/core';
import { TopNavComponent } from '../../components/shared/top-nav/top-nav.component';
import { SiderBarComponent } from '../../components/shared/sider-bar/sider-bar.component';
import { Routes, RouterModule } from '@angular/router';

import { CommonModule } from '@angular/common';  
import { BrowserModule } from '@angular/platform-browser';
import { SpinnerDirective } from '../../directives/spinner.directive';


import {
  MainLayoutContainerComponent
} from './main-layout-container.component';

@NgModule({
  imports: [
    //SharedCommonModule
    CommonModule,
    BrowserModule,
    RouterModule
  ],
  exports: [
    MainLayoutContainerComponent
  ],
  declarations: [
    MainLayoutContainerComponent,
    TopNavComponent,
    SiderBarComponent,
    SpinnerDirective
  ],
  providers: [],
  entryComponents: [],
})
export class MainLayoutModule {
}
