import { Injectable, EventEmitter } from '@angular/core';
import { HttpService } from './http.service';
import { Observable } from 'rxjs/Observable';
import { SettingsService } from './settings.service';


import 'rxjs/Rx';
@Injectable()
export class AppService {

    appInitialized: EventEmitter<any> = new EventEmitter<any>();
    private _data;
    public get AllCategories() {
        return this._data.articleCategories.concat(this._data.galleryPhotoCategories);
    }

    constructor() { }

    raiseAppInitlizedEvent(data) {
        //
        this._data = data;
        console.log("initialized", data);
        this.appInitialized.emit(data);


    }

}
