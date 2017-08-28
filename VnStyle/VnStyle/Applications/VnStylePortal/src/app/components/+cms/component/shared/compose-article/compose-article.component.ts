import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-compose-article',
  templateUrl: './compose-article.component.html',
  styleUrls: ['./compose-article.component.css']
})
export class ComposeArticleComponent implements OnInit {

  @Input() public languages: Array<any> = [];
  @Input() private article = null;

  private tabId = "article-language-tab-";

  constructor() { }

  ngOnInit() {
  }

  getLanguageById(languageId){
    return this.languages.filter(p=> p.id == languageId)[0];
  }

}
