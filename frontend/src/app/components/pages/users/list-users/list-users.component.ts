import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { Registry } from 'src/app/shared/models/users';
import { ListResponse } from 'src/app/shared/models/listresponse';
import { UsersService } from 'src/app/shared/service/users.service';


@Component({
  selector: "app-list-users",
  templateUrl: "./list-users.component.html",
  styleUrls: ["./list-users.component.scss"],
})
export class ListUsersComponent {
  constructor(private usersService: UsersService, private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.page = 1;
    this.getUser("Name", "asc", "name");
  }

  registries: Registry[] = [];
  filter: string = "";
  action: string = "";
  id!: string;
  page: number = 1;
  text: string = "";
  unsubscribe$: Subject<boolean> = new Subject<boolean>();

  userEdit!: string;

  orders: {
    name: "asc" | "desc";
    surname: "asc" | "desc";
    birth: "asc" | "desc";
  } = {
    name: "asc",
    surname: "asc",
    birth: "asc",
  };

  onClickRole(event: any): void {
    this.filter = event.target.value;
    this.page = 1;
    console.log(this.filter);
      this.getUser("Name", this.orders.name);
    
  }

  onClickAction(action: string, id: string): void {
    this.action = action;
    this.id = id;
    console.log(this.id);
  }

  onClickPage(page: number) {
    this.page = page;
    console.log(this.page);
    this.getUser("Name", this.orders["name"]);
  }

  getUser(
    order: string,
    type: "asc" | "desc",
    id?: keyof typeof this.orders,
    search?: string
  ): void {
    let role = "";
    search = this.text;

    switch (this.filter) {
      case "student":
        role = "student";
        break;
      case "teacher":
        role = "teacher";
        break;
    }
    console.log(this.filter);

    
    this.usersService
      .getUsers(order, type, this.page, this.filter, search).pipe(takeUntil(this.unsubscribe$)).subscribe({
        next: (res: ListResponse<Registry[]>) => {
          console.log(res);
          console.log(id);

          if (id) {
            this.orders = {
              name: "asc",
              surname: "asc",
              birth: "asc",
              [id]: type,
            };
          }
          console.log("orders", this.orders);
          // id === 'name' && this.orderName === "asc" ? this.orderName = "desc" : this.orderName = "asc";
          // id === 'surname' && this.orderSurname === "desc" ? this.orderSurname = "asc" : this.orderSurname = "desc";
          // id === 'birth' && this.orderBirth === "desc" ? this.orderBirth = "asc" : this.orderBirth = "desc";

          this.registries = res.data;
          console.log(this.registries);
        },
        error: (error) => {
          console.log(error);
        },
      });
  }

  // saveUser(user: string) {
  //   this.userEdit = user;
  //   this.getUser();
  // }

  dUser(id: string): void {
    console.log(id);
    this.usersService.deleteUser(id).pipe(takeUntil(this.unsubscribe$)).subscribe({
      next: (res) => {
        this.page = 1;
        this.getUser("Name", "asc", "name");
        console.log(res);
      },
      error: (error) => {
        console.log("error", error);
      },
    });
  }

  saveSearch(text: string) {
    this.text = text;
    this.getUser("Name", "asc", "name", this.text);
  }

  ngOnDestroy(): void {
    this.unsubscribe$.next(true);
    this.unsubscribe$.complete(); 
  }
}

