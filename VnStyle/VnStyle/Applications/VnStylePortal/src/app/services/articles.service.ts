import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { Observable } from 'rxjs/Observable';
import { SettingsService } from './settings.service';


import 'rxjs/Rx';
@Injectable()
export class ArticleService {

  constructor(private httpService: HttpService, private settingService: SettingsService) { }

  getArticles(): Observable<any> {
    return this.httpService.get(this.settingService.portal + `api/articles`)
      .map(res => res.json())
      .catch(err => Observable.throw(err));
  }

  createArticle(article): Observable<any> {
    return this.httpService.post(this.settingService.portal + 'api/articles', article)
      .catch(err => Observable.throw(err));
  }

  updateArticle(article): Observable<any> {
    return this.httpService.put(this.settingService.portal + 'api/articles', article)
      .catch(err => Observable.throw(err));
  }

  getArticleById(articleId): Observable<any> {
    return this.httpService.get(this.settingService.portal + `api/articles/${articleId}`)
      .map(res => res.json()).catch(err => Observable.throw(err));
  }

  deleteArticle(articleId): Observable<any> {
    return this.httpService.delete(this.settingService.portal + `api/articles/${articleId}`).catch(err => Observable.throw(err));
  }


  getRelatedArticles(articleId): Observable<any> {
    return this.httpService.get(this.settingService.portal + `api/articles/${articleId}/related`)
      .map(res => res.json()).catch(err => Observable.throw(err));
  }

  searchArticles(query): Observable<any> {
    return this.httpService.get(this.settingService.portal + `api/articles/search?query=`)
      .map(res => res.json()).catch(err => Observable.throw(err));
  }

  addRelatedArticle(articleId, relatedArticleId): Observable<any> {
    return this.httpService.put(this.settingService.portal + `api/articles/${articleId}/related/${relatedArticleId}`)
      .catch(err => Observable.throw(err));
  }

  deleteRelatedArticle(articleId, relatedArticleId): Observable<any> {
    return this.httpService.delete(this.settingService.portal + `api/articles/${articleId}/related/${relatedArticleId}`)
      .catch(err => Observable.throw(err));
  }

  //{id}/related/{relatedArticleId1}/swap/{relatedArticleId2}

  swapRelatedArticle(articleId, relatedArticleId1, relatedArticleId2){
    return this.httpService.put(this.settingService.portal + `api/articles/${articleId}/related/${relatedArticleId1}/swap/${relatedArticleId2}`)
    .catch(err => Observable.throw(err));
  }
}
