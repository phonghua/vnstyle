import { Component, OnInit, Input, Output, EventEmitter, forwardRef, HostListener} from '@angular/core';
import {CurrencyPipe} from '@angular/common';
import {NG_VALUE_ACCESSOR, ControlValueAccessor} from '@angular/forms';

const noop = () => {
};

export const CUSTOM_INPUT_CONTROL_VALUE_ACCESSOR: any = {
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => CurrencyEditorComponent),
    multi: true
};


@Component({
  selector: 'app-currency-editor',
  template: `
    <input class="form-control text-right" type="text" [(ngModel)]="myValue" (blur)="onBlur()" (focus)="onFocus()"/>
  `,
  styleUrls: ['./currency-editor.component.css'],
  providers: [CUSTOM_INPUT_CONTROL_VALUE_ACCESSOR]
})
export class CurrencyEditorComponent implements OnInit {

  @Input() currencyCode:  string = 'vnd'; 
  private innerValue: number = 0;
  private pipe: CurrencyPipe = new CurrencyPipe('au');
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

  onFocus(value) {
    this._myValue = this.value ? this.value.toFixed(2) : '0';
    this.isHighlighted = true;
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
      return this.pipe.transform(this.value, this.currencyCode, true);
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
