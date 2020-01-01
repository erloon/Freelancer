import { Injectable } from '@angular/core';
import { UserManager, UserManagerSettings, User } from 'oidc-client';
import { BehaviorSubject, } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { ConfigService } from './config.service';

@Injectable({
  providedIn: 'root'
})
export class IdentityService {

  private _authenticationStatusSource = new BehaviorSubject<boolean>(false);
  authenticationStatus$ = this._authenticationStatusSource.asObservable();

  private manager = new UserManager(getClientSettings());
  private user: User | null;


  constructor(private http: HttpClient, private configService: ConfigService) {
    this.manager.getUser().then(user => {
      this.user = user;
      this._authenticationStatusSource.next(this.isAuthenticated());
    });
  }

  login() {
    return this.manager.signinRedirect();
  }

  async completeAuthentication() {
    this.user = await this.manager.signinRedirectCallback();
    this._authenticationStatusSource.next(this.isAuthenticated());
  }

  loginByGoogle() { } // TODO

  loginByFacebook() { } // TODO

  register(email: string, name: string, companyName: string, password: string) {
    return this.http.post(this.configService.authApiURI + '/account', {
      name: name,
      email: email,
      companyName: companyName,
      password: password
    });
  }

  isAuthenticated(): boolean {
    return this.user != null && !this.user.expired;
  }

  get authorizationHeaderValue(): string {
    return `${this.user.token_type} ${this.user.access_token}`;
  }

  get name(): string {
    return this.user != null ? this.user.profile.name : '';
  }

  signout() {
    this.manager.signoutRedirect();
  }
}

export function getClientSettings(): UserManagerSettings {
  return {
    authority: 'http://localhost:5000',
    client_id: 'spa',
    redirect_uri: 'http://localhost:4200/auth-callback',
    post_logout_redirect_uri: 'http://localhost:4200/',
    response_type: 'id_token token',
    scope: 'openid profile email FinanceManagerAPI',
    filterProtocolClaims: true,
    loadUserInfo: true,
    automaticSilentRenew: true,
    silent_redirect_uri: 'http://localhost:4200/silent-refresh.html'
  };
}
