import { Component, OnInit, AfterViewInit, ElementRef } from '@angular/core';
import {
  FormGroup,
  Validators,
  FormControl,
  FormBuilder
} from '@angular/forms';
import { ValidationRulesService } from 'src/app/authentication/shared/services/validation-rules.service';
import { templateJitUrl } from '@angular/compiler';
import { IdentityService } from '../../shared/services/identity.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  submitted = false;
  success: boolean;
  error: string;

  email = new FormControl('', [
    Validators.required,
    this.validationRulesService.loginHasEmailFormat,
    Validators.maxLength(30)
  ]);
  name = new FormControl('', [
    Validators.required,
    Validators.maxLength(20)
  ]);
  password = new FormControl('', [
    Validators.required,
    Validators.maxLength(20)
  ]);
  confirmPassword = new FormControl('', [
    Validators.required,
    Validators.maxLength(20)
  ]);
  companyName = new FormControl('', [
    Validators.required,
    Validators.maxLength(50)
  ]);

  constructor(
    private validationRulesService: ValidationRulesService,
    private formBuilder: FormBuilder,
    public identityService: IdentityService,

  ) {
    this.registerForm = this.formBuilder.group(
      {
        name: this.name,
        email: this.email,
        companyName: this.companyName,
        password: this.password,
        confirmPassword: this.confirmPassword
      },
      {
        validators: this.validationRulesService.passwordMustMatch(
          'password',
          'confirmPassword'
        )
      }
    );
  }

  get f() {
    return this.registerForm.controls;
  }

  ngOnInit() { }

  onRegister() {
    this.submitted = true;
    if (this.registerForm.invalid) {
      console.log(this.registerForm.errors);
      return;
    }
    this.identityService.register(this.email.value, this.name.value, this.companyName.value, this.password.value)
      .subscribe(
        result => {
          console.log(result);
          if (result) {
            this.success = true;
          }
        },
        error => {
          console.error(error);
          this.error = error;
        }
      );
  }

}
