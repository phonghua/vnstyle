import { Component, OnInit } from '@angular/core';
import { VideosService } from '../../../../services';

@Component({
  selector: 'app-videos',
  templateUrl: './videos.component.html',
  styleUrls: ['./videos.component.css']
})
export class VideosComponent implements OnInit {

  public videoGrid = {
    data : [],
    loading : false
  }
  constructor(private videoService: VideosService) { }

  ngOnInit() {
    this.loadVideos();
  }


  private loadVideos() {
    this.videoGrid.loading = true;
    this.videoService.getVideos().subscribe(data => {
      this.videoGrid.loading = false;
      this.videoGrid.data = data;
    });
  }

  openAddNewForm (){
    
  }
}
