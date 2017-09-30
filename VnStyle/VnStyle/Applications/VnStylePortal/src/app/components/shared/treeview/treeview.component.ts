import { Component, OnInit, Input } from '@angular/core';
import { TreeItem } from './TreeItem';
@Component({
  selector: 'app-treeview',
  templateUrl: './treeview.component.html',
  styleUrls: ['./treeview.component.css']
})
export class TreeviewComponent implements OnInit {

  @Input() items: Array<TreeItem>;
  @Input() showCheckbox : true;
  constructor() { }

  ngOnInit() {
  }


  toggle(item) {
    item.selected = true;
    item.expanded = !item.expanded;
  }

  check(item) {
    let newState = !item.checked;
    item.checked = newState;
    this.checkRecursive(item, newState);
  }
  checkRecursive(item, state) {
    item.children.forEach(d => {
      d.checked = state;
      d.checkRecursive(state);
    })
  }

}
