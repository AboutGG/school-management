import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ErrorsInterceptor } from 'src/app/shared/helpers/errors.interceptor';

@Component({
  selector: 'app-not-found',
  templateUrl: './not-found.component.html',
  styleUrls: ['./not-found.component.scss']
})
export class NotFoundComponent implements OnInit{

  statusCode!: number;

  constructor(private route: ActivatedRoute) {
    
  }

  ngOnInit() {
    this.statusCode = this.route.snapshot.params['statusCode'];
    console.log(this.statusCode);
  }
}