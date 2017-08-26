import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthSilentRenewComponent } from './auth-silent-renew.component';

describe('AuthSilentRenewComponent', () => {
  let component: AuthSilentRenewComponent;
  let fixture: ComponentFixture<AuthSilentRenewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AuthSilentRenewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AuthSilentRenewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
