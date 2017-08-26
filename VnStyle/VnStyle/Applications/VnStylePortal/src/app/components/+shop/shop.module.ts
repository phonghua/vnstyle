import { NgModule } from '@angular/core';
import { routes } from './shop.routes';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';


import { ShopComponent } from './shop.component';
import { ShopListingComponent } from './component/shop-listing/shop-listing.component';

@NgModule({
    imports: [
        // SharedCommonModule,
        // SHARED_DIRECTIVE_MODULES,
        RouterModule.forChild(routes),
        CommonModule,
        RouterModule
    ],
    declarations: [
        ShopComponent,
        ShopListingComponent,

        
    ],
    entryComponents: [

    ]
})
export class ShopModule { }
