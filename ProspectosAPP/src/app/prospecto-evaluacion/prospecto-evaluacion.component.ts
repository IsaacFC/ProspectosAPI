import { Component, OnInit } from '@angular/core';
import { CapturaService } from '../shared/captura.service';
import { Prospecto } from '../shared/prospecto.model';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import * as fileSaver from 'file-saver';

@Component({
  selector: 'app-prospecto-evaluacion',
  templateUrl: './prospecto-evaluacion.component.html',
  styleUrls: ['./prospecto-evaluacion.component.css']
})
export class ProspectoEvaluacionComponent implements OnInit {
  searchValue: string;

  constructor(public service: CapturaService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.service.refreshList();
  }

  download(id: any) {
    this.service.download(id)
    .subscribe(response => {let blob:any = new Blob([response], { type: 'text/json; charset=utf-8' });
    const url = window.URL.createObjectURL(blob);
    fileSaver.saveAs(blob, id + ' client.zip');
  }), error => console.log('Error downloading the file'),
    () => console.info('File downloaded successfully');;
  }

  populateForm(selectedRecord: Prospecto) {
    this.service.formDataEvaluacion = Object.assign({}, selectedRecord);
  }

  resetForm(form: NgForm) {
    form.form.reset();
    this.service.formData = new Prospecto();
    this.service.formDataEvaluacion = new Prospecto();
  }

  updateStatus(form: NgForm) {

    if ((this.service.formDataEvaluacion.estatus != 'Enviado' 
    && this.service.formDataEvaluacion.observaciones == '')) {
      this.service.putCliente()
        .subscribe(
          res => {
            this.resetForm(form);
            this.service.refreshList();
            this.toastr.info('Estado del Prospecto Actualizado Exitosamente', 'Prospecto Actualizado');
          },
          err => {
            this.resetForm(form);
            this.service.refreshList();
            this.toastr.error('Seleccione un prospecto', err.message);
          }
        );
    } else if (this.service.formDataEvaluacion.estatus != 'Enviado' 
      && this.service.formDataEvaluacion.observaciones != '') {
        this.service.formDataEvaluacion.observaciones = '';
        this.service.putCliente()
        .subscribe(
          res => {
            this.resetForm(form);
            this.service.refreshList();
            this.toastr.info('Estado del Prospecto Actualizado Exitosamente', 'Prospecto Actualizado');
          },
          err => {
            this.resetForm(form);
            this.service.refreshList();
            this.toastr.error('Seleccione un prospecto', err.message);
          }
        );
    } else {
      this.toastr.error('Complete los campos', 'Seleccione un estatus o complete observaciones');
    }
  }
}
