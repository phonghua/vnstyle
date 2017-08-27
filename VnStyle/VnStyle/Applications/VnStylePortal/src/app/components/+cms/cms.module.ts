import { NgModule } from '@angular/core';
import { routes } from './cms.routes';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';


import { CmsComponent } from './cms.component';
import { ArticlesComponent } from './component/articles/articles.component';

@NgModule({
    imports: [
        // SharedCommonModule,
        // SHARED_DIRECTIVE_MODULES,
        RouterModule.forChild(routes),
        CommonModule,
        RouterModule
    ],
    declarations: [
        CmsComponent,
        ArticlesComponent,
    ],
    entryComponents: [

    ]
})
export class CmsModule { }
