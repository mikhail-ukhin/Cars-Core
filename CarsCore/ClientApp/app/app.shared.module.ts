import { VehicleService } from './services/vehicle.service';
import * as Raven from 'raven-js';

import { NgModule, ErrorHandler } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';


import {ToastModule} from 'ng2-toastr/ng2-toastr';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';
import { VehicleFormComponent } from './components/vehicle-form/vehicle-form.component';
import { AppErrorHandler } from './app.error-handler';

Raven
  .config('https://d1f1e7cdd892461ba9c1dc16439cb5ef@sentry.io/1202539')
  .install();

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
        HomeComponent,
        VehicleFormComponent
    ],
    imports: [

        ToastModule.forRoot(),

        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: 'vehicles/new', component: VehicleFormComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [
        {
            provide: ErrorHandler, useClass: AppErrorHandler
        },
        VehicleService
    ]
})
export class AppModuleShared {
}