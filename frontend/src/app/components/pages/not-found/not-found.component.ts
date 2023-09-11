import { Location } from "@angular/common";
import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";

@Component({
  selector: "app-not-found",
  templateUrl: "./not-found.component.html",
  styleUrls: ["./not-found.component.scss"],
})
export class NotFoundComponent implements OnInit {
  statusCode!: any;

  constructor(private route: ActivatedRoute, private router: Router, private location: Location) {}

  // ngOnInit() {
  //   const navigation = this.router.getCurrentNavigation();
  //   const state = navigation?.extras.state as { statusCode: number } | undefined;
  //   if (state?.statusCode) {
  //     this.statusCode = state.statusCode;
  //   }
  // }
  ngOnInit(): void {
    this.statusCode = this.route.snapshot.paramMap.get('statusCode');
    console.log(this.statusCode);
  }

  goHome() {
    this.router.navigate(['']);
  }

  goBack() {
    this.location.back();
  }
}
