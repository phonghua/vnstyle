import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import {
  RouterModule,
  RouteReuseStrategy
} from '@angular/router';
import {
  routing, appRoutingProviders
} from './app.routing';


import { FileUploadModule } from 'ng2-file-upload';

import { AppComponent } from './app.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { AuthCallbackComponent } from './components/auth-callback/auth-callback.component';
import { AuthSilentRenewComponent } from './components/auth-silent-renew/auth-silent-renew.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';

import { AuthService } from './services/auth.service';
import { HttpService } from './services/http.service';
import { MarkupService } from './services/markup.service';
import { MarkupPromotionService } from './services/markup-promotion.service';
import { SettingsService } from './services/settings.service';
import { GeneralService } from './services/general.service';
import { MarkupCommentService } from './services/markup-comment.service';
import { ShopService } from './services/shop.service';
import { FlowerService } from './services/flower.service';
import { ArticleService } from './services/articles.service';


import { RIcheckDirective } from './components/shared/r-icheck/r-icheck.directive';
import { RIcheckComponent } from './components/shared/r-icheck/r-icheck.component';
import { FormSummaryErrorComponent } from './components/shared/form-summary-error/form-summary-error.component';

import { ToastModule } from 'ng2-toastr/ng2-toastr';
import { NgxPaginationModule } from 'ngx-pagination';
import { RatingModule } from 'ngx-rating';
import { Ng2Bs3ModalModule } from 'ng2-bs3-modal/ng2-bs3-modal';

import { SpinnerComponent } from './components/shared/spinner/spinner.component';
import { CurrencyEditorComponent } from './components/shared/currency-editor/currency-editor.component';
import { DecimalEditorComponent } from './components/shared/decimal-editor/decimal-editor.component';
import { LoginComponent } from './components/login/login.component';

import { AuthLayoutModule } from './containers/auth-layout-container/';
import { MainLayoutModule } from './containers/main-layout-container/main-layout-container.module';

import { SHARED_SERVICES } from './services/';
import { SpinnerDirective } from './directives/spinner.directive';
import { BlockUIModule } from 'ng-block-ui';



// Application wide
// const APP_PROVIDERS = [
//   ...SHARED_SERVICES,
//   { provide: RouteReuseStrategy, useClass: CustomReuseStrategy },
//   RouterService
// ];


@NgModule({
  declarations: [
    AppComponent,
    PageNotFoundComponent,
    // SiderBarComponent,
    // TopNavComponent,
    AuthCallbackComponent,
    DashboardComponent,
    AuthSilentRenewComponent,
    RIcheckDirective,
    RIcheckComponent,
    FormSummaryErrorComponent,
    SpinnerComponent,
    CurrencyEditorComponent,
    DecimalEditorComponent,
    LoginComponent,
    // SpinnerDirective
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpModule,
    routing,
    FileUploadModule,
    ToastModule.forRoot(),
    NgxPaginationModule,
    RatingModule,
    Ng2Bs3ModalModule,

    AuthLayoutModule,
    MainLayoutModule,
    BlockUIModule,
    
  ],
  exports: [
    AuthLayoutModule,
    MainLayoutModule,
    RouterModule
  ],
  providers: [
    ...SHARED_SERVICES,

    appRoutingProviders,
    MarkupService,
    HttpService,
    AuthService,
    SettingsService,
    GeneralService,
    ArticleService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
