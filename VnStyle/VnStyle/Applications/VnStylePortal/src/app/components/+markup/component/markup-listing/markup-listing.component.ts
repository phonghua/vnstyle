import { Component, OnInit } from '@angular/core';
import { MarkupService } from '../../../../services';

@Component({
  selector: 'app-markup-listing',
  templateUrl: './markup-listing.component.html',
  styleUrls: ['./markup-listing.component.css']
})
export class MarkupListingComponent implements OnInit {
  private gridState: any = {
    fetched: false,
    fetching: false,
    data: [],
    pagination: {}
  };

  constructor(private markupService: MarkupService) { }

  ngOnInit() {
    this.markupService.getMarkups().subscribe((data) => {
      this.gridState.data = data;
    });
  }

}
