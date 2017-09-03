import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { ArticleService, LanguageService } from '../../../../services';
import 'rxjs/Rx';
import { Observable } from 'rxjs/Observable';
import { HttpService } from '../../../../services';

@Component({
  selector: 'app-article-new',
  templateUrl: './article-new.component.html',
  styleUrls: ['./article-new.component.css']
})
export class ArticleNewComponent implements OnInit {

  private pageState: any = {
    fetched: false,
    fetching: false,
    initialized: false,
    data: null
  };

  private languages: Array<any> = [];
  private article: any = {};
  private get selectedLanguage() {
    return this.languages.filter(p => p.selected)[0];
  }
  @ViewChild('fileInput') _elFileInput: ElementRef;

  constructor(private articleService: ArticleService, private languageService: LanguageService, private httpService: HttpService) { }

  ngOnInit() {
    Observable.forkJoin(this.languageService.getLanguages()).subscribe(res => {

      this.pageState.fetched = true;
      this.pageState.fetching = false;
      this.pageState.initialized = true;

      this.languages = res[0].map(p => {
        p.selected = p.isDefault;
        return p;
      });


      this.article.articleLanguages = this.languages.filter(p => p.isDefault).map(p => {
        return {
          languageId: p.id,
          content: "",
          metaTag: {}
        }
      });
    });
  }

  saveArticle() {
    this.articleService.createArticle(this.article).subscribe(data => { }, err => { });
  }

  browseFiles() {
    console.log("browseFiles");
    this._elFileInput.nativeElement.click();
  }


  fileOnChanged(event) {
    let files = this._elFileInput.nativeElement.files;
    this.httpService.postGeneralFile(files, {
      onProgress: (processEvent) => {
        console.log("onProgress", processEvent);
      },
      onFinished: (result) => {
        // this.imageUrl = result.Data.images[0].FileUrl;
        // this.imageId = result.Data.images[0].PhotoId;

        // this.propagateChange({
        //   imageId: this.imageId,
        //   imageUrl: this.imageUrl
        // });

      },
      error: () => { }
    });

    console.log("fileOnChanged", event, this._elFileInput, files);

  }
}
