export interface IComponent {
    // getActions(): Action[];
    // getTitle(): string;
    getBreadcrumb(): Breadcrumb[];
    getActions(): Action[];
}


export class Action {
    icon: string;
    name: string;
    func: any;

    constructor(_name, _icon, _func) {
        this.name = _name;
        this.icon = _icon;
        this.func = _func;
    }
}

export class Breadcrumb {
    name: string;
    url: string;
    constructor(_name, _url) {
        this.name = _name;
        this.url = _url;
    }
}