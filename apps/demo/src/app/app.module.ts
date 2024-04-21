import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { environment } from '../environments/environment';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

// Material imports
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { MatPaginatorModule } from '@angular/material/paginator';

// Components
import { AppComponent } from './app.component';
import { MazeGeneratorComponent } from './maze-generator/maze-generator.component';
import { MazeListComponent } from './maze-list/maze-list.component';

// Services
import { ValantDemoApiClient } from './api-client/api-client';
import { LoggingService } from './logging/logging.service';
import { ValantService } from './services/valant.service';

export function getBaseUrl(): string {
  return environment.baseUrl;
}

@NgModule({
  declarations: [AppComponent, MazeGeneratorComponent, MazeListComponent],
  imports: [
    BrowserModule, 
    HttpClientModule, 
    BrowserAnimationsModule, 
    MatSnackBarModule,
     MatTableModule,
     MatIconModule,
     MatPaginatorModule
    ],
  providers: [
    LoggingService,
    ValantService,
    ValantDemoApiClient.Client,
    { provide: ValantDemoApiClient.API_BASE_URL, useFactory: getBaseUrl },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
