import { Component } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { Registry } from "src/app/shared/models/users";
import { UsersService } from "src/app/shared/service/users.service";

@Component({
  selector: "app-detail-user",
  templateUrl: "./detail-user.component.html",
  styleUrls: ["./detail-user.component.scss"],
})
export class DetailUserComponent {
  id!: string;

  details!: Registry;

  // detailId!: any;

  constructor(
    private usersService: UsersService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.id = this.route.snapshot.params["id"];
    console.log(this.id);
    this.usersService.getDetailsUser(this.id).subscribe((res: Registry) => {
      console.log(res);
      this.details = res;
      // this.detailId = this.route.snapshot.paramMap.get("details");
    });
  }
}
