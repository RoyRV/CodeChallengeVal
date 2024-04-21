import { Component, OnInit, ViewChild } from '@angular/core';
import { MazeListComponent } from '../maze-list/maze-list.component';

@Component({
  selector: 'valant-maze-index',
  templateUrl: './maze-index.component.html',
  styleUrls: ['./maze-index.component.less']
})
export class MazeIndexComponent implements OnInit {
  @ViewChild('mazeList') mazeList!: MazeListComponent;

  constructor() { }

  ngOnInit(): void {
  }
  
  onMazeAdded(): void {
    // Call the refreshTodoList method in TodoListComponent
    this.mazeList.refresh();
  }

}
