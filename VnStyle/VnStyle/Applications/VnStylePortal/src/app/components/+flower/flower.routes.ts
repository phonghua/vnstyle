import { Route } from '@angular/router';
import { FlowerComponent } from './flower.component';
import { FlowerListingComponent } from './component/flower-listing/flower-listing.component';


export const routes: Route[] = [
    {
        path: '', component: FlowerComponent,
        children: [
            {
                path: '', pathMatch: 'full', redirectTo: 'listing'
            },
            {
                path: 'listing', component: FlowerListingComponent
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
