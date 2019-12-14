import * as core from "@angular/core";
import { AbstractControl, FormGroup } from "@angular/forms";

@core.Injectable({
  providedIn: "root"
})
export class ValidationRulesService {
  constructor() {}

  loginHasEmailFormat(control: AbstractControl) {
    if (!control.value.includes("@")) {
      return { validLogin: true };
    }
    return null;
  }
  companyNameMaxCharacters(control: AbstractControl) {
    if (control.value.length > 50) {
      return { companyMaxLength: true };
    }
    return null;
  }
  passwordMustMatch(controlName: string, matchingControlName: string) {
    return (formGroup: FormGroup) => {
      const control = formGroup.controls[controlName];
      const matchingControl = formGroup.controls[matchingControlName];

      if (matchingControl.errors && !matchingControl.errors.mustMatch) {
        return;
      }

      if (control.value !== matchingControl.value) {
        matchingControl.setErrors({ mustMatch: true });
      } else {
        matchingControl.setErrors(null);
      }
    };
  }
}
