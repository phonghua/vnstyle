import {
  Component,
  OnInit,
  Input,
  Output,
  EventEmitter,
  ViewChild,
  ElementRef
} from "@angular/core";
import { SettingsService, AuthService } from "../../../../../services";

@Component({
  selector: "app-compose-article-language",
  templateUrl: "./compose-article-language.component.html",
  styleUrls: ["./compose-article-language.component.css"]
})
export class ComposeArticleLanguageComponent implements OnInit {
  @Input() public language: any = null;

  public articleLanguageValue = {
    content: "",
    headLine: null,
    extract: null,
    metaTag: null
  };

  public editorOptions = {
    key: "",
    imageUploadURL: "",
    requestHeaders: null
  };

  @Input()
  public get articleLanguage() {
    return this.articleLanguageValue;
  }

  public set articleLanguage(val) {
    this.articleLanguageValue = val;
  }

  public initialized = false;

  constructor(
    private settingService: SettingsService,
    private authService: AuthService
  ) {
    this.editorOptions.key = this.settingService.froalaKey;
    this.editorOptions.imageUploadURL =
      this.settingService.portal + "api/media/editor-upload";

    this.editorOptions.requestHeaders = {
      Authorization: this.authService.currentUser.token_type + " " + this.authService.currentUser.access_token
    };

    this.initialized = true;
    console.log("...", this.editorOptions);
  }

  ngOnInit() {}
}
