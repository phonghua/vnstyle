import { Component, OnInit } from '@angular/core';
import { ArticleService } from './../../../../services';
@Component({
  selector: 'app-articles',
  templateUrl: './articles.component.html',
  styleUrls: ['./articles.component.css']
})
export class ArticlesComponent implements OnInit {

  

  private articleGrid = {
    loading: false,
    data: [],
    pagination: {}
  }

  constructor(private articleService: ArticleService) { }

  ngOnInit() {

    this.articleGrid.loading = true;
    this.articleService.getArticles().subscribe(data => {
      console.log("articles", data);

      this.articleGrid.loading = false;
      this.articleGrid.data = data;
    })
  }

}
