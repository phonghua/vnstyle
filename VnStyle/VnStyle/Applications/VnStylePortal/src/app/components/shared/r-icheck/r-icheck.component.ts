import { Component, OnInit, Input, ViewChild, ElementRef, forwardRef } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR, NG_VALIDATORS, FormControl, Validator, AbstractControl } from '@angular/forms';
declare var $: any
import 'rxjs/Rx';

@Component({
  selector: 'app-r-icheck',
  templateUrl: './r-icheck.component.html',
  styleUrls: ['./r-icheck.component.css'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => RIcheckComponent),
      multi: true,
    },
    {
      provide: NG_VALIDATORS,
      useExisting: forwardRef(() => RIcheckComponent),
      multi: true,
    }]
})
export class RIcheckComponent implements OnInit, ControlValueAccessor, Validator {


  @ViewChild('checkbox') _elCheckBox: ElementRef;
  private _checked: boolean = false;

  constructor() { }

  ngOnInit() {
    console.log("ngOnInit");

    $(this._elCheckBox.nativeElement).iCheck({
      checkboxClass: 'icheckbox_square-blue',
      radioClass: 'iradio_minimal-blue',
      increaseArea: '20%' // optional
    });


    $(this._elCheckBox.nativeElement).on('ifChecked', (event) => {
      this._checked = true;
      if (this.propagateChange) this.propagateChange(this._checked);
    });
    $(this._elCheckBox.nativeElement).on('ifUnchecked', (event) => {
      this._checked = false;
      if (this.propagateChange) this.propagateChange(this._checked);
    });
  }

  validate(c: AbstractControl): { [key: string]: any; } {
    // throw new Error('Method not implemented.');
    return null;
  }
  registerOnValidatorChange(fn: () => void): void {
    //throw new Error('Method not implemented.');
  }

  writeValue(obj: any): void {
    this._checked = !(!(obj));
    console.log("this._checked", this._checked);
    $(this._elCheckBox.nativeElement).iCheck(this._checked ? 'check' : 'uncheck');



  }
  registerOnChange(fn: any): void {
    this.propagateChange = fn;
  }
  registerOnTouched(fn: any): void {
    //throw new Error('Method not implemented.');
  }
  setDisabledState(isDisabled: boolean): void {
    console.log("setDisabledState");
    $(this._elCheckBox.nativeElement).iCheck(isDisabled ? 'disable' : 'enable');
  }

  private propagateChange = (_: any) => { };

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
