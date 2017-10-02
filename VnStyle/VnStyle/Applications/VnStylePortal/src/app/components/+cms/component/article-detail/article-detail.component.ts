import { Component, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ArticleService, LanguageService } from '../../../../services';
import 'rxjs/Rx';
import { Observable } from 'rxjs/Observable';
import { ConfirmModalComponent } from '../../../shared/confirm-modal/confirm-modal.component';
import { ToastsManager } from 'ng2-toastr/ng2-toastr';

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

  private articleId = 0;
  private cateName = "";
  private rootCateId = 0;

  constructor(private articleService: ArticleService, private languageService: LanguageService,
    private route: ActivatedRoute, private router: Router, public toastr: ToastsManager, vcr: ViewContainerRef) {
    this.toastr.setRootViewContainerRef(vcr);
  }


  ngOnInit() {
    this.route.params.subscribe(params => {

      this.articleId = params["id"];
      this.cateName = params["rootCateName"];
      this.rootCateId = params["rootCateId"];

      this.initializePage(this.articleId);
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
      this.editArticleState.editing = false;
    });

    this.articleService.searchArticles("").subscribe(data => {
      this.allArticles = data;
    });

  }

  deleteArticle() {
    this.confirmModal.message = "Bạn có chắc muốn xóa bài viết này không?";
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
      this.initializePage(this.articleId);
      this.toastr.success("Cập nhật bài viết thành công");
    }, err => {

    });
  }

  addEnglishArticle() {
    this.addArticleLanguage("en");
  }

  addArticleLanguage(languageId) {
    var firstLanguage = Object.assign({}, this.article.articleLanguages[0]);
    var additionalLanguage = Object.assign(firstLanguage, { languageId: languageId, id: 0 });
    this.article.articleLanguages.push(additionalLanguage);
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

  public switchLanguage(languageId) {
    this.selectedLanguage = languageId;
  }
}
