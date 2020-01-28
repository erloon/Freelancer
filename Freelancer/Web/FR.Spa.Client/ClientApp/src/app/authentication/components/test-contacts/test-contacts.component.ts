import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ConfigurationService } from 'src/app/shared/configuration.service';
import { IdentityService } from '../../shared/services/identity.service';
import { Contact } from '../../shared/model/contact';

@Component({
  selector: 'app-test-contacts',
  templateUrl: './test-contacts.component.html',
  styleUrls: ['./test-contacts.component.scss']
})
export class TestContactsComponent implements OnInit {

  constructor(private http: HttpClient, private configuration: ConfigurationService, private identityService: IdentityService) { }
  contacts: Contact[];

  ngOnInit() {
  }

  getContacts() {

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': this.identityService.authorizationHeaderValue
      })
    };
    this.http.get<Contact[]>(this.configuration.finaceManagerApiURI + '/Contact', httpOptions)
      .subscribe(
        response => {
          this.contacts = response;
        },
        error => {
          console.log(error);
        }
      )
  }

}

