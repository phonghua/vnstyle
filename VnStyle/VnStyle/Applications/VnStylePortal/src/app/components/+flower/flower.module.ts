import { NgModule } from '@angular/core';
import { routes } from './flower.routes';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';


import { FlowerComponent } from './flower.component';
import { FlowerListingComponent } from './component/flower-listing/flower-listing.component';

@NgModule({
    imports: [
        // SharedCommonModule,
        // SHARED_DIRECTIVE_MODULES,
        RouterModule.forChild(routes),
        CommonModule,
        RouterModule
    ],
    declarations: [
        FlowerComponent,
        FlowerListingComponent,       
    ],
    entryComponents: [

    ]
})
export class FlowerModule { }
