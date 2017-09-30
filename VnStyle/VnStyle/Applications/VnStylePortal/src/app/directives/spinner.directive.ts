import {
    Directive,
    Input,
    OnInit,
    ComponentRef,
    ComponentFactoryResolver,
    ViewContainerRef,
    TemplateRef
} from '@angular/core';
import { BlockUIContentComponent, BlockUIService } from 'ng-block-ui';
import { GeneralService } from '../services/general.service';
//import { BlockUIDefaultName } from 'ng-block-ui';

@Directive({ selector: '[spinner]' })
export class SpinnerDirective implements OnInit {
    private blockUIComponentRef: ComponentRef<BlockUIContentComponent>;
    blockTarget: string;
    message: any;
    template: any;

    @Input()
    set spinner(loading) {       
        //let blockUIContent = this.findContentNode(this.viewRef.element.nativeElement);
        if (loading) {
            // if (this.parentElement)
            //     this.parentElement.classList.add('block-ui-loading');

            this.blockUIService.start(this.blockTarget, "Loading...");
        }
        else {
            // if (this.parentElement)
            //     this.parentElement.classList.remove('block-ui-loading');
            this.blockUIService.stop(this.blockTarget);
        }
    };

    constructor(
        private viewRef: ViewContainerRef,
        private templateRef: TemplateRef<any>,
        private componentFactoryResolver: ComponentFactoryResolver,
        private general: GeneralService,
        private blockUIService: BlockUIService
    ) {
        this.blockTarget = this.general.guid();
        // this.viewRef.createEmbeddedView(this.templateRef);
        // this.parentElement = this.viewRef.element.nativeElement.nextSibling;
    }

    private blockUIContent = null;
    parentElement: any;
    ngOnInit() {
        try {
            this.viewRef.createEmbeddedView(this.templateRef);
             this.parentElement = this.viewRef.element.nativeElement.nextSibling;

            if (this.parentElement && !this.isComponentInTemplate(this.parentElement)) {
                this.parentElement.classList.add('block-ui__element');
                this.blockUIComponentRef = this.createComponent();

                this.blockUIContent = this.findContentNode(this.viewRef.element.nativeElement);

                if (this.blockUIContent) {
                    this.parentElement.appendChild(this.blockUIContent);
                    this.blockUIComponentRef.instance.className = 'block-ui-wrapper--element';
                    this.blockUIComponentRef.instance.name = this.blockTarget;
                }
            }
        } catch (error) {
            console.error('ng-block-ui:', error);
        }
    }


    private isComponentInTemplate(element: any): boolean {
        let { children } = element || [];
        children = Array.from(children).reverse();
        return children.some((el: any) => el.localName === 'block-ui');
    }

    // Needed for IE (#17)
    private findContentNode(element: any) {
        const { nextSibling } = element;
        return [nextSibling, nextSibling.nextSibling].find((e) => e.localName === 'block-ui-content');
    }

    private createComponent() {
        const resolvedBlockUIComponent = this.componentFactoryResolver.resolveComponentFactory(BlockUIContentComponent);
        return this.viewRef.createComponent(resolvedBlockUIComponent);
    }
}