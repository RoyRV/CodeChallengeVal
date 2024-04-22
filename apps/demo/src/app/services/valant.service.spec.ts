import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { ValantService } from './valant.service';

xdescribe('ValantService', () => {
  let service: ValantService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
    });
    service = TestBed.inject(ValantService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
