import { Component, OnInit } from '@angular/core';
import { AppService, GeneralService } from './../../../services';

@Component({
  selector: 'app-sider-bar',
  templateUrl: './sider-bar.component.html',
  styleUrls: ['./sider-bar.component.css']
})
export class SiderBarComponent implements OnInit {

  private articleCategories = [];
  //private galleryPhotoCategories = [];
  private artists = [];

  public menuItems;

  constructor(private appService: AppService, private generateService: GeneralService) { }


  ngOnInit() {
    this.appService.appInitialized.subscribe(data => {
      console.log("subscribe at siderbar", data);
      this.articleCategories = data.articleCategories;
      //this.galleryPhotoCategories = data.galleryPhotoCategories;

      this.artists = data.artists;

      this.menuItems = this.getMenuItems();      
    });
  }

  getMenuItems(){
    var menu = [];
    var articleCategories = this.articleCategories.map(p => {
      return { text: p.name, url: '/cms/' + p.id + '/' + this.generateService.friendlyUrl(p.name) + '/articles' };
    });
   

    var artistList = this.artists.map(p=> {
      return {
        text: p.name, url: '/cms/artists/' + p.id + '/' + this.generateService.friendlyUrl(p.name)
      };
    })
    var artistMenu =  {text : "Artists", heading : false, 
      children : [{text: 'Quản lý', url: '/cms/artists'}].concat(artistList)
    }


    var video = { text: "Video", url: '/cms/videos' };
    menu = menu.concat(articleCategories).concat([video]).concat(artistMenu);
    return menu;
  }



}
