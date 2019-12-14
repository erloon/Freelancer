import { Component, OnInit } from "@angular/core";
import {
  FormGroup,
  Validators,
  FormControl,
  FormBuilder
} from "@angular/forms";
import { ValidationRulesService } from "src/app/services/Identity/validation-rules.service";
import { templateJitUrl } from "@angular/compiler";
import { IdentityService } from "src/app/services/Identity/identity.service";

@Component({
  selector: "app-register",
  templateUrl: "./register.component.html",
  styleUrls: ["./register.component.scss"]
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  submitted = false;

  login = new FormControl("", [
    Validators.required,
    this.validationRulesService.loginHasEmailFormat,
    Validators.maxLength(30)
  ]);
  password = new FormControl("", [
    Validators.required,
    Validators.maxLength(20)
  ]);
  confirmPassword = new FormControl("", [
    Validators.required,
    Validators.maxLength(20)
  ]);
  companyName = new FormControl("", [
    Validators.required,
    Validators.maxLength(50)
  ]);

  constructor(
    private validationRulesService: ValidationRulesService,
    private formBuilder: FormBuilder,
    private identityService: IdentityService
  ) {
    this.registerForm = this.formBuilder.group(
      {
        login: this.login,
        companyName: this.companyName,
        password: this.password,
        confirmPassword: this.confirmPassword
      },
      {
        validators: this.validationRulesService.passwordMustMatch(
          "password",
          "confirmPassword"
        )
      }
    );
  }

  get f() {
    return this.registerForm.controls;
  }

  ngOnInit() {}

  onRegister() {
    this.submitted = true;
    if (this.registerForm.invalid) {
      console.log(this.registerForm.errors);
      return;
    }
    this.identityService.register(this.login.value, this.password.value);
  }

}
