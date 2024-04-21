import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MazeIndexComponent } from './maze-index.component';

describe('MazeIndexComponent', () => {
  let component: MazeIndexComponent;
  let fixture: ComponentFixture<MazeIndexComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MazeIndexComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MazeIndexComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
