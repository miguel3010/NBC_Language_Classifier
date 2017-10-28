import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PreditionDisplayComponent } from './predition-display.component';

describe('PreditionDisplayComponent', () => {
  let component: PreditionDisplayComponent;
  let fixture: ComponentFixture<PreditionDisplayComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PreditionDisplayComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PreditionDisplayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
