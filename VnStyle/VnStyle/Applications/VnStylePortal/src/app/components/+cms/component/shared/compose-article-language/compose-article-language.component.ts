import { Component, OnInit, Input, Output, EventEmitter, ViewChild, ElementRef } from '@angular/core';
import { HttpService } from '../../../../../services';

@Component({
  selector: 'app-compose-article-language',
  templateUrl: './compose-article-language.component.html',
  styleUrls: ['./compose-article-language.component.css']
})
export class ComposeArticleLanguageComponent implements OnInit {
  @Input() public language: any = null;


  private articleLanguageValue = {
    content: ""
  };

  private editorOptions = {};

  @Input()
  get articleLanguage() {
    return this.articleLanguageValue;
  }

  set articleLanguage(val) {
    this.articleLanguageValue = val;
  }

  @ViewChild('fileInput') _elFileInput: ElementRef;

  constructor(private httpService: HttpService) { }

  ngOnInit() {
  }

  browseFiles() {
    console.log("browseFiles");
    this._elFileInput.nativeElement.click();
  }


  fileOnChanged(event) {
    let files = this._elFileInput.nativeElement.files;
    this.httpService.postGeneralFile( files, {
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
