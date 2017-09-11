import { Component, OnInit } from '@angular/core';
import { AppService, GeneralService } from './../../../services';

@Component({
  selector: 'app-sider-bar',
  templateUrl: './sider-bar.component.html',
  styleUrls: ['./sider-bar.component.css']
})
export class SiderBarComponent implements OnInit {

  private articleCategories = [];
  private galleryPhotoCategories = [];

  private get menuItems() {
    var menu = [];
    var articleCategories = this.articleCategories.map(p => {
      return { text: p.name, url: '/cms/' + p.id + '/' + this.generateService.friendlyUrl(p.name) + '/articles' };
    });
    var galleryPhotoCategories = this.galleryPhotoCategories.map(p => {
      var heading = {
        text: p.name, heading: false, children: [
          { text: p.name, url: '/cms/' + p.id + '/' + this.generateService.friendlyUrl(p.name) + '/gallery-photo' },
          { text: 'Danh mục', url: '/cms/' + p.id + '/' + this.generateService.friendlyUrl(p.name) + '/categories' }
        ]
      };

      // /cms/4/hinh-anh/categories

      return heading;
    });



    menu = menu.concat(articleCategories).concat(galleryPhotoCategories);

    return menu;
  }

  constructor(private appService: AppService, private generateService: GeneralService) { }


  ngOnInit() {
    this.appService.appInitialized.subscribe(data => {
      console.log("subscribe at siderbar", data);
      this.articleCategories = data.articleCategories;
      this.galleryPhotoCategories = data.galleryPhotoCategories;

      console.log(this.menuItems);
    });
  }



}
