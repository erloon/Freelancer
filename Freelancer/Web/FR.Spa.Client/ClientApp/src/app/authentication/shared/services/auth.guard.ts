import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { IdentityService } from './identity.service';

@Injectable()
export class AuthGuard implements CanActivate {

  constructor(private router: Router, private identityService: IdentityService) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    if (this.identityService.isAuthenticated()) { return true; }
    this.router.navigate(['/login'], { queryParams: { redirect: state.url }, replaceUrl: true });
    return false;
  }

}
