import { Component, OnInit, Input, Output, EventEmitter, forwardRef, HostListener} from '@angular/core';
import {CurrencyPipe, DecimalPipe } from '@angular/common';
import {NG_VALUE_ACCESSOR, ControlValueAccessor} from '@angular/forms';

const noop = () => {
};

export const CUSTOM_INPUT_CONTROL_VALUE_ACCESSOR: any = {
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => DecimalEditorComponent),
    multi: true
};


@Component({
  selector: 'app-decimal-editor',
  template: `
    <input class="form-control text-right" type="text" [(ngModel)]="myValue" (blur)="onBlur()" (focus)="onFocus($event)"/>
  `,
  styleUrls: ['./decimal-editor.component.css'],
  providers: [CUSTOM_INPUT_CONTROL_VALUE_ACCESSOR]
})
export class DecimalEditorComponent implements OnInit {

  //@Input() currencyCode:  string = 'vnd'; 
  private innerValue: number = 0;
  private pipe: DecimalPipe  = new DecimalPipe ('vn');
  private isHighlighted = false;
  private _myValue;

  //Placeholders for the callbacks which are later provided
  //by the Control Value Accessor
  private onTouchedCallback: () => void = noop;
  private onChangeCallback: (_: any) => void = noop;

  get value(): number {
    return this.innerValue;
  }
  set value(v: number) {
    if (v !== this.innerValue) {
      this.innerValue = v;
      this.onChangeCallback(v);
    }
  }

  writeValue(value: number) {
    if (value !== this.innerValue) {
      this.innerValue = value;
    }
  }

  registerOnChange(fn: any) {
    this.onChangeCallback = fn;
  }

  registerOnTouched(fn: any) {
    this.onTouchedCallback = fn;
  }

  onFocus(event) {
    this._myValue = this.value ? this.value.toFixed(0) : '0';
    this.isHighlighted = true;
    //this

    console.log("onFocus", event);

    
  }

  onBlur() {
    this.isHighlighted = false;
    this.onTouchedCallback();
  }

  /* string accessors for binding to the date input */
  get myValue() : string {
    if (this.isHighlighted)
      return this._myValue;
    else
      return this.pipe.transform(this.value);
  }

  set myValue(value: string) {
    this._myValue = value;
    value = value.replace(/.*?(([0-9]*\.)?[0-9]+).*/g, "$1");
    this.value = +value;
  }

  constructor() { }

  ngOnInit() {
  }

}
