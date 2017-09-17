import { Component, OnInit, ViewChild } from '@angular/core';
import { ArtistService } from '../../../../services';
import { ConfirmModalComponent } from '../../../shared/confirm-modal/confirm-modal.component';
import { ModalComponent } from 'ng2-bs3-modal/ng2-bs3-modal';

@Component({
  selector: 'app-artists',
  templateUrl: './artists.component.html',
  styleUrls: ['./artists.component.css']
})
export class ArtistsComponent implements OnInit {

  artistGrid = {
    data: []
  }

  private artistModel = { id: 0 };
  private artistModalTitle = "Thêm mới";

  @ViewChild('confirmModal') confirmModal: ConfirmModalComponent;
  @ViewChild('artistModal') artistModal: ModalComponent;

  constructor(private artistService: ArtistService) { }

  ngOnInit() {
    this.loadArtistGrid();
  }

  loadArtistGrid() {
    this.artistService.getArtists().subscribe(data => {
      this.artistGrid.data = data;
    })
  }

  deleteArtist(artist) {
    this.confirmModal.open();
    this.confirmModal.ok = () => {
      this.artistService.deleteArtist(artist.id).subscribe(() => {
        this.confirmModal.close();
        this.loadArtistGrid();
      });
    }
  }

  openArtistCreatingModal() {
    this.artistModel = { id: 0 };
    this.artistModalTitle = "Thêm mới";
    this.artistModal.open();
  }

  openArtistUpdatingModal(artist) {
    this.artistModalTitle = "Cập nhật";
    this.artistModel = Object.assign({}, artist);
    this.artistModal.open();
  }

  saveArtist() {
    if (this.artistModel.id && this.artistModel.id > 0) {
      this.artistService.updateArtist(this.artistModel.id, this.artistModel).subscribe(data => {
        this.loadArtistGrid();
        this.artistModal.close();
      })
    }
    else {
      this.artistService.createArtist(this.artistModel).subscribe(data => {
        this.loadArtistGrid();
        this.artistModal.close();
      })
    }

  }


}
