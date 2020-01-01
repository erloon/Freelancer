import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CollapseModule } from 'ngx-bootstrap/collapse';

import { CoreRoutingModule } from './core-routing.module';

import { NavbarComponent } from './components/navbar/navbar.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { FooterComponent } from './components/footer/footer.component';
import { IndexComponent } from './pages/index/index.component';
import { StartComponent } from './pages/start/start.component';

@NgModule({
  declarations: [NavbarComponent, SidebarComponent, FooterComponent, IndexComponent, StartComponent],
  exports: [NavbarComponent, SidebarComponent, FooterComponent, StartComponent],
  imports: [
    CommonModule,
    CoreRoutingModule,
    CollapseModule
  ]
})
export class CoreModule { }
