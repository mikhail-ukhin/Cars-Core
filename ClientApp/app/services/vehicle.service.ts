import { Http } from '@angular/http';
import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';

@Injectable()
export class VehicleService {

  constructor(private http: Http) { }

  getMakes() {
    return this.http.get('/api/vehicles/makes').map(
      result => result.json()
    );
  }

  getFeatures() {
    return this.http.get("/api/vehicles/features").map(result => result.json());
  }
}
