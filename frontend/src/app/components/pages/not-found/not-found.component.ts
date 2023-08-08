import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-not-found',
  templateUrl: './not-found.component.html',
  styleUrls: ['./not-found.component.scss']
})
export class NotFoundComponent implements OnInit {

  statusCode: any;

  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    this.statusCode = this.route.snapshot.paramMap.get('statusCode');
    console.log("sto cazzo di",this.statusCode);
    
  }
}
