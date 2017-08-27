import { Injectable } from '@angular/core';

@Injectable()
export class SettingsService {

  private _portal = 'http://localhost:56847/';

  
  public get portal() {
    return this._portal;
  }
  
  
  constructor() {
    var w = <any>window;
    if (w.bootstrapSettings) {
      this._portal = w.bootstrapSettings.portal;
    }
  }
}
