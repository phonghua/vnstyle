import { NgModule } from '@angular/core';
import { TopNavComponent } from '../../components/shared/top-nav/top-nav.component';
import { SiderBarComponent } from '../../components/shared/sider-bar/sider-bar.component';
import { Routes, RouterModule } from '@angular/router';

import { CommonModule } from '@angular/common';  
import { BrowserModule } from '@angular/platform-browser';
// import {
//   SharedCommonModule
// } from '../../common';

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
  ],
  providers: [],
  entryComponents: [],
})
export class MainLayoutModule {
}
