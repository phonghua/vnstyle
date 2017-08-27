
import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';

import { AuthService } from './services/auth.service';
import { AuthGuard } from './services/auth-guard.service';

import { AuthCallbackComponent } from './components/auth-callback/auth-callback.component';
import { AuthSilentRenewComponent } from './components/auth-silent-renew/auth-silent-renew.component';

import { DashboardComponent } from './components/dashboard/dashboard.component';
import { LoginComponent } from './components/login/login.component';

import { AuthLayoutComponent } from './containers/auth-layout-container/auth-layout-container.component';
import { MainLayoutContainerComponent } from './containers/main-layout-container/main-layout-container.component';

const routes: Routes = [

    { path: 'auth/callback', component: AuthCallbackComponent },
    { path: 'auth/silent-renew', component: AuthSilentRenewComponent },
    {
        path: '', component: MainLayoutContainerComponent, canActivate: [AuthGuard],
        children: [
            { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
            {
                path: 'dashboard',
                component: DashboardComponent
            },
            {
                path: 'shop',
                loadChildren: './components/+shop/shop.module#ShopModule'
            },
            {
                path: 'flower',
                loadChildren: './components/+flower/flower.module#FlowerModule'
            },
            {
                path: 'markup',
                loadChildren: './components/+markup/markup.module#MarkupModule'
            },
            {
                path: 'cms',
                loadChildren: './components/+cms/cms.module#CmsModule'
            },
        ]
    },

    {
        path: 'auth', component: AuthLayoutComponent,
        children: [
            { path: '', redirectTo: 'login', pathMatch: 'full' },
            {
                path: 'login', component: LoginComponent,
                data: {
                    pageTitle: 'Đăng Nhập'
                }
            },
            //{ path: 'forgot-password', loadChildren: './+forgot-password/forgot-password.module#ForgotPasswordModule' }
        ]
    },

    { path: '404', component: PageNotFoundComponent },
    { path: '**', component: PageNotFoundComponent }
];

export const appRoutingProviders: any[] = [
    AuthService, AuthGuard
];

export const routing: ModuleWithProviders = RouterModule.forRoot(routes, { useHash: false });