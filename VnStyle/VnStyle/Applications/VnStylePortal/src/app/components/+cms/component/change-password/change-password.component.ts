import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { UsersService, AuthService } from '../../../../services';
import { ToastsManager } from 'ng2-toastr/ng2-toastr';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {

  public model = { currentPassword: "", newPassword: "" };
  constructor(private userService: UsersService, public toastr: ToastsManager, vcr: ViewContainerRef,
    private authService: AuthService) {
    this.toastr.setRootViewContainerRef(vcr);
  }

  ngOnInit() {

  }


  changePassword() {
    this.userService.changePassword(this.model).subscribe(data => {
      this.toastr.success("Đổi mật khẩu thành công");
      setTimeout(() => {
        this.authService.logout();
      }, 2000);
      
    }, err => {
      console.log("error", err);
      this.toastr.error(err.join(','));
    });
  }
}
