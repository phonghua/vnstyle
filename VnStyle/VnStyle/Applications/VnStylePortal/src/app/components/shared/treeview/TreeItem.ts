export class TreeItem {
    name: string;
    expanded: boolean;
    checked: boolean;
    children: Array<TreeItem>;
    constructor(name, children) {
        this.name = name;
        this.children = children;
    }

    toggle() {
        this.expanded = !this.expanded;
    }
    check() {
        let newState = !this.checked;
        this.checked = newState;
        this.checkRecursive(newState);
    }
    checkRecursive(state) {
        this.children.forEach(d => {
            d.checked = state;
            d.checkRecursive(state);
        })
    }
}