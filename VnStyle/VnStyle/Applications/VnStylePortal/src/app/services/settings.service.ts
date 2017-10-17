import { Injectable } from "@angular/core";
import { isDevMode } from "@angular/core";

@Injectable()
export class SettingsService {
  private _portal = "http://localhost:56847/";

  public get portal() {
    return this._portal;
  }

  public get froalaKey() {
    return "TezfbntA-8nzhiqpF4wl==";
  };

  constructor() {
    var w = <any>window;
    console.log("isDevMode()", isDevMode());
    if (isDevMode()) {
      this._portal = "http://localhost:56847/";
    } else {
      this._portal = "http://vnstyletattoo.vn/";
    }
  }
}
