export class TreeItem {
    id:number;
    name: string;
    expanded: boolean;
    checked: boolean;
    children: Array<TreeItem>;
    selected : boolean = false;
    constructor(id, name, children) {
        this.id = id;
        this.name = name;
        this.children = children;
        this.checked = false;
        this.expanded = true;
    }

    // toggle() {
    //     console.log(this.expanded);
    //     this.expanded = !this.expanded;
    //     console.log(this);
    // }
    // check() {
    //     let newState = !this.checked;
    //     this.checked = newState;
    //     this.checkRecursive(newState);
    // }
    // checkRecursive(state) {
    //     this.children.forEach(d => {
    //         d.checked = state;
    //         d.checkRecursive(state);
    //     })
    // }
}