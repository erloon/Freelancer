import { Component, OnInit } from '@angular/core';
import {
  ReactiveFormsModule,
  FormGroup,
  FormControl,
  Validators,
  FormBuilder,
  AbstractControl
} from '@angular/forms';

import { IdentityService } from '../../shared/services/identity.service';
import { ValidationRulesService } from '../../shared/services/validation-rules.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  form: FormGroup;
  submitted = false;

  login = new FormControl('', [Validators.required, this.validationRulesService.loginHasEmailFormat]);
  password = new FormControl('', Validators.required);


  constructor(formBuilder: FormBuilder, private identityService: IdentityService, private validationRulesService: ValidationRulesService) {
    this.form = formBuilder.group({
      login: this.login,
      password: this.password
    });
  }

  get f() { return this.form.controls; }

  onLogin() {
    this.submitted = true;
    if (this.form.invalid) {
      console.log(this.form.errors);
      return;
    }
    //this.identityService.login(this.login.value, this.password.value);
  }

  ngOnInit() {
    this.identityService.login();
  }
}
