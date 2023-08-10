import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-subjects',
  templateUrl: './subjects.component.html',
  styleUrls: ['./subjects.component.scss']
})
export class SubjectsComponent {
  public editForm!: FormGroup;

  ngOnInit(): void {
    this.editForm = new FormGroup({
      name: new FormControl("", Validators.required),
      teacher: new FormControl("", Validators.required)
    });
  }

onEditSubmit(){
  
}



}
