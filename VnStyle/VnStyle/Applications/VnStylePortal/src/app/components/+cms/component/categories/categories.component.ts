import { Component, OnInit } from "@angular/core";
import { CategoryService } from "../../../../services";
import { ActivatedRoute } from "@angular/router";

@Component({
  selector: "app-categories",
  templateUrl: "./categories.component.html",
  styleUrls: ["./categories.component.css"]
})
export class CategoriesComponent implements OnInit {
  constructor(
    private categoryService: CategoryService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    
    this.route.params.subscribe(params => {
      const rootCateId = params["rootCateId"];
      console.log("root cateId", rootCateId);

      this.initializePage(rootCateId);
    });
  }

  initializePage(rootCateId){
    this.categoryService.getCategories(rootCateId).subscribe( data => {
      console.log("categories", data);
    });
  }
}
