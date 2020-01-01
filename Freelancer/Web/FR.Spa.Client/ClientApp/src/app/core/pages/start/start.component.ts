import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IdentityService } from 'src/app/authentication/shared/services/identity.service';

@Component({
  selector: 'app-start',
  templateUrl: './start.component.html',
  styleUrls: ['./start.component.scss']
})
export class StartComponent implements OnInit {

  constructor(
    private router: Router,
    private identityService: IdentityService
  ) { }

  ngOnInit() {
  }

  register() {
    this.router.navigate(['/register']);
  }

  login(){
    this.identityService.login();
  }
}
