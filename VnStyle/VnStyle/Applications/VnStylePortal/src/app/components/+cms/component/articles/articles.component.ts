import { Component, OnInit } from '@angular/core';
import { ArticleService } from './../../../../services';
@Component({
  selector: 'app-articles',
  templateUrl: './articles.component.html',
  styleUrls: ['./articles.component.css']
})
export class ArticlesComponent implements OnInit {

  constructor(private articleService : ArticleService) { }

  ngOnInit() {
    this.articleService.getArticles().subscribe(data => {
      console.log("articles", data);
    })
  }

}
