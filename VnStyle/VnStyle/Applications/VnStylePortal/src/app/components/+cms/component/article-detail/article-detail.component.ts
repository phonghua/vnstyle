import { Component, OnInit, ViewChild } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ArticleService, LanguageService } from '../../../../services';
import 'rxjs/Rx';
import { Observable } from 'rxjs/Observable';
import { ConfirmModalComponent } from '../../../shared/confirm-modal/confirm-modal.component';

@Component({
  selector: 'app-article-detail',
  templateUrl: './article-detail.component.html',
  styleUrls: ['./article-detail.component.css']
})
export class ArticleDetailComponent implements OnInit {

  private selectedLanguage = null;

  private article = {
    id: 0,
    articleLanguages: [],
    featureImage: null,
    featureImageId: null
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


  @ViewChild('confirmModal') confirmModal: ConfirmModalComponent;

  constructor(private articleService: ArticleService, private languageService: LanguageService,
    private route: ActivatedRoute) { }





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
    this.confirmModal.open();
    this.confirmModal.ok = () => {
      alert(this.article.id);
    }
  }

  editArticle() {
    this.editArticleState.originalModel = JSON.parse(JSON.stringify(this.article));
    this.editArticleState.editing = true;
  }

  saveArticle() {
    if (this.article.featureImage) {
      this.article.featureImageId = this.article.featureImage.imageId;
    }
    this.articleService.updateArticle(this.article).subscribe(data => {

    }, err => {

    });
  }

  cancelEdit() {
    this.article = Object.assign({}, this.editArticleState.originalModel);
    this.editArticleState.editing = false;
  }
}
