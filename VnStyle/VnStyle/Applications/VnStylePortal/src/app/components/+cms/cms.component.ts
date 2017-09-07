import { Component, ViewEncapsulation, OnInit } from '@angular/core';
import { AppService } from '../../services';

@Component({
    selector: 'cms',
    template: '<router-outlet></router-outlet>',
    encapsulation: ViewEncapsulation.None,
})
export class CmsComponent implements OnInit {

    private initialized: boolean = false;
    /**
     *
     */
    constructor(private appService: AppService) {

    }
    ngOnInit(): void {
        console.log("CmsComponent init");
        this.appService.appInitialized.subscribe(data => {
            console.log("app subscribe at CmsComponent")
            this.initialized = true;
        });
    }

}
