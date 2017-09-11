import { Component, OnInit, Input } from '@angular/core';
import { TreeItem } from './TreeItem';
@Component({
  selector: 'app-treeview',
  templateUrl: './treeview.component.html',
  styleUrls: ['./treeview.component.css']
})
export class TreeviewComponent implements OnInit {


  @Input() items: Array<TreeItem>;
  constructor() { }

  ngOnInit() {
  }

}
