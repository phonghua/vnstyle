import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-form-summary-error',
  templateUrl: './form-summary-error.component.html',
  styleUrls: ['./form-summary-error.component.css']
})
export class FormSummaryErrorComponent implements OnInit {

  @Input() errors: any



  constructor() { }

  ngOnInit() {
  }


  // formFieldIsInValid(fieldName) {
  //   return this.errors && this.errors.length > 0 ? (this.errors.filter(p => p.FieldName == fieldName).length > 0) : false;
  // }

  // formFieldErrorMessages(fieldName) {
  //   const errors = this.errors && this.errors.length > 0 ? (!this.formFieldIsInValid(fieldName) ? [] : (this.errors.filter(p => p.FieldName == fieldName)[0].Messages)) : [];
  //   return errors;
  // }
}
