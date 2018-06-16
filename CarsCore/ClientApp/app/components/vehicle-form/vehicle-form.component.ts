import {VehicleService} from '../../services/vehicle.service';
import {Component, OnInit, ViewContainerRef} from '@angular/core';
import { ToastsManager } from 'ng2-toastr/ng2-toastr';

@Component({
    selector: 'app-vehicle-form',
    templateUrl: './vehicle-form.component.html',
    styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {

    constructor(private vehicleService: VehicleService, public toastr: ToastsManager, vcr: ViewContainerRef) {
        this.toastr.setRootViewContainerRef(vcr);
    }

    makes: any[] = [];
    vehicle: any = {
        features: [],
        contact: {}
    };
    models: any[] = [];
    features: any[] = [];

    ngOnInit(): void {
        this.vehicleService.getMakes().subscribe(makes => this.makes = makes);
        this.vehicleService.getFeatures().subscribe(features => this.features = features);
    }

    onMakeChange() {
        let selectedMake = this.makes.find(m => m.id == this.vehicle.makeId);

        delete this.vehicle.modelId;
        this.models = selectedMake ? selectedMake.models : [];
    }

    onFeatureToggle(id: string, $event: any) {
        let features = this.vehicle.features;

        if ($event.target.checked) {
            features.push(id);
        }
        else {
            let index = features.indexOf(id);

            if (index != -1) {
                features.splice(index, 1);
            }
        }
    }
    
    onSubmit() {
        
        this.vehicleService.create(this.vehicle).subscribe(result => {
            this.toastr.success('Новый автомобиль добавлен', 'Операция выполнена!');
        });
    }
}
