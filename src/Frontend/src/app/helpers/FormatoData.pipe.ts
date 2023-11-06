import { DatePipe as AngularDatePipe, DatePipe } from '@angular/common';
import { Pipe as AngularPipe, PipeTransform as AngularPipeTransform } from '@angular/core';
import { Constants as AppConstants } from '../util/constants';

@AngularPipe({
  name: 'FormatoData'
})
export class FormatoDataPipe extends DatePipe implements AngularPipeTransform {

  override transform(value?: any, args?: any): any {
    return super.transform(value, AppConstants.DATE_FMT);
  }

}
