import { Component, OnInit } from '@angular/core';
import { AppService } from './../../../services';
@Component({
  selector: 'app-sider-bar',
  templateUrl: './sider-bar.component.html',
  styleUrls: ['./sider-bar.component.css']
})
export class SiderBarComponent implements OnInit {

  private articleCategories = [];
  private galleryCategories = [];

  constructor(private appService: AppService) { }


  ngOnInit() {
    this.appService.appInitialized.subscribe(data => {
      console.log("subscribe at siderbar", data);
      this.articleCategories = data.articleCategories;
      this.galleryCategories = data.galleryCategories;

    });
  }

}
