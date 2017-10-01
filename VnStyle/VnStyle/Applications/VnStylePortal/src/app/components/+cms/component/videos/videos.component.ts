import { Component, OnInit, ViewChild, Input, ViewContainerRef } from '@angular/core';
import { VideosService } from '../../../../services';
import { ModalComponent } from 'ng2-bs3-modal/ng2-bs3-modal';
import { ToastsManager } from 'ng2-toastr/ng2-toastr';
import { ConfirmModalComponent } from '../../../shared/confirm-modal/confirm-modal.component';

@Component({
  selector: 'app-videos',
  templateUrl: './videos.component.html',
  styleUrls: ['./videos.component.css']
})
export class VideosComponent implements OnInit {

  public videoGrid = {
    data: [],
    loading: false
  }

  public videoModel = { title: "", link: "" };

  @ViewChild('modal') modal: ModalComponent;
  @ViewChild('confirmModal') confirmModal: ConfirmModalComponent;

  constructor(private videoService: VideosService, public toastr: ToastsManager, vcr: ViewContainerRef) {
    this.toastr.setRootViewContainerRef(vcr);
  }

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

  openAddNewForm() {
    this.videoModel = { title: "", link: "" };
    this.modal.open();
  }

  close() {
    this.modal.close();
  }



  ok() {
    this.videoService.postVideo(this.videoModel).subscribe(() => {
      this.loadVideos();
      this.modal.close();
      this.toastr.success("Thêm mới video thành công");
    });
  }


  deleteVideo(video){
    this.confirmModal.message = "Bạn có chắc muốn xóa video này?";
    this.confirmModal.open();
    this.confirmModal.ok = () => {
      this.videoService.deleteVideo(video.id).subscribe(() => {
        this.loadVideos();
        this.toastr.success("Xóa video thành công");
        this.confirmModal.close();
      });
    }
  }
}
