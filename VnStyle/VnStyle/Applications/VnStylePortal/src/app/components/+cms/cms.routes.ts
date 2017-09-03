import { Route } from "@angular/router";
import { CmsComponent } from "./cms.component";
import { ArticlesComponent } from "./component/articles/articles.component";
import { ArticleNewComponent } from "./component/article-new/article-new.component";
import { ArticleDetailComponent } from "./component/article-detail/article-detail.component";
import { CategoriesComponent } from "./component/categories/categories.component";
export const routes: Route[] = [
  {
    path: "",
    component: CmsComponent,
    children: [
      { path: "", pathMatch: "full", redirectTo: "articles" },
      { path: "articles", component: ArticlesComponent },
      { path: "articles/new", component: ArticleNewComponent },
      { path: "articles/:id", component: ArticleDetailComponent },
      { path: "1/categories", component: CategoriesComponent }
    ]
  }
];
