import { Component, OnInit } from '@angular/core';
import { CapturaService } from '../shared/captura.service';
import { Prospecto } from '../shared/prospecto.model';
import * as fileSaver from 'file-saver';


@Component({
  selector: 'app-prospecto-listado',
  templateUrl: './prospecto-listado.component.html',
  styleUrls: ['./prospecto-listado.component.css']
})
export class ProspectoListadoComponent implements OnInit {
  searchValue: string;

  constructor(public service: CapturaService) { }

  ngOnInit(): void {
    this.service.refreshList();
  }

  populateForm(selectedRecord: Prospecto) {
    this.service.formDataListado = Object.assign({}, selectedRecord);
  }

  download(id: any) {
    this.service.download(id)
      .subscribe(response => {
        let blob: any = new Blob([response], { type: 'text/json; charset=utf-8' });
        const url = window.URL.createObjectURL(blob);
        fileSaver.saveAs(blob, id + ' client.zip');
      }), error => console.log('Error downloading the file'),
      () => console.info('File downloaded successfully');;
  }


}
