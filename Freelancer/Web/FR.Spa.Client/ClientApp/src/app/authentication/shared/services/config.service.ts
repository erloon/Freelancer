import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ConfigService {

  constructor() { }
  get authApiURI() {
    return "http://localhost:5000/api";
  }

  get financeManagerApiUrl() {
    return "http://localhost:5015/api";
  }
}
