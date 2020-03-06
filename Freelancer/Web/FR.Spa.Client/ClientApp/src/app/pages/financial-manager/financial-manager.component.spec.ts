import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FinancialManagerComponent } from './financial-manager.component';

describe('FinancialManagerComponent', () => {
  let component: FinancialManagerComponent;
  let fixture: ComponentFixture<FinancialManagerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FinancialManagerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FinancialManagerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
