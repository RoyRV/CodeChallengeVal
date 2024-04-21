import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { Maze } from '../entities/maze';
import { ValantService } from '../services/valant.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'valant-maze-list',
  templateUrl: './maze-list.component.html',
  styleUrls: ['./maze-list.component.less'],
})
export class MazeListComponent implements OnInit {
  displayedColumns: string[] = ['FileName', 'actions'];
  dataSource = new MatTableDataSource<Maze>([]);
  pageSizeOptions: number[] = [5, 10, 25, 100];
  totalItems: number = 0;
  warningType: string = 'warning-snackbar';
  errorType: string = 'error-snackbar';

  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(private valantService: ValantService,private _snackBar: MatSnackBar) {}

  ngOnInit(): void {
    this.dataSource.paginator = this.paginator;
    this.loadData();
  }
  loadData() {
    this.valantService.getAllMazes().subscribe({
      next: (response: string[]) => {
        if (response.length <= 0) {
          this.showFileTypeErrorAlert('There are none mazes available', this.warningType);
          return;
        }
        const mazes: Maze[] = response.map(fileName => ({ FileName: fileName }));

        // Assign new data to the dataSource
        this.dataSource.data = mazes;
        this.totalItems = mazes.length;
      },
      error: (error) => {
        this.showFileTypeErrorAlert('Error getting mazes: ' + error, this.errorType);
      },
    }); 

  }

  // Method to refresh the todo list
  refresh(): void {
    console.log("refresh");
    this.loadData();
  }

  onPageChange(event: PageEvent) {
    this.loadData();
    const startIndex = event.pageIndex * event.pageSize;

    const endIndex = Math.min(startIndex + event.pageSize, this.totalItems);

    const pageItems = this.dataSource.data.slice(startIndex, endIndex);

    this.dataSource.data = pageItems;
  }

  viewMaze(maze: Maze) {
    // Implement view logic here, for example, navigate to a detail page
    console.log('View maze:', maze.FileName);
  }

  deleteMaze(maze: Maze) {
    // Implement delete logic here, for example, remove item from dataSource
    console.log('Delete maze:', maze.FileName);
  }

  
  private showFileTypeErrorAlert(errorMessage: string, className: string) {
    this._snackBar.open(errorMessage, 'Close', {
      duration: 2000, // Duration in milliseconds
      verticalPosition: 'top', // Position of the alert
      panelClass: [className],
    });
  }
}
