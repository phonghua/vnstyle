import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { Observable } from 'rxjs/Observable';
import { SettingsService } from './settings.service';
import 'rxjs/Rx';
const swal = require('sweetalert2');

@Injectable()
export class GeneralService {

  constructor(private httpService: HttpService, private settingService: SettingsService) { }


  sweetAlert(config) {
    return swal(config);
  }

  deleteConfirm(title, text, okCallback, cancelCallback?) {
    swal({
      title: title || 'Are you sure?',
      text: text,
      type: 'warning',
      showCancelButton: true,
      confirmButtonColor: '',
      cancelButtonColor: '',
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'No, cancel',
      confirmButtonClass: 'btn btn-danger',
      cancelButtonClass: 'btn btn-default',
      buttonsStyling: false
    }).then(okCallback, (dismiss) => {

      if (dismiss === 'cancel') {
        if (cancelCallback && typeof (cancelCallback) == 'function') cancelCallback();
      }
    })
  }


  graphQL(query): Observable<any> {
    //const query = ["markups : markups(isDeleted : false){list{id, name , createdDate, image}}"];
    var data = { query: "query { " + query.join(",") + " }" };
    return this.httpService.post(this.settingService.portal + "webapi/graph/BizApp", data)
      .map(res => res.json())
      .catch(err => Observable.throw(err));
  }

  guid() {
    function s4() {
      return Math.floor((1 + Math.random()) * 0x10000)
        .toString(16)
        .substring(1);
    }
    return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
      s4() + '-' + s4() + s4() + s4();
  }

  friendlyUrl(title, maxlen) {
    //Đổi chữ hoa thành chữ thường
    var slug = title.toLowerCase();

    //Đổi ký tự có dấu thành không dấu
    slug = slug.replace(/á|à|ả|ạ|ã|ă|ắ|ằ|ẳ|ẵ|ặ|â|ấ|ầ|ẩ|ẫ|ậ/gi, 'a');
    slug = slug.replace(/é|è|ẻ|ẽ|ẹ|ê|ế|ề|ể|ễ|ệ/gi, 'e');
    slug = slug.replace(/i|í|ì|ỉ|ĩ|ị/gi, 'i');
    slug = slug.replace(/ó|ò|ỏ|õ|ọ|ô|ố|ồ|ổ|ỗ|ộ|ơ|ớ|ờ|ở|ỡ|ợ/gi, 'o');
    slug = slug.replace(/ú|ù|ủ|ũ|ụ|ư|ứ|ừ|ử|ữ|ự/gi, 'u');
    slug = slug.replace(/ý|ỳ|ỷ|ỹ|ỵ/gi, 'y');
    slug = slug.replace(/đ/gi, 'd');
    //Xóa các ký tự đặt biệt
    slug = slug.replace(/\`|\~|\!|\@|\#|\||\$|\%|\^|\&|\*|\(|\)|\+|\=|\,|\.|\/|\?|\>|\<|\'|\"|\:|\;|_/gi, '');
    //Đổi khoảng trắng thành ký tự gạch ngang
    slug = slug.replace(/ /gi, '-');
    //Đổi nhiều ký tự gạch ngang liên tiếp thành 1 ký tự gạch ngang
    //Phòng trường hợp người nhập vào quá nhiều ký tự trắng
    slug = slug.replace(/\-\-\-\-\-/gi, '-');
    slug = slug.replace(/\-\-\-\-/gi, '-');
    slug = slug.replace(/\-\-\-/gi, '-');
    slug = slug.replace(/\-\-/gi, '-');
    //Xóa các ký tự gạch ngang ở đầu và cuối
    slug = '@' + slug + '@';
    return slug.replace(/\@\-|\-\@|\@/gi, '');
    
  }



}
