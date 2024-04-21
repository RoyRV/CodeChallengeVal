import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'valant-maze-generator',
  templateUrl: './maze-generator.component.html',
  styleUrls: ['./maze-generator.component.less'],
})
export class MazeGeneratorComponent implements OnInit {
  allowedFileTypes = ['text/plain'];
  mazeFile: File = null;

  constructor(private _snackBar: MatSnackBar) {}

  ngOnInit(): void {}

  uploadMaze(files: FileList): void {
    if (files.length === 0) {
      this.showFileTypeErrorAlert('Multiple file uploads are not allowed.');
      return;
    }

    const file: File = files[0];
    if (file) {
      if (this.allowedFileTypes.includes(file.type)) {
        this.mazeFile = file;
      } else {
        this.showFileTypeErrorAlert('Only TXT files are allowed.');
      }
    }
  }

  handleGenerateClick() {
    if (this.mazeFile == null) {
      this.showFileTypeErrorAlert('Please upload a maze file first');
      return;
    }
  }

  showFileTypeErrorAlert(errorMessage: string) {
    this._snackBar.open(errorMessage, 'Close', {
      duration: 5000, // Duration in milliseconds
      verticalPosition: 'top', // Position of the alert
    });
  }
}
