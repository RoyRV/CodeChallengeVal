import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MazeDetailsComponent } from './maze-details.component';

describe('MazeDetailsComponent', () => {
  let component: MazeDetailsComponent;
  let fixture: ComponentFixture<MazeDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MazeDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MazeDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
