import { Component, ElementRef, HostListener, Input, OnChanges, OnInit, Renderer2, ViewChild } from '@angular/core';
import { MazeService } from '../../../services/maze.service';
import { ValantDemoApiClient } from '../../../api-client/api-client';

@Component({
  selector: 'app-maze-navigator',
  templateUrl: './maze-navigator.component.html',
  styleUrls: ['./maze-navigator.component.scss']
})
export class MazeNavigatorComponent implements OnInit, OnChanges {
  @Input() mazeId: string | null = null;
  @Input() grid: string[][] = [];
  @Input() startPosition: ValantDemoApiClient.Position | null = null;
  currentPosition: ValantDemoApiClient.Position | null = null;
  movements: string[] = [];

  @ViewChild('mazeContainer', { static: true }) mazeContainer!: ElementRef;

  constructor(
    private mazeService: MazeService,
    private renderer: Renderer2
  ) {}
  
  ngOnInit(): void {
    this.mazeService.getMovements().subscribe((data: string[]) =>{
      this.movements = data;
    })
  }

  ngOnChanges(): void {
    if (this.startPosition) {
      this.currentPosition = this.startPosition;
      this.updateGrid();
    }
  }

  @HostListener('window:keydown', ['$event'])
  handleKeyDown(event: KeyboardEvent): void {
    switch (event.key) {
      case 'ArrowUp':
        this.move('Up');
        break;
      case 'ArrowDown':
        this.move('Down');
        break;
      case 'ArrowLeft':
        this.move('Left');
        break;
      case 'ArrowRight':
        this.move('Right');
        break;
    }
  }

  move(direction: string): void {
    if (!this.mazeId || !this.currentPosition) {
      alert('Please select a maze first!');
      return;
    }

    this.mazeService.navigate(this.mazeId, direction).subscribe(result => {
      this.currentPosition = result.currentPosition;
      this.updateGrid();

      if (result.isAtEnd) {
        alert('Success! You have reached the end of the maze.');
      }
    }, error => {
      this.triggerVibration();
    });
  }

  triggerVibration(): void {
    this.renderer.addClass(this.mazeContainer.nativeElement, 'shake');
    setTimeout(() => {
      this.renderer.removeClass(this.mazeContainer.nativeElement, 'shake');
    }, 500);
  }

  updateGrid(): void {
    
  }
}
