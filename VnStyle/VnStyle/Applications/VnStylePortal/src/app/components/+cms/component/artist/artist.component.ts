import { Component, OnInit, ViewChild, ElementRef, ViewContainerRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpService, GalleryService, ArtistService, GeneralService } from '../../../../services';
import { ToastsManager } from 'ng2-toastr/ng2-toastr';

@Component({
  selector: 'app-artist',
  templateUrl: './artist.component.html',
  styleUrls: ['./artist.component.css']
})
export class ArtistComponent implements OnInit {

  public artistId;
  public rootCateName;
  @ViewChild('fileInput') _elFileInput: ElementRef;

  public get colNum() {
    const deviceWidth = this.generalService.getDocumentWidth();
    if (deviceWidth == 'sm') return 2;
    if (deviceWidth == 'xs') return 1;
    return 3;
  }

  public get cols() {
    var arr = [];
    for (let i = 0; i < this.colNum; i++) {
      arr.push(i);
    }
    return arr;
  }



  public photoGrid = {
    data: []
  };

  public selectedPhoto = -1;

  constructor(private httpService: HttpService, private route: ActivatedRoute,
    private artistService: ArtistService, private generalService: GeneralService,
    public toastr: ToastsManager, vcr: ViewContainerRef) {
    this.toastr.setRootViewContainerRef(vcr);
  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.artistId = params["id"];
      // this.rootCateName = params["rootCateName"];

      this.loadPhoto();
    });
  }

  loadPhoto() {
    this.artistService.getPhoto(this.artistId).subscribe(data => {
      this.photoGrid.data = data;

      if (this.photoGrid.data && this.photoGrid.data.length > 0) {
        this.selectedPhoto = 0;
      }
    });
  }

  browerFiles() {
    this._elFileInput.nativeElement.click();
  }

  fileOnChanged(event) {
    let files = this._elFileInput.nativeElement.files;
    this.httpService.postArtistPhoto(this.artistId, files, {
      onProgress: (processEvent) => {
      },
      onFinished: (result) => {
        // var imageUrl = result.data.images[0].fileUrl;
        // var imageId = result.data.images[0].id;

        this.toastr.success("Upload photo success");
        this.loadPhoto();

      },
      error: () => { }
    });
  }

  viewPrevPhoto() {
    if (this.selectedPhoto > 0) {
      this.selectedPhoto = this.selectedPhoto - 1;
    }
  }

  viewNextPhoto() {
    if (this.selectedPhoto < this.photoGrid.data.length - 1) {
      this.selectedPhoto = this.selectedPhoto + 1;
    }
  }

  deletePhoto(photoId) {
    this.artistService.deletePhoto(this.artistId, photoId).subscribe(() => {
      
    })
  }
}
