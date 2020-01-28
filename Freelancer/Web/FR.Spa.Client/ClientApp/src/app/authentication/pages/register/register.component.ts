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
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  submitted = false;
  success: boolean;
  errors: Array<string>;

  email = new FormControl('', [
    Validators.required,
    this.validationRulesService.loginHasEmailFormat,
    Validators.maxLength(30)
  ]);
  name = new FormControl('', [
    Validators.required,
    Validators.maxLength(50),
    Validators.minLength(2)
  ]);
  password = new FormControl('', [
    Validators.required,
    Validators.maxLength(100),
    Validators.minLength(6)
  ]);
  confirmPassword = new FormControl('', [
    Validators.required,
    Validators.maxLength(20)
  ]);
  companyName = new FormControl('', [
    Validators.required,
    Validators.maxLength(100),
    Validators.minLength(2)
  ]);

  constructor(
    private validationRulesService: ValidationRulesService,
    private formBuilder: FormBuilder,
    private identityService: IdentityService,
    private router: Router

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

  register() {
    this.submitted = true;
    if (this.registerForm.invalid) {
      return;
    }
    this.identityService.register(this.email.value, this.name.value, this.companyName.value, this.password.value)
      .subscribe(
        result => {
          if (result) {
            this.success = true;
            this.router.navigate(['\\']);
          }
        },
        errorResp => {
          this.errors = errorResp.error;
        }
      );
  }

  cancel() {
    this.router.navigate(['\start']);
  }

}
