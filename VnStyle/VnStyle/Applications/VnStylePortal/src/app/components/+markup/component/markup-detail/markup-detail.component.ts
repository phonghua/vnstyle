import { Component, OnInit } from '@angular/core';
import { MarkupService, CountryService, CityService, DistrictService } from '../../../../services';
import { ActivatedRoute } from '@angular/router';
//import Rx from 'rxjs/Rx';
import { Observable } from 'rxjs/Observable';
var Rx = require('rxjs/Rx');


@Component({
  selector: 'app-markup-detail',
  templateUrl: './markup-detail.component.html',
  styleUrls: ['./markup-detail.component.css']
})
export class MarkupDetailComponent implements OnInit {
  private markupId: number;
  private pageState: any = {
    fetched: false,
    fetching: false,
    initialized: false,
    data: null
  };

  constructor(private markupService: MarkupService, private route: ActivatedRoute,
    private countryService: CountryService, private cityService: CityService, private districtService: DistrictService) { }

  ngOnInit() {
    //
    Observable.forkJoin([
      this.countryService.getCountries(),
      this.cityService.getCities(),
      this.districtService.getDistricts()
    ]).subscribe((response) => {
      console.log("response", response);
    });







    this.route.params.subscribe(params => {
      this.markupId = +params['id']; // (+) converts string 'id' to a number

      // In a real app: dispatch action to load the details here.
      this.initialize(this.markupId);
    });
  }

  initialize(markupId) {
    // clean up page and initialize data
    this.pageState.fetched = false;
    this.pageState.fetching = true;
    this.pageState.initialized = false;
    this.pageState.data = null;


    this.markupService.getMarkup(markupId).subscribe(data => {
      this.pageState.fetched = true;
      this.pageState.fetching = false;
      this.pageState.initialized = true;



      this.pageState.data = {
        markup: data,
      };

      if (this.pageState.data.markup.markupInfo) {
        
          const total = this.pageState.data.markup.markupInfo.oneStarCount + this.pageState.data.markup.markupInfo.twoStarCount + this.pageState.data.markup.markupInfo.threeStarCount + this.pageState.data.markup.markupInfo.fourStarCount + this.pageState.data.markup.markupInfo.fiveStarCount;
          this.pageState.data.markup.markupInfo.rating = {
            total: total ,
            oneStarPercentage: this.pageState.data.markup.markupInfo.oneStarCount / (total == 0 ? 1 : total),
            twoStarPercentage: this.pageState.data.markup.markupInfo.oneStarCount / (total == 0 ? 1 : total),
            threeStarPercentage: this.pageState.data.markup.markupInfo.oneStarCount / (total == 0 ? 1 : total),
            fourStarPercentage: this.pageState.data.markup.markupInfo.oneStarCount / (total == 0 ? 1 : total),
            fiveStarPercentage: this.pageState.data.markup.markupInfo.oneStarCount / (total == 0 ? 1 : total),
          };
        

        //this.markupItemForm = Object.assign({}, this.markupInfo);
      }


      console.log(this.pageState);

    });
  }
}
