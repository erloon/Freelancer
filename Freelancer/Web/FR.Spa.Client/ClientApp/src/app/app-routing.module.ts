import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { FinancialManagerComponent } from './pages/financial-manager/financial-manager.component';
import { BalanceComponent } from './pages/balance/balance.component';
import { CompanyProfileComponent } from './pages/company-profile/company-profile.component';
import { RegisterComponent } from './authentication/pages/register/register.component';
import { AuthCallbackComponent } from './authentication/components/auth-callback/auth-callback.component';

const routes: Routes = [
 // {path: '',   redirectTo: '/dashboard', pathMatch: 'full'},
 { path: 'auth-callback', component: AuthCallbackComponent  },
  {path: 'dashboard', component: DashboardComponent},
  {path: 'financialManager', component: FinancialManagerComponent},
  {path: 'balance', component: BalanceComponent},
  {path: 'companyProfile', component: CompanyProfileComponent},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
