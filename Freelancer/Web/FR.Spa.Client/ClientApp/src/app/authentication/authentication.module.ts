import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { AuthenticationRoutingModule } from './authentication-routing.module';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthCallbackComponent } from './components/auth-callback/auth-callback.component';

@NgModule({
  declarations: [LoginComponent, RegisterComponent, AuthCallbackComponent],
  imports: [
    CommonModule,
    AuthenticationRoutingModule,
    CollapseModule,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class AuthenticationModule { }
