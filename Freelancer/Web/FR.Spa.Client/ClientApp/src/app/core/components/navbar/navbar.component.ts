import { AppService } from '../../../services/app.service';
import { Component, OnInit } from '@angular/core';
import { IdentityService } from 'src/app/authentication/shared/services/identity.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  constructor(
    private appService: AppService,
    private identityService: IdentityService) { }
  isCollapsed = true;
  ngOnInit() {
  }

  toggleSidebarPin() {
    this.appService.toggleSidebarPin();
  }
  toggleSidebar() {
    this.appService.toggleSidebar();
  }
  signOut(){
    this.identityService.signout();
  }

}
