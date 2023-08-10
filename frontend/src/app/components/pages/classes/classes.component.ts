import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-classes',
  templateUrl: './classes.component.html',
  styleUrls: ['./classes.component.scss']
})
export class ClassesComponent {

  public editForm!: FormGroup;

  ngOnInit(): void {
    this.editForm = new FormGroup({
      name: new FormControl("", Validators.required),
      classType: new FormControl("", Validators.required)
    });
  }

onEditSubmit(){
  
}

}
