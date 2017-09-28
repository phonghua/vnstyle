import { Component, OnInit , Input} from '@angular/core';

@Component({
  selector: 'app-seo-meta',
  templateUrl: './seo-meta.component.html',
  styleUrls: ['./seo-meta.component.css']
})
export class SeoMetaComponent implements OnInit {

  public seoMetaValue = {
    content : "",
    description : "",
    keywords : ""
  };

  public editorOptions = {};

  @Input()
  get seoMeta() {
    return this.seoMetaValue;
  }

  set seoMeta(val) {
    this.seoMetaValue = val;
  }

  constructor() { }

  ngOnInit() {
  }

}
