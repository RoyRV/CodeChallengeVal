import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ValantDemoApiClient } from '../api-client/api-client';

@Injectable({
  providedIn: 'root',
})
export class ValantService {
  constructor(private httpClient: ValantDemoApiClient.Client) {}

  public getAvailableMoves(): Observable<string[]> {
    return this.httpClient.availableMoves();
  }
  
  public uploadMaze(request:ValantDemoApiClient.UploadMazeRequest): Observable<boolean> {
    return this.httpClient.upload(request);
  }
}
