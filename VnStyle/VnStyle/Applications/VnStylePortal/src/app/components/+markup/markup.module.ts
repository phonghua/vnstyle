import { NgModule } from '@angular/core';
import { routes } from './markup.routes';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MarkupComponent } from './markup.component';
import { MarkupListingComponent } from './component/markup-listing/markup-listing.component';
import { MarkupDetailComponent } from './component/markup-detail/markup-detail.component';
import { MarkupFetchingComponent } from './component/markup-fetching/markup-fetching.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { GalleryComponent } from './component/gallery/gallery.component';
import { SlideComponent } from './component/slide/slide.component';

@NgModule({
    imports: [
        // SharedCommonModule,
        // SHARED_DIRECTIVE_MODULES,
        RouterModule.forChild(routes),
        CommonModule,
        RouterModule,
        FormsModule,
        ReactiveFormsModule,
    ],
    declarations: [
        MarkupComponent,
        MarkupListingComponent,
        MarkupDetailComponent,
        MarkupFetchingComponent,
        GalleryComponent,
        SlideComponent
    ],
    entryComponents: [

    ]
})
export class MarkupModule { }
