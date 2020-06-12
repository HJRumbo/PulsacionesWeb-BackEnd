import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { User } from 'src/app/seguridad/user';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user-registro',
  templateUrl: './user-registro.component.html',
  styleUrls: ['./user-registro.component.css']
})
export class UserRegistroComponent implements OnInit {

  formGroup: FormGroup;
  user:  User;
  constructor(private userService: UserService, private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.buildForm();
  }

  private buildForm(){
    this.user = new User();
    this.user.userName = '';
    this.user.firstName = '';
    this.user.lastName = '';
    this.user.password = '';
    this.user.mobilePhone = '';
    this.user.email = '';

    this.formGroup = this.formBuilder.group({
      userName: [this.user.userName, Validators.required],
      firstName: [this.user.firstName, Validators.required],
      lastName: [this.user.lastName, Validators.required],
      password: [this.user.password, Validators.required],
      mobilePhone: [this.user.mobilePhone, Validators.required],
      email: [this.user.email, Validators.required]
    });
  }

  get control(){

    return this.formGroup.controls;
  }

  onSubmit(){
    if(this.formGroup.invalid){
      return;
    }
    this.add();
  }

  add() {

    this.user = this.formGroup.value;
    this.userService.post(this.user).subscribe(u => {
      if (u != null) {
        alert('Persona creada!');
        this.user = u;

      }else{
        alert('Error.');
      }
    });
  }


}
