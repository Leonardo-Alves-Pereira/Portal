import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class SpinnerService {

constructor(private ngxSpinner: NgxSpinnerService) { }

showSpinner(): void {
  this.ngxSpinner.show();
  const hideSpinnerTimeout = setTimeout(() => {
    this.ngxSpinner.hide();
    clearTimeout(hideSpinnerTimeout);
  }, 2000);
}

}
