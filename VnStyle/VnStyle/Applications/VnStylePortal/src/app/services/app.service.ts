import { Injectable, EventEmitter } from '@angular/core';
import { HttpService } from './http.service';
import { Observable } from 'rxjs/Observable';
import { SettingsService } from './settings.service';


import 'rxjs/Rx';
@Injectable()
export class AppService {

    appInitialized: EventEmitter<any> = new EventEmitter<any>();
    constructor() { }

    raiseAppInitlizedEvent(data) {
        this.appInitialized.emit(data);
    }

}
