import { NgModule } from '@angular/core';
import { routes } from './cms.routes';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FroalaEditorModule, FroalaViewModule } from 'angular-froala-wysiwyg';


import { CmsComponent } from './cms.component';
import { ArticlesComponent } from './component/articles/articles.component';
import { ArticleNewComponent } from './component/article-new/article-new.component';
import { ArticleDetailComponent } from './component/article-detail/article-detail.component';
import { ComposeArticleComponent } from './component/shared/compose-article/compose-article.component';
import { ComposeArticleLanguageComponent } from './component/shared/compose-article-language/compose-article-language.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

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
        FroalaViewModule.forRoot()
    ],
    declarations: [
        CmsComponent,
        ArticlesComponent,
        ArticleNewComponent,
        ArticleDetailComponent,
        ComposeArticleComponent,
        ComposeArticleLanguageComponent
    ],
    entryComponents: [

    ]
})
export class CmsModule { }
