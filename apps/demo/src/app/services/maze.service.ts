import { Injectable } from '@angular/core';
import { ValantDemoApiClient } from '../api-client/api-client';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MazeService {
  constructor(private apiService: ValantDemoApiClient.Client) {} // Usa el servicio generado

  uploadMaze(mazeId: string, grid: string[][], startPosition: ValantDemoApiClient.Position): Observable<void> {
    const request = {
      mazeId,
      grid,
      startPosition
    };
    return this.apiService.upload(request);
  }

  navigate(mazeId: string, direction: string): Observable<ValantDemoApiClient.MazeNavigationResult> {
    const request = {
      mazeId,
      direction
    };
    return this.apiService.navigate(request);
  }

  getMazeState(mazeId: string): Observable<ValantDemoApiClient.MazeStateDto> {
    return this.apiService.state(mazeId);
  }

  getMovements() {
    return this.apiService.maze();
  }

}
