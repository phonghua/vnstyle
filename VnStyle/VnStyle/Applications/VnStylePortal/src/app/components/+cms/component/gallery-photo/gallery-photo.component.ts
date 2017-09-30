import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpService, GalleryService } from '../../../../services';

@Component({
  selector: 'app-gallery-photo',
  templateUrl: './gallery-photo.component.html',
  styleUrls: ['./gallery-photo.component.css']
})
export class GalleryPhotoComponent implements OnInit {

  private rootCate;
  private rootCateName;
  @ViewChild('fileInput') _elFileInput: ElementRef;
  private photoGrid = {
    data: []
  };


  constructor(private httpService: HttpService, private route: ActivatedRoute, private galleryService: GalleryService) { }

  ngOnInit() {
    this.route.params.subscribe(params => {

      this.rootCate = params["rootCateId"];
      // this.rootCateName = params["rootCateName"];

      this.galleryService.getPhoto(1).subscribe(data => {
        this.photoGrid.data = data;
      });
    });
  }


  browerFiles() {
    this._elFileInput.nativeElement.click();
  }

  fileOnChanged(event) {
    
  }


}
