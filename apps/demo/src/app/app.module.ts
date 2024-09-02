import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { LoggingService } from './logging/logging.service';
import { StuffService } from './stuff/stuff.service';
import { environment } from '../environments/environment';
import { ValantDemoApiClient } from './api-client/api-client';
import { MazeNavigatorComponent } from './components/maze/maze-navigator/maze-navigator.component';
import { MazeService } from './services/maze.service';
import { AppRoutingModule } from './app.routing';
import { MazeUploadComponent } from './components/maze/maze-upload/maze-upload.component';
import { MazeComponent } from './components/maze/maze.component';
import { MazeSelectorComponent } from './components/maze/maze-selector/maze-selector.component';

export function getBaseUrl(): string {
  return environment.baseUrl;
}

@NgModule({
  declarations: [
    AppComponent,
    MazeNavigatorComponent,
    MazeSelectorComponent,
    MazeUploadComponent,
    MazeComponent
  ],
  imports: [BrowserModule, HttpClientModule, AppRoutingModule],
  providers: [
    LoggingService,
    StuffService,
    MazeService,
    ValantDemoApiClient.Client,
    { provide: ValantDemoApiClient.API_BASE_URL, useFactory: getBaseUrl },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
