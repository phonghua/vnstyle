import { Route } from '@angular/router';
import { CmsComponent } from './cms.component';
import { ArticlesComponent } from './component/articles/articles.component';


export const routes: Route[] = [
    {
        path: '', component: CmsComponent,
        children: [
            {
                path: '', pathMatch: 'full', redirectTo: 'articles'
            },
            {
                path: 'articles', component: ArticlesComponent
            },
        ]
    }
];
