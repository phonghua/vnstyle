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
}
