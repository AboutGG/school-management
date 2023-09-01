import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Registry } from 'src/app/shared/models/users';
import { UsersService } from 'src/app/shared/service/users.service';

@Component({
  selector: 'app-detail-user',
  templateUrl: './detail-user.component.html',
  styleUrls: ['./detail-user.component.scss']
})
export class DetailUserComponent {

  id!: number;

  details!: Registry;

  constructor(private usersService: UsersService, private route: ActivatedRoute) {}

  ngOnInit(): void {
    const id = this.route.snapshot.params['id'];
    console.log(id)
    this.usersService.getDetailsUser(id).subscribe((res: Registry) => { 
      console.log(res)
    this.details = res;
    })
  } 
}
