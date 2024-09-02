import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-maze-selector',
  templateUrl: './maze-selector.component.html',
  styleUrls: ['./maze-selector.component.scss']
})
export class MazeSelectorComponent {
    @Input() availableMazes: string[] = [];
    @Output() mazeSelected = new EventEmitter<string>();
  
    selectMaze(mazeId: string): void {
      this.mazeSelected.emit(mazeId);
    }
}
