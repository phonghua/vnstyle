import { Component, OnInit } from '@angular/core';
import { ArticleService, LanguageService } from '../../../../services';
import 'rxjs/Rx';
import { Observable } from 'rxjs/Observable';

@Component({
  selector: 'app-article-detail',
  templateUrl: './article-detail.component.html',
  styleUrls: ['./article-detail.component.css']
})
export class ArticleDetailComponent implements OnInit {

  constructor(private articleService: ArticleService, private languageService: LanguageService) { }

  ngOnInit() {
    Observable.forkJoin(this.languageService.getLanguages()).subscribe(res => {
      console.log("res", res);
    });
  }

}
