import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ValantService } from '../services/valant.service';
import { ValantDemoApiClient } from '../api-client/api-client';

@Component({
  selector: 'valant-maze-generator',
  templateUrl: './maze-generator.component.html',
  styleUrls: ['./maze-generator.component.less'],
})
export class MazeGeneratorComponent implements OnInit {
  allowedFileTypes = ['text/plain'];
  mazeFile: File = null;

  constructor(private valantService: ValantService, private _snackBar: MatSnackBar) {}

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

  async handleGenerateClick() {
    if (this.mazeFile == null) {
      this.showFileTypeErrorAlert('Please upload a maze file first');
      return;
    }
    await this.uploadFile(this.mazeFile);
  }

  private async uploadFile(file: File) {
    const text: string = await this.readFile(this.mazeFile);
    const lines: string[] = text.split('\n').map((line) => line.replace(/\r/g, ''));

    var request: ValantDemoApiClient.UploadMazeRequest = {
      fileName: file.name,
      mazeFile: lines,
    };

    this.valantService.uploadMaze(request).subscribe({
      next: (response: boolean) => {
        if (response) {
          this.showFileTypeErrorAlert('Successfully upload');
        } else {
          this.showFileTypeErrorAlert('Invalid format');
        }
      },
      error: (error) => {
        this.showFileTypeErrorAlert('Error getting stuff: ' + error);
      },
    });
  }

  private readFile(file: File): Promise<string> {
    return new Promise((resolve, reject) => {
      const reader: FileReader = new FileReader();
      reader.onload = () => {
        resolve(reader.result as string);
      };
      reader.onerror = reject;
      reader.readAsText(file);
    });
  }

  private showFileTypeErrorAlert(errorMessage: string) {
    this._snackBar.open(errorMessage, 'Close', {
      duration: 5000, // Duration in milliseconds
      verticalPosition: 'top', // Position of the alert
    });
  }
}
