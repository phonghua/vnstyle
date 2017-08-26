import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-spinner',
  // templateUrl: './spinner.component.html',
  template: `
    <div class="loader"></div>
  `,
  styleUrls: ['./spinner.component.css']
})
export class SpinnerComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
