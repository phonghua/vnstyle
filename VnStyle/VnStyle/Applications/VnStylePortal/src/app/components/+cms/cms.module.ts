import { NgModule } from "@angular/core";
import { routes } from "./cms.routes";
import { RouterModule } from "@angular/router";
import { CommonModule } from "@angular/common";
import { FroalaEditorModule, FroalaViewModule } from "angular-froala-wysiwyg";


import { CmsComponent } from "./cms.component";
import { ArticlesComponent } from "./component/articles/articles.component";
import { ArticleNewComponent } from "./component/article-new/article-new.component";
import { ArticleDetailComponent } from "./component/article-detail/article-detail.component";
import { ComposeArticleComponent } from "./component/shared/compose-article/compose-article.component";
import { ComposeArticleLanguageComponent } from "./component/shared/compose-article-language/compose-article-language.component";
import { SeoMetaComponent } from "./component/shared/seo-meta/seo-meta.component";
import { CategoriesComponent } from "./component/categories/categories.component";
import { GalleryPhotoComponent } from './component/gallery-photo/gallery-photo.component';
import { GalleryPhotoNewComponent } from './component/gallery-photo-new/gallery-photo-new.component';

import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { AdatePipe } from "./../../pipes/adate.pipe";
import { PictureFormControlComponent } from '../../components/shared/picture-form-control/picture-form-control.component';
import { RIcheckComponent } from '../../components/shared/r-icheck/r-icheck.component';

import { ConfirmModalComponent } from '../../components/shared/confirm-modal/confirm-modal.component';
import { Ng2Bs3ModalModule } from 'ng2-bs3-modal/ng2-bs3-modal';
import { Typeahead } from './../shared/typehead/typehead.component';

@NgModule({
  imports: [
    // SharedCommonModule,
    // SHARED_DIRECTIVE_MODULES,
    RouterModule.forChild(routes),
    CommonModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    FroalaEditorModule.forRoot(),
    FroalaViewModule.forRoot(),
    Ng2Bs3ModalModule

  ],
  declarations: [
    CmsComponent,
    ArticlesComponent,
    ArticleNewComponent,
    ArticleDetailComponent,
    ComposeArticleComponent,
    ComposeArticleLanguageComponent,
    SeoMetaComponent,
    AdatePipe,
    CategoriesComponent,
    PictureFormControlComponent,
    RIcheckComponent,
    ConfirmModalComponent,
    Typeahead,
    GalleryPhotoComponent,
    GalleryPhotoNewComponent
  ],
  entryComponents: []
})
export class CmsModule { }
