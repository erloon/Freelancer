import { Component, OnInit } from '@angular/core';
import { AppService } from './services/app.service';
import { Subscription } from 'rxjs';
import { IdentityService } from './authentication/shared/services/identity.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  isAuthenticated = false;
  authenticationStatusSubscription: Subscription;

  constructor(
    private appService: AppService,
    private identityService: IdentityService) { }

  ngOnInit(): void {
    this.authenticationStatusSubscription = this.identityService.authenticationStatus$.subscribe(status => {
      this.isAuthenticated = status;
    });
  }

  getClasses() {
    const classes = {
      'pinned-sidebar': this.appService.getSidebarStat().isSidebarPinned,
      'toggeled-sidebar': this.appService.getSidebarStat().isSidebarToggeled
    }
    return classes;
  }
  toggleSidebar() {
    this.appService.toggleSidebar();
  }
}
