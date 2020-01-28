import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { CollapseModule } from 'ngx-bootstrap/collapse';
import { ToastrModule } from 'ngx-toastr';

import { AppRoutingModule } from './app-routing.module';

import {AuthenticationModule} from './authentication/authentication.module';
import {CoreModule} from './core/core.module';

import { AppComponent } from './app.component';

import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { FinancialManagerComponent } from './pages/financial-manager/financial-manager.component';
import { BalanceComponent } from './pages/balance/balance.component';
import { CompanyProfileComponent } from './pages/company-profile/company-profile.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { from } from 'rxjs';
import { AuthGuard } from './authentication/shared/services/auth.guard';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    FinancialManagerComponent,
    BalanceComponent,
    CompanyProfileComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    CommonModule,
    AuthenticationModule,
    CoreModule,
    BrowserAnimationsModule,
    CollapseModule.forRoot(),
    ToastrModule.forRoot(),
    FormsModule,
    ReactiveFormsModule
    ],
  providers: [AuthGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
