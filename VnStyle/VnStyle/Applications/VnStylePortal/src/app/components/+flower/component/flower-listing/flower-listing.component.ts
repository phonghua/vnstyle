import { Component, OnInit } from '@angular/core';
import { FlowerService, ShopService } from '../../../../services/';
@Component({
  selector: 'app-flower-listing',
  templateUrl: './flower-listing.component.html',
  styleUrls: ['./flower-listing.component.css']
})
export class FlowerListingComponent implements OnInit {

  private gridState: any = {
    fetched: false,
    fetching: false,
    data: [],
    pagination: {}
  };

  constructor(private shopService : ShopService, private flowerService : FlowerService) { }

  ngOnInit() {
    this.flowerService.getFlowers().subscribe(data => {
      this.gridState.data = data;
    });
  }

  refreshFlowers(){
    
  }
}
