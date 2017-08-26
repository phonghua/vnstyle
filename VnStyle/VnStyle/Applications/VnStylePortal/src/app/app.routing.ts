
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

    // { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
    // { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard] },
    // { path: 'markup', component: MarkupComponent, canActivate: [AuthGuard] },
    // { path: 'markup/promotions', component: MarkupPromotionComponent, canActivate: [AuthGuard] },
    // { path: 'markup/promotions/:id', component: MarkupPromotionDetailComponent, canActivate: [AuthGuard] },
    // { path: 'markup/new', component: MarkupNewComponent, canActivate: [AuthGuard] },
    // { path: 'markup/:id', component: MarkupDetailComponent, canActivate: [AuthGuard] },

    // { path: 'users', component: UsersComponent, canActivate: [AuthGuard] },
    // { path: 'fetch-markup', component: FetchMarkupComponent, canActivate: [AuthGuard] },

    {
        path: '', component: MainLayoutContainerComponent, canActivate: [AuthGuard],
        children: [
            { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
            {
                path: 'dashboard',
                component: DashboardComponent
                //loadChildren: './+user-management/user-management.module#UserManagementModule'
            },
            // {
            //     path: 'university',
            //     loadChildren: './+university/university.module#UniversityModule'
            // },
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