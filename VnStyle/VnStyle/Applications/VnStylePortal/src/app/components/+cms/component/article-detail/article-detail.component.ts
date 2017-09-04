import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ArticleService, LanguageService } from '../../../../services';
import 'rxjs/Rx';
import { Observable } from 'rxjs/Observable';

@Component({
  selector: 'app-article-detail',
  templateUrl: './article-detail.component.html',
  styleUrls: ['./article-detail.component.css']
})
export class ArticleDetailComponent implements OnInit {

  constructor(private articleService: ArticleService, private languageService: LanguageService,
    private route: ActivatedRoute) { }



  private selectedLanguage = null;

  private article = {
    articleLanguages: []
  };

  private count = 0;
  private editArticleState = {
    editing: false,
    originalModel: null
  }

  private languages = [];
  private get selectedArticleLanguage() {
    return this.article.articleLanguages.filter(p => p.languageId == this.selectedLanguage)[0];
  }

  ngOnInit() {
    console.log("on init")
    this.route.params.subscribe(params => {

      const articleId = params["id"];
      this.initializePage(articleId);
    });


  }


  initializePage(articleId) {
    Observable.forkJoin([
      this.languageService.getLanguages(),
      this.articleService.getArticleById(articleId)
    ]).subscribe(res => {
      this.languages = res[0];
      this.article = res[1];
      this.selectedLanguage = this.languages.filter(p => p.isDefault)[0].id;
    });
  }

  deleteArticle() {

  }

  editArticle() {
    this.editArticleState.originalModel = JSON.parse(JSON.stringify(this.article));
    this.editArticleState.editing = true;
  }

  saveArticle() {
    this.articleService.updateArticle(this.article).subscribe(data => {

    }, err => {

    });
  }

  cancelEdit() {
    this.article = Object.assign({}, this.editArticleState.originalModel);
    this.editArticleState.editing = false;
  }
}
