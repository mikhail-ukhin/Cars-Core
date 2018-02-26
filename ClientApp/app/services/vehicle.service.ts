import { Http } from '@angular/http';
import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';

@Injectable()
export class VehicleService {

  constructor(private http: Http) { }

  getMakes() {
    return this.http.get('/api/vehicle/makes').map(
      result => result.json()
    );
  }

  getFeatures() {
    return this.http.get("/api/vehicle/features").map(result => result.json());
  }
}
