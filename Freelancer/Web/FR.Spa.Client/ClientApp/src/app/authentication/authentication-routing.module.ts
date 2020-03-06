import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RegisterComponent } from './pages/register/register.component';
import { TestContactsComponent } from './components/test-contacts/test-contacts.component';
import { AuthGuard } from './shared/services/auth.guard';


const routes: Routes = [
  { path: 'register', component: RegisterComponent },
  { path: 'testContacts', component: TestContactsComponent, canActivate:[AuthGuard] }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthenticationRoutingModule { }
