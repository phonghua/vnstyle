import { Route } from '@angular/router';
import { ShopComponent } from './shop.component';
import { ShopListingComponent } from './component/shop-listing/shop-listing.component';
// import { ListJobComponent } from './components/list-job/list-job.component';
// import { CreateJobComponent } from './components/create-job/create-job.component';
// import { UpdateJobComponent } from './components/update-job/update-job.component';


export const routes: Route[] = [
    {
        path: '', component: ShopComponent,
        children: [
            {
                path: '', pathMatch: 'full', redirectTo: 'listing'
            },
            {
                path: 'listing', component: ShopListingComponent
            },
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
