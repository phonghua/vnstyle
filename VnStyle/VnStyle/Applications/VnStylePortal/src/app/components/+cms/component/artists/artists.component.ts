import { Component, OnInit } from '@angular/core';
import { ArtistService } from '../../../../services';

@Component({
  selector: 'app-artists',
  templateUrl: './artists.component.html',
  styleUrls: ['./artists.component.css']
})
export class ArtistsComponent implements OnInit {

  artistGrid = {
    data : []
  }
  constructor(private artistService : ArtistService) { }

  ngOnInit() {
    this.artistService.getArtists().subscribe(data => {
      this.artistGrid.data = data;
    })
  }


  deleteArtist(artist){
    //this.artistService.deleteArticle
  }

}
