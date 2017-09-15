import { Component, OnInit } from "@angular/core";
import { CategoryService } from "../../../../services";
import { ActivatedRoute } from "@angular/router";
import { TreeItem } from '../../../shared/treeview/TreeItem';

@Component({
  selector: "app-categories",
  templateUrl: "./categories.component.html",
  styleUrls: ["./categories.component.css"]
})


export class CategoriesComponent implements OnInit {
  private categories = {
    loading: false,
    data: [],
  }



  constructor(
    private categoryService: CategoryService,
    private route: ActivatedRoute
  ) { }

  ngOnInit() {

    this.route.params.subscribe(params => {
      const rootCateId = params["rootCateId"];
      console.log("root cateId", rootCateId);

      this.initializePage(rootCateId);
    });
  }

  initializePage(rootCateId) {
    this.categories.loading = false;
    this.categoryService.getCategories(rootCateId).subscribe(data => {
      // console.log("categories", data);
      this.categories.loading = false;
      this.categories.data = data;
    });
  }

  getCategoryTree() {
    return this.categories.data.filter(p => p.parent == null).map(p => {
      p.children = this.getCategoryChildren(p);
      return new TreeItem(p.id, p.name, p.children);
    });
  }

  getCategoryChildren(cate) {
    return this.categories.data.filter(p => p.parent == cate.id).map(p => {
      p.children = this.getCategoryChildren(p);
      return new TreeItem(p.id, p.name, p.children);
    });
  }

}
