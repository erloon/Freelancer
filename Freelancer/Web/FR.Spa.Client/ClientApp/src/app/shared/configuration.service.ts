import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ConfigurationService {

  constructor() { }

  get finaceManagerApiURI() {
    return 'http://localhost:5015/api';
  }
}
