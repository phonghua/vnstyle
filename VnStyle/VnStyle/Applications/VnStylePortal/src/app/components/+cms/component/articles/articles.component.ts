import { Component, OnInit } from '@angular/core';
import { ArticleService } from './../../../../services';
import { Route, ActivatedRoute } from '@angular/router'

@Component({
  selector: 'app-articles',
  templateUrl: './articles.component.html',
  styleUrls: ['./articles.component.css']
})
export class ArticlesComponent implements OnInit {


  private rootCateId = null;
  private rootCateName = null;
  private articleGrid = {
    loading: false,
    data: [],
    pagination: {}
  }

  constructor(private articleService: ArticleService, private route: ActivatedRoute) { }

  ngOnInit() {

    this.route.params.subscribe(params => {
      this.rootCateId = params["rootCateId"];
      this.rootCateName = params["rootCateName"];

      this.articleGrid.loading = true;
      this.articleService.getArticles(this.rootCateId).subscribe(data => {
        console.log("articles", data);

        this.articleGrid.loading = false;
        this.articleGrid.data = data;
      })
    });


  }

}
