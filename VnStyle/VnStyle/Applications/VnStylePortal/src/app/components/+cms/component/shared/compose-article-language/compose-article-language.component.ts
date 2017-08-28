import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-compose-article-language',
  templateUrl: './compose-article-language.component.html',
  styleUrls: ['./compose-article-language.component.css']
})
export class ComposeArticleLanguageComponent implements OnInit {
  @Input() public language: any = null;


  private articleLanguageValue = {
    content : ""
  };

  private editorOptions = {};

  @Input()
  get articleLanguage() {
    return this.articleLanguageValue;
  }

  set articleLanguage(val) {
    this.articleLanguageValue = val;
  }

  constructor() { }

  ngOnInit() {
  }

 

}
