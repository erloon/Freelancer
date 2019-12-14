import { Component, OnInit } from '@angular/core';
import { UserRegistration } from 'src/app/shared/user.registration';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  userRegistration: UserRegistration = { name: '', companyName: '', email: '', password: '' };
  constructor() { }

  ngOnInit() {
  }

  onSubmit() {

  }

}
