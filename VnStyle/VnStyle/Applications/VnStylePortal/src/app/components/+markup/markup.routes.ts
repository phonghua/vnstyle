import { Route } from '@angular/router';
import { MarkupComponent } from './markup.component';
//import { ShopListingComponent } from './component/shop-listing/shop-listing.component';
import { MarkupListingComponent } from './component/markup-listing/markup-listing.component';
import { MarkupDetailComponent } from './component/markup-detail/markup-detail.component';
import { MarkupFetchingComponent } from './component/markup-fetching/markup-fetching.component';



export const routes: Route[] = [
    {
        path: '', component: MarkupComponent,
        children: [
            {
                path: '', pathMatch: 'full', redirectTo: 'listing'
            },
            {
                path: 'listing', component: MarkupListingComponent,
            },
            {
                path: 'detail/:id', component: MarkupDetailComponent,
            },
            {
                path: 'fetching', component: MarkupFetchingComponent,
            }
            //   {
            //     path: 'create', component: CreateJobComponent
            //   },
            //   {
            //     path: ':id', component: UpdateJobComponent,
            //     data: {
            //       pageTitle: 'Cập Nhật Thông Tin Trường Đại Học'
            //     }
            //   }
        ]
    }
];
