import { Pipe, PipeTransform } from '@angular/core';
import { Prospecto } from '../shared/prospecto.model';
import { CapturaService } from '../shared/captura.service';

@Pipe({
  name: 'searchfilter'
})
export class SearchfilterPipe implements PipeTransform {


  transform(list: Prospecto[], searchValue: string): Prospecto[] {
    if (!list || !searchValue) {
      return list;
    }
    return list.filter(obj =>
      obj.nombre.toLocaleLowerCase().includes(searchValue.toLocaleLowerCase())
      || obj.apellidoPaterno.toLocaleLowerCase().includes(searchValue.toLocaleLowerCase())
      || obj.apellidoMaterno.toLocaleLowerCase().includes(searchValue.toLocaleLowerCase())
      || obj.calle.toLocaleLowerCase().includes(searchValue.toLocaleLowerCase())
      || obj.numeroCalle.toLocaleLowerCase().includes(searchValue.toLocaleLowerCase())
      || obj.colonia.toLocaleLowerCase().includes(searchValue.toLocaleLowerCase())
      || obj.cp.toLocaleLowerCase().includes(searchValue.toLocaleLowerCase())
      || obj.telefono.toLocaleLowerCase().includes(searchValue.toLocaleLowerCase())
      || obj.rfc.toLocaleLowerCase().includes(searchValue.toLocaleLowerCase())
      || obj.estatus.toLocaleLowerCase().includes(searchValue.toLocaleLowerCase())
    );

  }

}
