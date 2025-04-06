import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'place',
})
export class PlacePipe implements PipeTransform {

  transform(value: number, ...args: any[]): string {
    if (value <= 0) {
      return 'Not found';
    }
    return `${value}`; 
  }

}
