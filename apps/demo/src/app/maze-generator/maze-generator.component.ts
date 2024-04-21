import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { MatSnackBar, MatSnackBarRef } from '@angular/material/snack-bar';
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
  hasWarnings: any = false;
  errorType: string = 'error-snackbar';
  warningType: string = 'warning-snackbar';
  successType: string = 'success-snackbar';
  @Output() mazeAdded:EventEmitter<any> = new EventEmitter<any>();


  constructor(private valantService: ValantService, private _snackBar: MatSnackBar) {}

  ngOnInit(): void {}

  async uploadMaze(files: FileList) {
    this.mazeFile = null;
    this.hasWarnings = false;
    if (files.length === 0) {
      this.showFileTypeErrorAlert('Multiple file uploads are not allowed.', this.errorType);
      return;
    }

    const file: File = files[0];
    if (file) {
      if (!this.allowedFileTypes.includes(file.type)) {
        this.showFileTypeErrorAlert('Only TXT files are allowed.', this.errorType);
        return;
      }
      if (!(await this.isValidContent(file))) {
        return;
      }
      this.mazeFile = file;
    }
  }

  async handleGenerateClick() {
    if (this.mazeFile == null) {
      this.showFileTypeErrorAlert('Please upload a maze file first', this.errorType);
      return;
    }
    await this.uploadFile(this.mazeFile);
  }

  private async uploadFile(mazeFile: File) {
    // const confirmed = await this.openConfirmationDialog("There are warnings, still want to proceed ?");
    // if (!confirmed) {
    //   return;
    // }  

    const lines: string[] = await this.getFileText(mazeFile);

    var request: ValantDemoApiClient.UploadMazeRequest = {
      fileName: mazeFile.name,
      mazeFile: lines,
    };

    this.valantService.uploadMaze(request).subscribe({
      next: (response: boolean) => {
        if (response) {
          this.showFileTypeErrorAlert('Successfully upload', this.successType);
          this.mazeAdded.emit(true); // Emit event to notify parent component (app-component)
        } else {
          this.showFileTypeErrorAlert('Invalid format', this.errorType);
        }
      },
      error: (error) => {
        this.showFileTypeErrorAlert('Error uploading maze file: ' + error, this.errorType);
      },
    });
  }

  private async isValidContent(mazeFile: File): Promise<boolean> {
    const lines: string[] = await this.getFileText(mazeFile);
    const pattern: RegExp = /^[\sSOXE]*$/i;
    if (!lines.every((item) => pattern.test(item))) {
      this.showFileTypeErrorAlert('Format should only contains characters as S, O, X, E', this.errorType);
      return false;
    }
    // Check if every string has the same length, first get the max length in the items
    const maxLength: number = lines.reduce((max, str) => Math.max(max, str.trim().length), 0);

    // Check if the item has the same length
    var hasSameLength = lines.every((item) => item.trim().length === maxLength);
    if (!hasSameLength) {
      this.hasWarnings = true;
      this.showFileTypeErrorAlert(
        'Not all the lines has the same length, missing spaces are going to be replaced with X.',
        this.warningType
      );
    }
    return true;
  }

  private async getFileText(mazeFile: File): Promise<string[]> {
    const text: string = await this.readFile(mazeFile);
    return text.split('\n').map((line) => line.replace(/\r/g, ''));
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

  private showFileTypeErrorAlert(errorMessage: string, className: string) {
    this._snackBar.open(errorMessage, 'Close', {
      duration: 2000, // Duration in milliseconds
      verticalPosition: 'top', // Position of the alert
      panelClass: [className],
    });
  }

  private openConfirmationDialog(message: string): Promise<boolean> {
    return new Promise<boolean>((resolve, reject) => {
      const snackBarRef = this._snackBar.open(message, 'Confirm', {
        duration: 0, // Set duration to 0 to make the snackbar persistent until action is taken
        panelClass: ['custom-snackbar'], // Custom CSS class for styling
        data: { hasDeclineButton: true } // Pass data to indicate the presence of a Decline button
      });

      snackBarRef.onAction().subscribe(() => {
        snackBarRef.dismiss();
        resolve(true); // User confirmed action
      });

      snackBarRef.afterDismissed().subscribe((data) => {
        if (!data.dismissedByAction) {
          resolve(false); // User declined action
        }
      });
    });
  }
}
