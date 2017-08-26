import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// import {
//   SharedCommonModule
// } from '../../common';

import {
  AuthLayoutComponent
} from './auth-layout-container.component';

@NgModule({
  imports: [
    //SharedCommonModule
    RouterModule,
  ],
  exports: [
    AuthLayoutComponent
  ],
  declarations: [
    AuthLayoutComponent
  ],
  providers: [],
  entryComponents: [],
})
export class AuthLayoutModule {
}
