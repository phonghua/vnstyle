import { Component, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { UsersService } from '../../../../services';
import { ModalComponent } from 'ng2-bs3-modal/ng2-bs3-modal';
import { ToastsManager } from 'ng2-toastr/ng2-toastr';
import { ConfirmModalComponent } from '../../../shared/confirm-modal/confirm-modal.component';


@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

  public userGrid = {
    data: [],
    loading: false
  }

  public model = { email: "", password: "" };
  public resetPasswordModel = {id : 0, password: "" };
  @ViewChild('modal') modal: ModalComponent;
  @ViewChild('confirmModal') confirmModal: ConfirmModalComponent;
  @ViewChild('resetPassword') resetPasswordModal: ModalComponent;


  constructor(private userService: UsersService, public toastr: ToastsManager, vcr: ViewContainerRef) {
    this.toastr.setRootViewContainerRef(vcr);
  }

  ngOnInit() {
    this.loadUsers();
  }

  loadUsers() {
    this.userGrid.loading = true;
    this.userService.getUsers().subscribe((data) => {
      this.userGrid.data = data;
      this.userGrid.loading = false;
    })
  }


  openAddNewForm() {
    this.model = { email: "", password: "" };
    this.modal.open();
  }

  openResetPasswordForm(user) {
    this.resetPasswordModel = {id : user.id, password: "" };
    this.resetPasswordModal.open();
  }

  close() {
    this.modal.close();
  }



  ok() {
    this.userService.createUser(this.model).subscribe(() => {
      this.loadUsers();
      this.modal.close();
      this.toastr.success("Thêm mới tài khoản thành công");
    });
  }

  submitResetPassword(){
    this.userService.resetPassword(this.resetPasswordModel.id, this.resetPasswordModel).subscribe(()=> {
      this.loadUsers();
      this.resetPasswordModal.close();
      this.toastr.success("Reset mật khẩu thành công");
    })
  }

  deleteUser(user) {
    this.confirmModal.message = "Bạn có chắc muốn xóa tài khoản này?";
    this.confirmModal.open();
    this.confirmModal.ok = () => {
      this.userService.deleteUser(user.id).subscribe(() => {
        this.loadUsers();
        this.toastr.success("Xóa tài khoản thành công");
        this.confirmModal.close();
      })
      // this.videoService.deleteVideo(video.id).subscribe(() => {
      //   this.loadVideos();
      //   this.toastr.success("Xóa video thành công");
      //   this.confirmModal.close();
      // });
    }
  }

}
