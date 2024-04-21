import { Component, OnInit, ViewChild } from '@angular/core';
import { LoggingService } from './logging/logging.service';
import { MazeListComponent } from './maze-list/maze-list.component';

@Component({
  selector: 'valant-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less'],
})
export class AppComponent implements OnInit { 
  @ViewChild('mazeList') mazeList!: MazeListComponent;

  constructor(private logger: LoggingService) {}

  ngOnInit() {
    this.logger.log('Welcome to the AppComponent'); 
  }   

  onMazeAdded(): void {
    // Call the refreshTodoList method in TodoListComponent
    this.mazeList.refresh();
  }
}
