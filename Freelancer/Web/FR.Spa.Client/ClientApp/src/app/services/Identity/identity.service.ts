import { Injectable } from "@angular/core";

@Injectable({
  providedIn: "root"
})
export class IdentityService {
  constructor() {}

  login(login: string, password: string) {
    if (login === "sa@sa" && password === "sa") {
      return true;
    }
    return false;
  }

  loginByGoogle() {} // TODO

  loginByFacebook() {} // TODO

  register(login: string, password: string) {
    return true;
  }
}
