import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpService } from '../../../../services';

@Component({
  selector: 'app-gallery-photo-new',
  templateUrl: './gallery-photo-new.component.html',
  styleUrls: ['./gallery-photo-new.component.css']
})
export class GalleryPhotoNewComponent implements OnInit {

  private rootCate;
  private rootCateName;

  private gallery = {};

  constructor(private httpService: HttpService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.params.subscribe(params => {

      this.rootCate = params["rootCateId"];
      this.rootCateName = params["rootCateName"];

    });
  }




}
