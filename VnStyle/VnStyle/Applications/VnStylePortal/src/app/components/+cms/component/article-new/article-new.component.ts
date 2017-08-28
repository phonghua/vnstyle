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
  private get selectedLanguage() {
    return this.languages.filter(p => p.selected)[0];
  }


  constructor(private articleService: ArticleService, private languageService: LanguageService) { }

  ngOnInit() {
    Observable.forkJoin(this.languageService.getLanguages()).subscribe(res => {

      this.pageState.fetched = true;
      this.pageState.fetching = false;
      this.pageState.initialized = true;

      this.languages = res[0].map(p => {
        p.selected = p.isDefault;
        return p;
      });


      this.article.articleLanguages = this.languages.map(p => {
        return {
          languageId: p.id,
          content: ""
        }
      });
    });
  }

}
