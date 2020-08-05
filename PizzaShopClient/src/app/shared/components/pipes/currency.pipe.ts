import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'currency',
  pure: false,
})
export class CurrencyPipe implements PipeTransform {
  transform(value: number, k: number): any {
    return k * value;
  }
}
