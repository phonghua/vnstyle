import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { ArticleService, LanguageService } from '../../../../services';
import 'rxjs/Rx';
import { Observable } from 'rxjs/Observable';
import { HttpService } from '../../../../services';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-article-new',
  templateUrl: './article-new.component.html',
  styleUrls: ['./article-new.component.css']
})
export class ArticleNewComponent implements OnInit {

  public pageState: any = {
    fetched: false,
    fetching: false,
    initialized: false,
    data: null
  };
  public rootCateId;
  private cateName;
  public languages: Array<any> = [];
  public article: any = {};
  public get selectedLanguage() {
    return this.languages.filter(p => p.selected)[0];
  }

  constructor(private articleService: ArticleService, private languageService: LanguageService, private httpService: HttpService, private router: Router,
    private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.rootCateId = params["rootCateId"];
      this.cateName = params["rootCateName"];
      this.initializePage();
    });
  }

  initializePage() {
    Observable.forkJoin(this.languageService.getLanguages()).subscribe(res => {

      this.pageState.fetched = true;
      this.pageState.fetching = false;
      this.pageState.initialized = true;

      this.languages = res[0].map(p => {
        p.selected = p.isDefault;
        return p;
      });

      this.article.rootCate = this.rootCateId;

      this.article.articleLanguages = this.languages.filter(p => p.isDefault).map(p => {
        return {
          languageId: p.id,
          content: "",
          metaTag: {}
        }
      });
    });
  }

  saveArticle() {
    if (this.article.featureImage) {
      this.article.featureImageId = this.article.featureImage.imageId;
    }
    this.articleService.createArticle(this.article).subscribe(data => {
      this.router.navigate(["cms", this.rootCateId, this.cateName, "articles", data.id]);
      //<td><a [routerLink]="['/cms/' + rootCateId +'/'+ rootCateName +'/articles/' + article.id]">{{article.headLine}}</a></td>
    }, err => { });
  }


}
