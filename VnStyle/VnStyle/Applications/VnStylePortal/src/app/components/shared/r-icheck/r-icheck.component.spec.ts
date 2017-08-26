/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { RIcheckComponent } from './r-icheck.component';

describe('RIcheckComponent', () => {
  let component: RIcheckComponent;
  let fixture: ComponentFixture<RIcheckComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RIcheckComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RIcheckComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
