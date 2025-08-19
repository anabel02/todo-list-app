import {Pipe, PipeTransform} from '@angular/core';

@Pipe({name: 'isNullOrWhitespace'})
export class IsNullOrWhitespacePipe implements PipeTransform {
  transform(value: string | null | undefined): boolean {
    return !value || value.trim().length === 0;
  }
}
