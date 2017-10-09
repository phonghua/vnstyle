import { Component, OnInit } from '@angular/core';
import { AppService, GeneralService } from './../../../services';

@Component({
  selector: 'app-sider-bar',
  templateUrl: './sider-bar.component.html',
  styleUrls: ['./sider-bar.component.css']
})
export class SiderBarComponent implements OnInit {

  private articleCategories = [];
  private artists = [];
  public menuItems;

  constructor(private appService: AppService, private generateService: GeneralService) { }


  ngOnInit() {
    this.appService.appInitialized.subscribe(data => {
      this.articleCategories = data.articleCategories;
      this.artists = data.artists;
      this.menuItems = this.getMenuItems();
    });
  }

    getMenuItems() {
      let menu = [];
      const articleCategories = this.articleCategories.map(p => {
        return { text: p.name, url: '/cms/' + p.id + '/' + this.generateService.friendlyUrl(p.name) + '/articles' };
      });

      const artistList = this.artists.map(p => {
        return {
          text: p.name, url: '/cms/artists/' + p.id + '/' + this.generateService.friendlyUrl(p.name)
        };
      });

      const artistMenu = {
        text: "Artists", heading: false,
        children: [{ text: 'Quản lý', url: '/cms/artists' }].concat(artistList)
      }

      const video = { text: "Video", url: '/cms/videos' };
      const featuredArticle = { text: "Featured Articles", url: '/cms/featured-articles' };
      const users = { text: "Users", url: '/cms/users' };
      menu = menu.concat(articleCategories).concat([video, featuredArticle, users]).concat(artistMenu);
      return menu;
    }

}
