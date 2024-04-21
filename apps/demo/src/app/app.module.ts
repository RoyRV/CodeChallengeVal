import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { LoggingService } from './logging/logging.service';
import { StuffService } from './stuff/stuff.service';
import { environment } from '../environments/environment';
import { ValantDemoApiClient } from './api-client/api-client';
import { MazeGeneratorComponent } from './maze-generator/maze-generator.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSnackBarModule } from '@angular/material/snack-bar';

export function getBaseUrl(): string {
  return environment.baseUrl;
}

@NgModule({
  declarations: [AppComponent, MazeGeneratorComponent],
  imports: [
    BrowserModule, 
    HttpClientModule,
     BrowserAnimationsModule,
     MatSnackBarModule
    ],
  providers: [
    LoggingService,
    StuffService,
    ValantDemoApiClient.Client,
    { provide: ValantDemoApiClient.API_BASE_URL, useFactory: getBaseUrl },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
