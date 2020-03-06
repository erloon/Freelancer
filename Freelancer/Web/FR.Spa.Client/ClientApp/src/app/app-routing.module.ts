import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { FinancialManagerComponent } from './pages/financial-manager/financial-manager.component';
import { BalanceComponent } from './pages/balance/balance.component';
import { CompanyProfileComponent } from './pages/company-profile/company-profile.component';
import { RegisterComponent } from './authentication/pages/register/register.component';
import { AuthCallbackComponent } from './authentication/components/auth-callback/auth-callback.component';
import { AuthGuard } from './authentication/shared/services/auth.guard';

const routes: Routes = [
  // {path: '',   redirectTo: '/dashboard', pathMatch: 'full'},
  { path: 'auth-callback', component: AuthCallbackComponent },
  { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard] },
  { path: 'financialManager', component: FinancialManagerComponent, canActivate: [AuthGuard]},
  { path: 'balance', component: BalanceComponent, canActivate: [AuthGuard]},
  { path: 'companyProfile', component: CompanyProfileComponent, canActivate: [AuthGuard]},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
