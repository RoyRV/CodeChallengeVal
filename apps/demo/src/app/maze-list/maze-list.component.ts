import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';

import { Router } from '@angular/router';

import { Maze } from '../entities/maze';
import { ValantService } from '../services/valant.service';
import { ValantDemoApiClient } from '../api-client/api-client';

@Component({
  selector: 'valant-maze-list',
  templateUrl: './maze-list.component.html',
  styleUrls: ['./maze-list.component.less'],
})
export class MazeListComponent implements OnInit {
  DEFAULT_SIZE = 4;
  START_INDEX = 0;
  pageSizeOptions: number[] = [2, 4, 6];

  displayedColumns: string[] = ['FileName', 'actions'];
  dataSource = new MatTableDataSource<Maze>([]);
  totalItems: number = 0;
  warningType: string = 'warning-snackbar';
  errorType: string = 'error-snackbar';

  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(
    private valantService: ValantService,
    private router: Router,
    private _snackBar: MatSnackBar) {}

  ngOnInit(): void {
    this.dataSource.paginator = this.paginator;
    var request: ValantDemoApiClient.GetMazesRequest = {
      startIndex: this.START_INDEX,
      size : this.DEFAULT_SIZE
    };
    this.loadData(request);
  }

  loadData(request : ValantDemoApiClient.GetMazesRequest) {
    this.valantService.getAllMazes(request).subscribe({
      next: (response: ValantDemoApiClient.GetMazesResponse) => {
        if (response !=null && response.total <= 0) {
          this.showFileTypeErrorAlert('There are none mazes available', this.warningType);
          return;
        }
        const mazes: Maze[] = response.items.map((item) => ({ FileName: item }));

        // Assign new data to the dataSource
        this.dataSource.data = mazes;
        this.totalItems = response.total;
      },
      error: (error) => {
        this.showFileTypeErrorAlert('Error getting mazes: ' + error, this.errorType);
      },
    });
  }

  // Method to refresh the todo list
  refresh(): void {
    var request: ValantDemoApiClient.GetMazesRequest = {
      startIndex: this.START_INDEX,
      size : this.DEFAULT_SIZE
    };
    this.loadData(request);
  }

  onPageChange(event: PageEvent) {
    const startIndex = event.pageIndex * event.pageSize;

    var request: ValantDemoApiClient.GetMazesRequest = {
      startIndex: startIndex,
      size : event.pageSize
    };

    this.loadData(request); 
  }

  viewMaze(maze: Maze) {
    this.router.navigate(['maze', maze.FileName]);
  }

  deleteMaze(maze: Maze) {
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
