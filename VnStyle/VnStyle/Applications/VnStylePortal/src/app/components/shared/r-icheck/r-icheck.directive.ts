import { Directive, ElementRef, OnInit } from '@angular/core';
declare var $: any

@Directive({
  selector: '[RIcheck]'
})
export class RIcheckDirective implements OnInit {

  ngOnInit(): void {
    this.initIcheck();
  }

  constructor(private el: ElementRef) {


  }

  ngAfterContentInit() {
  }

  initIcheck() {
    // $(this.el.nativeElement).iCheck({
    //   checkboxClass: 'icheckbox_square-blue',
    //   radioClass: 'iradio_minimal-blue',
    //   increaseArea: '20%' // optional
    // });

    // $(this.el.nativeElement).on('ifChecked', function (event) {
    //   debugger;
    //   //alert(event.type + ' callback');
    //   //this.el.nativeElement.value = 
    // });
  }
}
