import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MazeGeneratorComponent } from './maze-generator.component';

xdescribe('MazeGeneratorComponent', () => {
  let component: MazeGeneratorComponent;
  let fixture: ComponentFixture<MazeGeneratorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MazeGeneratorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MazeGeneratorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
