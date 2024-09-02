import { Component } from '@angular/core';
import { ValantDemoApiClient } from '../../api-client/api-client';
import { MazeService } from '../../services/maze.service';

@Component({
  selector: 'app-maze',
  templateUrl: './maze.component.html',
  styleUrls: ['./maze.component.scss']
})
export class MazeComponent {
  availableMazes: string[] = [];
  selectedMazeId: string | null = null;
  grid: string[][] = [];
  startPosition: ValantDemoApiClient.Position | null = null;

  constructor(private mazeService: MazeService) {}

  onMazeUploaded(event: { mazeId: string, grid: string[][], startPosition: ValantDemoApiClient.Position }): void {
    this.availableMazes.push(event.mazeId);
    this.selectedMazeId = event.mazeId;
    this.grid = event.grid;
    this.startPosition = event.startPosition;
  }

  onMazeSelected(mazeId: string): void {
    this.selectedMazeId = mazeId;

    this.mazeService.getMazeState(mazeId).subscribe(state => {
      this.grid = state.grid;
      this.startPosition = state.startPosition;
    }, error => {
      alert('Failed to load the selected maze.');
    });
  }
}
