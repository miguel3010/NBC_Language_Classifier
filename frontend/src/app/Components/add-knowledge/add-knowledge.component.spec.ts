import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddKnowledgeComponent } from './add-knowledge.component';

describe('AddKnowledgeComponent', () => {
  let component: AddKnowledgeComponent;
  let fixture: ComponentFixture<AddKnowledgeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddKnowledgeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddKnowledgeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
