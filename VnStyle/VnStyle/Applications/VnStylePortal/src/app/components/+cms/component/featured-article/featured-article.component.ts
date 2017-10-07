import { Component, OnInit } from '@angular/core';
import { ArticleService } from '../../../../services';

@Component({
  selector: 'app-featured-article',
  templateUrl: './featured-article.component.html',
  styleUrls: ['./featured-article.component.css']
})
export class FeaturedArticleComponent implements OnInit {

  public featuredArticles = {
    data: [],
    loading: false
  };



  constructor(private articleService: ArticleService) { }

  ngOnInit() {
    this.loadFeaturedArticles();
  }

  loadFeaturedArticles() {
    this.featuredArticles.loading = true;
    this.articleService.getFeaturedArticles().subscribe((data) => {
      this.featuredArticles.data = data;
      this.featuredArticles.loading = false;
    });

    this.articleService.searchArticles("").subscribe(data => {
      this.allArticles = data;
    });
  }

  allArticles: any[] = [];
  private get suggestFeaturedArticles() {
    return this.allArticles.filter(p=> !this.featuredArticles.data.some(s => s.articleId == p.id));
  }

  private selectedArticle = null;

  public featuredArticleSelected(event) {
    if (event && event.id && event.id > 0) {
      this.articleService.addFeaturedArticles(event.id).subscribe(() => {
        this.selectedArticle = null;
        this.loadFeaturedArticles();
      });
    }
  }

  swapFeatureArticle(article1, article2) {
    this.articleService.swapFeatureArticle(article1, article2).subscribe(() => {
      this.loadFeaturedArticles();
    }, () => {

    })
  }

  removeFeaturedArticle(articleId) {
    this.articleService.removeFeaturedArticles(articleId).subscribe(() => {
      this.loadFeaturedArticles();
    }, () => {

    });
  }



}
