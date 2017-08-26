import { Component, OnInit } from '@angular/core';
import { ShopService } from './../../../../services/shop.service';

@Component({
  selector: 'app-shop-listing',
  templateUrl: './shop-listing.component.html',
  styleUrls: ['./shop-listing.component.css']
})
export class ShopListingComponent implements OnInit {
  private gridState: any = {
    fetched: false,
    fetching: false,
    data: [],
    pagination: {}
  };
  constructor(private shopService: ShopService) { }

  ngOnInit() {
    this.shopService.getShops().subscribe((data) => {
      this.gridState.data = data;
    });
  }



}
