import { Route } from "@angular/router";
import { CmsComponent } from "./cms.component";
import { ArticlesComponent } from "./component/articles/articles.component";
import { ArticleNewComponent } from "./component/article-new/article-new.component";
import { ArticleDetailComponent } from "./component/article-detail/article-detail.component";
import { CategoriesComponent } from "./component/categories/categories.component";
import { GalleryPhotoComponent } from './component/gallery-photo/gallery-photo.component';
import { GalleryPhotoNewComponent } from './component/gallery-photo-new/gallery-photo-new.component';

export const routes: Route[] = [
  {
    path: "",
    component: CmsComponent,
    children: [
      // { path: "", pathMatch: "full", redirectTo: "articles" },
      //{ path: "articles", component: ArticlesComponent },
      { path: ":rootCateId/:rootCateName/articles", component: ArticlesComponent },
      { path: ":rootCateId/:rootCateName/articles/new", component: ArticleNewComponent },
      { path: ":rootCateId/:rootCateName/articles/:id", component: ArticleDetailComponent },
      { path: ":rootCateId/:rootCateName/gallery-photo", component: GalleryPhotoComponent },
      { path: ":rootCateId/:rootCateName/gallery-photo/new", component: GalleryPhotoNewComponent },
      { path: ":rootCateId/:rootCateName/categories", component: CategoriesComponent },

    ]
  }
];
