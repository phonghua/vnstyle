import { Component, OnInit } from '@angular/core';
import { ArticleService, LanguageService } from '../../../../services';
import 'rxjs/Rx';
import { Observable } from 'rxjs/Observable';

@Component({
  selector: 'app-article-new',
  templateUrl: './article-new.component.html',
  styleUrls: ['./article-new.component.css']
})
export class ArticleNewComponent implements OnInit {

  private pageState: any = {
    fetched: false,
    fetching: false,
    initialized: false,
    data: null
  };

  private languages: Array<any> = [];
  private article: any = {};


  constructor(private articleService: ArticleService, private languageService: LanguageService) { }

  ngOnInit() {
    Observable.forkJoin(this.languageService.getLanguages()).subscribe(res => {
      console.log("res", res);
      this.languages = res[0];

      this.article.articleLanguages = this.languages.map(p=> { return {
        languageId : p.id,
        content : "default content"
      }});
    });
  }

}
