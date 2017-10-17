import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { Observable } from 'rxjs/Observable';
import { SettingsService } from './settings.service';


import 'rxjs/Rx';
@Injectable()
export class ArticleService {

  constructor(private httpService: HttpService, private settingService: SettingsService) { }

  getArticles(rootCateId?): Observable<any> {
    return this.httpService.get(this.settingService.portal + `api/articles?rootCateId=${rootCateId}`)
      .map(res => res.json())
      .catch(err => Observable.throw(err));
  }

  createArticle(article): Observable<any> {
    return this.httpService.post(this.settingService.portal + 'api/articles', article)
      .map(res => res.json())
      .catch(err => Observable.throw(err));
  }

  updateArticle(article): Observable<any> {
    return this.httpService.post(this.settingService.portal + 'api/articles/update', article)
      .catch(err => Observable.throw(err));
  }

  getArticleById(articleId): Observable<any> {
    return this.httpService.get(this.settingService.portal + `api/articles/${articleId}`)
      .map(res => res.json()).catch(err => Observable.throw(err));
  }

  deleteArticle(articleId): Observable<any> {
    return this.httpService.post(this.settingService.portal + `api/articles/${articleId}/delete`).catch(err => Observable.throw(err));
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
    return this.httpService.post(this.settingService.portal + `api/articles/${articleId}/related/${relatedArticleId}/update`)
      .catch(err => Observable.throw(err));
  }

  deleteRelatedArticle(articleId, relatedArticleId): Observable<any> {
    return this.httpService.post(this.settingService.portal + `api/articles/${articleId}/related/${relatedArticleId}/delete`)
      .catch(err => Observable.throw(err));
  }

  swapRelatedArticle(articleId, relatedArticleId1, relatedArticleId2){
    return this.httpService.post(this.settingService.portal + `api/articles/${articleId}/related/${relatedArticleId1}/swap/${relatedArticleId2}/update`)
    .catch(err => Observable.throw(err));
  }

  // 
  getFeaturedArticles(): Observable<any> {
    return this.httpService.get(this.settingService.portal + `api/articles/featured`)
      .map(res => res.json()).catch(err => Observable.throw(err));
  }

  addFeaturedArticles(articleId): Observable<any> {
    return this.httpService.post(this.settingService.portal + `api/articles/featured/${articleId}/update`)
    .catch(err => Observable.throw(err));
  }

  removeFeaturedArticles(articleId): Observable<any> {
    return this.httpService.post(this.settingService.portal + `api/articles/featured/${articleId}/delete`)
    .catch(err => Observable.throw(err));
  }

  swapFeatureArticle(articleId, articleId2){
    return this.httpService.post(this.settingService.portal + `api/articles/featured/${articleId}/swap/${articleId2}/update`)
      .catch(err => Observable.throw(err));
  }
}
