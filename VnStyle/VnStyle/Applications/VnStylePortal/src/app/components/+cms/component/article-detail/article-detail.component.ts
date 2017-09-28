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

  public selectedLanguage = null;

  public article = {
    id: 0,
    articleLanguages: [],
    featureImage: null,
    featureImageId: null
  };

  public count = 0;
  public editArticleState = {
    editing: false,
    originalModel: null
  }

  public releatedArticles = {
    data: []
  }

  public languages = [];
  public get selectedArticleLanguage() {
    return this.article.articleLanguages.filter(p => p.languageId == this.selectedLanguage)[0];
  }


  @ViewChild('confirmModal') confirmModal: ConfirmModalComponent;

  constructor(private articleService: ArticleService, private languageService: LanguageService,
    private route: ActivatedRoute, private router: Router) { }





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
      this.articleService.getArticleById(articleId),
      this.articleService.getRelatedArticles(articleId)
    ]).subscribe(res => {
      this.languages = res[0];
      this.article = res[1];
      this.selectedLanguage = this.languages.filter(p => p.isDefault)[0].id;
      this.releatedArticles.data = res[2];
    });

    this.articleService.searchArticles("").subscribe(data => {
      this.allArticles = data;
    });

  }

  deleteArticle() {
    this.confirmModal.open();
    this.confirmModal.ok = () => {
      this.articleService.deleteArticle(this.article.id).subscribe(() => {
        this.router.navigate(["cms", "articles"]);
      });
    }
  }

  editArticle() {
    this.editArticleState.originalModel = JSON.parse(JSON.stringify(this.article));
    this.editArticleState.editing = true;
  }

  refreshRelatedArticleGrid() {
    this.articleService.getRelatedArticles(this.article.id).subscribe(data => {
      this.releatedArticles.data = data;
    })
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



  allArticles: any[] = [];
  private get suggestRelatedArticles() {
    return this.allArticles.filter(p => p.id != this.article.id);
  }

  private selectedArticle = null;

  public relatedArticleSelected(event) {
    // console.log("relatedArticleSelected", article, this.selectedArticle);
    if (event && event.id && event.id > 0) {
      this.articleService.addRelatedArticle(this.article.id, event.id).subscribe(() => {
        this.selectedArticle = null;
        this.refreshRelatedArticleGrid();
      });
    }
  }

  public removeRelatedArticle(relatedArticleId) {
    this.articleService.deleteRelatedArticle(this.article.id, relatedArticleId).subscribe(() => {
      this.refreshRelatedArticleGrid();
    });
  }

  public swapRelatedArticle(relatedArticleId1, relatedArticleId2) {
    this.articleService.swapRelatedArticle(this.article.id, relatedArticleId1, relatedArticleId2).subscribe(() => {
      this.refreshRelatedArticleGrid();
    });
  }
}
