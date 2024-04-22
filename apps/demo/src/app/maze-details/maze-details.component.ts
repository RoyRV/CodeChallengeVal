import { Component, OnInit } from '@angular/core';
import { Maze } from '../entities/maze';
import { ActivatedRoute,Router  } from '@angular/router';
import { ValantService } from '../services/valant.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'valant-maze-details',
  templateUrl: './maze-details.component.html',
  styleUrls: ['./maze-details.component.less']
})
export class MazeDetailsComponent implements OnInit {
  mazeId:string;
  maze: Maze;
  errorType: string = 'error-snackbar';
  warningType: string = 'warning-snackbar';

  constructor(
    private valantService: ValantService,  
    private route: ActivatedRoute,
    private router: Router,
    private _snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.mazeId = params['id']; 
      this.getDetails(this.mazeId); 
    });
  }

  private getDetails(mazeId: string) {
    this.valantService.getMazeById(mazeId).subscribe({
      next: (response: string[]) => {
        if(response == null){
          this.showAlert(`Maze with fileName ${mazeId} does not exists.`, this.warningType);
          // Redirect if not found
          setTimeout(() => { this.router.navigate(['/']); }, 3000); 
          return;         
        }
        this.handleMaze(response);
      },
      error: (error) => {
        this.showAlert('Failed to retrieve maze info', this.errorType);
      },
    });
  }

  private handleMaze(response: string[]) {
    
  }

  private showAlert(errorMessage: string, className: string) {
    this._snackBar.open(errorMessage, 'Close', {
      duration: 2000, // Duration in milliseconds
      verticalPosition: 'top', // Position of the alert
      panelClass: [className],
    });
  }
}
