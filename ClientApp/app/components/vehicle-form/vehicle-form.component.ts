import { VehicleService } from './../../services/vehicle.service';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {

  constructor(
    private vehicleService: VehicleService) { }

  makes: any[] = [];
  vehicle: any = {};
  models: any[] = [];
  features: any[] = [];

  ngOnInit(): void {
    this.vehicleService.getMakes().subscribe(makes => this.makes = makes);
    this.vehicleService.getFeatures().subscribe(features => this.features = features);
  }

  onMakeChange() {
    let selectedMake = this.makes.find(m => m.id == this.vehicle.make);

    this.models = selectedMake ? selectedMake.models : [];
  }
}
