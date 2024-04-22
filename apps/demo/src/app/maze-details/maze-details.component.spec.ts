import { MazeDetailsComponent } from './maze-details.component';
import { Shallow } from 'shallow-render';
import { AppModule } from '../app.module';
import { of } from 'rxjs';
import { ValantService } from '../services/valant.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BrowserAnimationsModule, NoopAnimationsModule } from '@angular/platform-browser/animations';

Shallow.alwaysReplaceModule(BrowserAnimationsModule, NoopAnimationsModule);
const valantServiceMock = { 
  getMazeById: jest.fn(() => of([])) ,
  getAvailableMoves: jest.fn(() => of([])) ,
};
const activatedRouteMock = { params: of({ id: '123' }) };
const routerMock = { navigate: jest.fn() };
const snackBarMock = { open: jest.fn() };

// valantServiceMock.getMazeById.mockReturnValue(of(validResponse));

describe('MazeDetailsComponent', () => {
  let component: Shallow<MazeDetailsComponent>;
  beforeEach(() => {
    component = new Shallow(MazeDetailsComponent, AppModule)
      .provideMock({ provide: ValantService, useValue: valantServiceMock })
      .provideMock({ provide: ActivatedRoute, useValue: activatedRouteMock })
      .provideMock({ provide: Router, useValue: routerMock })
      .provideMock({ provide: MatSnackBar, useValue: snackBarMock });
    jest.clearAllMocks();
  });

  it('should render', async () => {
    const rendering = await component.render();
    expect(rendering).toBeTruthy();
  });

  it('should call getMazeById method from MazeService on initialization', async () => {
    await component.render();
    expect(valantServiceMock.getMazeById).toHaveBeenCalled();
  });

  it('should call the snack bar when response is null', async () => {
    valantServiceMock.getMazeById.mockReturnValue(of(null));
    const rendering = await component.render();
    const instance = rendering.instance;

    // Assert
    expect(instance.maze).toBeUndefined();
    expect(snackBarMock.open).toHaveBeenCalledWith(
      `Maze with fileName 123 does not exists.`,
      expect.anything(),
      expect.anything()
    );
    expect(routerMock.navigate).toHaveBeenCalled();
  });

  it('should call the snack bar when response is not', async () => {
    var validResponse = ['string1', 'string2'];
    valantServiceMock.getMazeById.mockReturnValue(of(validResponse));
    const rendering = await component.render();
    const instance = rendering.instance;

    // Assert
    expect(instance.maze).toEqual(validResponse);
  });

  it('should splitString into chars', async () => {
    var text = "abc";
    var expected = ['a','b','c']

    const rendering = await component.render();
    const instance = rendering.instance;
    var result = instance.splitStringIntoCharacters(text);

    expect(result).toEqual(expected);
  });

  it('should call getAvailableMoves and show possible moves', async () => {
    var getByIdResponse = ['string1', 'string2'];
    valantServiceMock.getMazeById.mockReturnValue(of(getByIdResponse));
    var getAvailableMovesResponse = ['UP', 'DOWN'];
    valantServiceMock.getAvailableMoves.mockReturnValue(of(getAvailableMovesResponse));
    const rendering = await component.render();
    const instance = rendering.instance;
    
    // Act
    instance.checkAvailableMoves(expect.anything(),expect.anything());

    expect(valantServiceMock.getAvailableMoves).toHaveBeenCalled();
    
    expect(snackBarMock.open).toHaveBeenCalledWith(
      `Possible moves : ${getAvailableMovesResponse.join(', ')}!`,
      expect.anything(),
      expect.anything()
    );
  });

  it('should call getAvailableMoves and show warning', async () => {
    var getByIdResponse = ['string1', 'string2'];
    valantServiceMock.getMazeById.mockReturnValue(of(getByIdResponse));
    var getAvailableMovesResponse = [];
    valantServiceMock.getAvailableMoves.mockReturnValue(of(getAvailableMovesResponse));
    const rendering = await component.render();
    const instance = rendering.instance;
    
    // Act
    instance.checkAvailableMoves(expect.anything(),expect.anything());

    expect(valantServiceMock.getAvailableMoves).toHaveBeenCalled();
    
    expect(snackBarMock.open).toHaveBeenCalledWith(
      `No Possible moves!`,
      expect.anything(),
      expect.anything()
    );
  });
});
