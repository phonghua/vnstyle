import { Component, OnInit, ViewChild , ElementRef} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpService, GalleryService, ArtistService, GeneralService } from '../../../../services';

@Component({
  selector: 'app-artist',
  templateUrl: './artist.component.html',
  styleUrls: ['./artist.component.css']
})
export class ArtistComponent implements OnInit {

  private artistId;
  private rootCateName;
  @ViewChild('fileInput') _elFileInput: ElementRef;

  private get colNum (){
    const deviceWidth = this.generalService.getDocumentWidth();
    if(deviceWidth == 'sm')  return 2;
    if(deviceWidth == 'xs')  return 1;
    return 3;
  }

  private get cols(){
    var arr = [];
    for(let i = 0; i < this.colNum; i++){
      arr.push(i);
    }
    return arr;
  }

  

  private photoGrid = {
    data: []
  };

  constructor(private httpService: HttpService, private route: ActivatedRoute,
    private artistService : ArtistService, private generalService : GeneralService) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      
        this.artistId = params["id"];
        // this.rootCateName = params["rootCateName"];
  
        this.artistService.getPhoto(this.artistId).subscribe(data => {
          this.photoGrid.data = data;
        });
      });
  }

  browerFiles() {
    this._elFileInput.nativeElement.click();
  }

  fileOnChanged(event) {
    let files = this._elFileInput.nativeElement.files;
    this.httpService.postArtistPhoto(this.artistId, files, {
      onProgress: (processEvent) => {
        console.log("onProgress", processEvent);
      },
      onFinished: (result) => {
        var imageUrl = result.data.images[0].fileUrl;
        var imageId = result.data.images[0].id;


      },
      error: () => { }
    });

    console.log("fileOnChanged", event, this._elFileInput, files);

  }

}
