import { Component, OnInit, ViewChild, Input, Output, EventEmitter } from '@angular/core';
import { ModalComponent } from 'ng2-bs3-modal/ng2-bs3-modal';


@Component({
  selector: 'app-confirm-modal',
  templateUrl: './confirm-modal.component.html',
  styleUrls: ['./confirm-modal.component.css']
})

export class ConfirmModalComponent implements OnInit {
  @ViewChild('modal') modal: ModalComponent;

  @Input() title: string = "Warning!";
  @Input() message: string = "Are you sure?";

  constructor() { }

  ngOnInit() {
  }


  close() {
    this.modal.close();
  }

  open() {
    this.modal.open();
  }

  ok() {

  }


}
