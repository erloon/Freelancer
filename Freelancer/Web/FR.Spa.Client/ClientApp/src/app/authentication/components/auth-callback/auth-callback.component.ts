import { Component, OnInit } from '@angular/core';
import { IdentityService } from '../../shared/services/identity.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-auth-callback',
  templateUrl: './auth-callback.component.html',
  styleUrls: ['./auth-callback.component.scss']
})
export class AuthCallbackComponent implements OnInit {
  error: boolean;

  constructor(
    private identityService: IdentityService,
    private router: Router,
    private route: ActivatedRoute) { }

  async ngOnInit() {
    if (this.route.snapshot.fragment.indexOf('error') >= 0) {
      this.error = true;
      return;
    }

    await this.identityService.completeAuthentication();
    this.router.navigate(['/dashboard']);
  }
}
