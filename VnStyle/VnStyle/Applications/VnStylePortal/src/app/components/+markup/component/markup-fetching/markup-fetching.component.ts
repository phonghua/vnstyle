import { Component, OnInit } from '@angular/core';
import { MarkupService } from '../../../../services';

@Component({
  selector: 'app-markup-fetching',
  templateUrl: './markup-fetching.component.html',
  styleUrls: ['./markup-fetching.component.css']
})
export class MarkupFetchingComponent implements OnInit {

  foodyJsonRequest: any;
  foodyData: any = {};

  constructor( private markupService: MarkupService) { }

  ngOnInit() {
  }


  transferFoody() {
    this.foodyData = JSON.parse(this.foodyJsonRequest);
    console.log(this.foodyData);
  }

  fetchFoody() {
    if (this.foodyData.Items) {
      

      this.foodyData.Items.forEach(item => {
        this.markupService.createFoodyMarkup(Object.assign({FoodyId : item.Id}, item)).subscribe(result => {
          item.status = result.Data.Status
        });
      })
    }
  }

}
