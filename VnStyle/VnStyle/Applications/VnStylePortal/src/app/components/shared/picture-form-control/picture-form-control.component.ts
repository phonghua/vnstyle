import { Component, OnInit, Input, ViewChild, ElementRef, forwardRef } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR, NG_VALIDATORS, FormControl, Validator, AbstractControl } from '@angular/forms';
//import { FileUploader } from 'ng2-file-upload';
import { SettingsService } from './../../../services/settings.service';
import { HttpService } from './../../../services/http.service';
import { Observable } from 'rxjs/Observable';
import 'rxjs/Rx';


@Component({
  selector: 'app-picture-form-control',
  templateUrl: './picture-form-control.component.html',
  styleUrls: ['./picture-form-control.component.css'],

  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => PictureFormControlComponent),
      multi: true,
    },
    {
      provide: NG_VALIDATORS,
      useExisting: forwardRef(() => PictureFormControlComponent),
      multi: true,
    }]
})
export class PictureFormControlComponent implements OnInit, ControlValueAccessor, Validator {

  imageId: any;
  imageUrl: any;
  @Input() readonly: any = true;
  //classStyle: string = "";

  @ViewChild('fileInput') _elFileInput: ElementRef;

  get displayedImageUrl() {
    if (this.imageUrl) return this.imageUrl;
    else return "https://vignette3.wikia.nocookie.net/lego/images/a/ac/No-Image-Basic.png/revision/latest?cb=20130819001030";
    //return this.imageUrl;
  }

  constructor(private settingService: SettingsService, private httpService: HttpService) { }


  ngOnInit() {

  }

  browerFiles() {
    if (!(this.readonly)) {
      this._elFileInput.nativeElement.click();
    }
  }

  fileOnChanged(event) {
    let files = this._elFileInput.nativeElement.files;
    this.httpService.postGeneralFile(files, {
      onProgress: (processEvent) => {
        console.log("onProgress", processEvent);
      },
      onFinished: (result) => {
        this.imageUrl = result.data.images[0].fileUrl;
        this.imageId = result.data.images[0].id;

        this.propagateChange({
          imageId: this.imageId,
          imageUrl: this.imageUrl
        });
      },
      error: () => {}
    });

    console.log("fileOnChanged", event, this._elFileInput, files);

  }


  writeValue(obj: any): void {
    console.log('writeValue', obj);
    if (obj) {
      this.imageId = obj.imageId;
      this.imageUrl = obj.imageUrl;
    }
    else {
      this.imageId = null;
      this.imageUrl = "";
    }
  }

  // registers 'fn' that will be fired wheb changes are made
  // this is how we emit the changes back to the form
  registerOnChange(fn: any): void {
    this.propagateChange = fn;
  }
  registerOnTouched(fn: any): void {

  }
  setDisabledState(isDisabled: boolean): void {
    //throw new Error('Method not implemented.');
  }

  // validates the form, returns null when valid else the validation object
  // in this case we're checking if the json parsing has passed or failed from the onChange method
  public validate(c: FormControl) {
    // return (!this.parseError) ? null : {
    //   jsonParseError: {
    //     valid: false,
    //   },
    // };

    return null;
  }

  registerOnValidatorChange(fn: () => void): void {
    //throw new Error('Method not implemented.');
  }

  private propagateChange = (_: any) => { };

  // change events from the textarea
  private onChange(event) {

    // // get value from text area
    // let newValue = event.target.value;

    // try {
    //     // parse it to json
    //     this.data = JSON.parse(newValue);
    //     this.parseError = false;
    // } catch (ex) {
    //     // set parse error if it fails
    //     this.parseError = true;
    // }

    // // update the form
    // this.propagateChange(this.data);
  }
}
