import { Component, OnInit, Input, Output, EventEmitter, ViewChild, ElementRef } from '@angular/core';


@Component({
  selector: 'app-compose-article-language',
  templateUrl: './compose-article-language.component.html',
  styleUrls: ['./compose-article-language.component.css']
})
export class ComposeArticleLanguageComponent implements OnInit {
  @Input() public language: any = null;


  public articleLanguageValue = {
    content: "",
    headLine : null,
    extract : null,
    metaTag : null
  };

  public editorOptions = {};

  @Input()
  public get articleLanguage() {
    return this.articleLanguageValue;
  }

  public set articleLanguage(val) {
    this.articleLanguageValue = val;
  }

 

  constructor() { }

  ngOnInit() {
  }

  



}
