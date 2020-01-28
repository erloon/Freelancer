import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ConfigService {

  constructor() { }
  get authApiURI() {
    return "http://localhost:5000/api";
  }

}
