import { Component, OnInit, Input, ElementRef, ViewChild } from '@angular/core';
import { MarkupService, HttpService } from '../../../../services';

@Component({
  selector: 'app-slide',
  templateUrl: './slide.component.html',
  styleUrls: ['./slide.component.css']
})
export class SlideComponent implements OnInit {
  @Input() markupId;
  @ViewChild('fileInput') _elFileInput: ElementRef;

  private pageState: any = {
    fetched: false,
    fetching: false,
    initialized: false
  };

  private galleryGridState: any = {
    fetched: false,
    fetching: false,
    data: [],
    pagination: {}
  };

  constructor(private markupService: MarkupService, private httpService: HttpService) { }

  ngOnInit() {
    this.getSlides().then(() => {
      this.pageState.fetched = true;
      this.pageState.fetching = false;
    })
  }

  refreshSlide() {

    this.markupService.getSlides(this.markupId).subscribe(data => {
      console.log("slide", data);
      this.galleryGridState.data = data;
    });
  }


  getSlides(): Promise<any> {

    return new Promise(resolve => {
      
      this.galleryGridState.fetched = false;
      this.galleryGridState.fetching = true;
      this.markupService.getSlides(this.markupId).subscribe(data => {
        console.log("slide", data);
        this.galleryGridState.data = data;
        this.galleryGridState.fetched = true;
        this.galleryGridState.fetching = false;
        resolve(data);
      });
    });
  }

  browseFiles() {
    console.log("browseFiles");
    this._elFileInput.nativeElement.click();
  }


  fileOnChanged(event) {
    let files = this._elFileInput.nativeElement.files;
    this.httpService.postMarkupGallery(this.markupId, files, {
      onProgress: (processEvent) => {
        console.log("onProgress", processEvent);
      },
      onFinished: (result) => {
        // this.imageUrl = result.Data.images[0].FileUrl;
        // this.imageId = result.Data.images[0].PhotoId;

        // this.propagateChange({
        //   imageId: this.imageId,
        //   imageUrl: this.imageUrl
        // });

        this.refreshSlide();
      },
      error: () => { }
    });

    console.log("fileOnChanged", event, this._elFileInput, files);

  }
}
