import { Component, OnInit } from '@angular/core';
import { IdentityService } from 'src/app/authentication/shared/services/identity.service';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {

  companyName: string;
  companyOwner: string;

  constructor(
    private identityService: IdentityService
  ) { }

  ngOnInit() {
    this.companyOwner = this.identityService.name;
    let user = this.identityService.getUser;
  }

}
