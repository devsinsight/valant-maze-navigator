import { Component, EventEmitter, Output } from '@angular/core';
import { MazeService } from '../../../services/maze.service';
import { ValantDemoApiClient } from '../../../api-client/api-client';


@Component({
  selector: 'app-maze-upload',
  templateUrl: './maze-upload.component.html',
  styleUrls: ['./maze-upload.component.scss']
})
export class MazeUploadComponent {
  @Output() mazeUploaded = new EventEmitter<{ mazeId: string, grid: string[][], startPosition: ValantDemoApiClient.Position }>();
  selectedFile: File | null = null;

  constructor(private mazeService: MazeService) {}

  onFileSelected(event: any): void {
    this.selectedFile = event.target.files[0];
  }

  onUpload(): void {
    if (!this.selectedFile) {
      alert('Please select a file first!');
      return;
    }

    const reader = new FileReader();
    reader.onload = (e: any) => {
      const fileContent = e.target.result;
      const grid = this.parseFileContent(fileContent);
      const startPosition = this.findStartPosition(grid);
      const mazeId = this.generateMazeId();
      
      this.mazeService.uploadMaze(mazeId, grid, startPosition).subscribe(() => {
        this.mazeUploaded.emit({ mazeId, grid, startPosition });
        alert('Maze uploaded successfully!');
      }, error => {
        alert('Failed to upload maze.');
      });
    };
    reader.readAsText(this.selectedFile);
  }

  parseFileContent(content: string): string[][] {
    const lines = content.split('\n').map(line => line.trim()).filter(line => line.length > 0);
    return lines.map(line => line.split(''));
  }

  findStartPosition(grid: string[][]): ValantDemoApiClient.Position {
    for (let y = 0; y < grid.length; y++) {
      for (let x = 0; x < grid[y].length; x++) {
        if (grid[y][x] === 'S') {
          return { x, y };
        }
      }
    }
    throw new Error('Start position not found in the maze.');
  }

  generateMazeId(): string {
    return 'maze-' + Math.random().toString(36).substr(2, 9);
  }
}
