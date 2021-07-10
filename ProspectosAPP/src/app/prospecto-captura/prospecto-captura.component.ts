import { HttpClient, HttpEventType, HttpRequest, HttpResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';


import { CapturaService } from '../shared/captura.service';
import { Prospecto } from '../shared/prospecto.model';

import { finalize } from 'rxjs/operators';



interface ResultadosDeCarga {
  fileName: string;
  fileSize: number;
}

@Component({
  selector: 'app-prospecto-captura',
  templateUrl: './prospecto-captura.component.html',
  styleUrls: ['./prospecto-captura.component.css']
})

export class ProspectoCapturaComponent implements OnInit {
  studentId = 9998;
  uploadProgress = 0;
  selectedFiles: File[];
  uploading = false;
  errorMsg = '';
  obj: any;
  submissionResults: ResultadosDeCarga[] = [];


  constructor(public service: CapturaService,
    private toastr: ToastrService, private readonly httpClient: HttpClient) { }

  ngOnInit(): void {
  }


  async onSubmit(form: NgForm) {
    let id = await this.insertRecord(form);
    await this.upload(id);
    this.obj = '';
  }

  async insertRecord(form: NgForm) {
    if (!this.selectedFiles || this.selectedFiles.length === 0) {
      this.toastr.error('No hay archivos seleccionados', 'Seleccione archivos');
      console.log('error al subir')
      return;
    }
    
    return new Promise((resolve, reject) => {
      this.service.postCliente()
        .subscribe(
          res => {
            console.log(res);
            this.obj = JSON.parse(res);
            console.log(this.obj.idProspecto);

            this.resetForm(form);
            this.service.refreshList();
            this.toastr.success('Datos del Prospecto Registrados Exitosamente ', 'Prospecto Registrado');
            resolve(this.obj.idProspecto);
          },
          err => {
            this.toastr.error(err.message, 'Error');
          }
        );
  });
      
    


  }


  resetForm(form: NgForm) {
    form.form.reset();
    this.service.formData = new Prospecto();
  }

  // Funcion para solo admitir numeros en input
  keyPressNumbers(event: any) {
    var theEvent = event || window.event;

    // Handle paste
    if (theEvent.type === 'paste') {
      key = event.clipboardData.getData('text/plain');
    } else {
      // Handle key press
      var key = theEvent.keyCode || theEvent.which;
      key = String.fromCharCode(key);
    }
    var regex = /[0-9]/;
    if (!regex.test(key)) {
      theEvent.returnValue = false;
      if (theEvent.preventDefault) theEvent.preventDefault();
    }
  }


  chooseFile(files: FileList) {
    this.selectedFiles = [];
    this.errorMsg = '';
    this.uploadProgress = 0;
    if (files.length === 0) {
      return;
    }
    for (let i = 0; i < files.length; i++) {
      this.selectedFiles.push(files[i]);
    }
  }
  upload(id: any) {

    

    const formData = new FormData();
    this.selectedFiles.forEach((f) => formData.append('certificates', f));

    const req = new HttpRequest(
      'POST',
      `http://localhost:49447/api/Prospecto/${id}`,
      formData,
      {
        reportProgress: true,
      }
    );
    console.log(req);
    this.uploading = true;

    this.httpClient
      .request<ResultadosDeCarga[]>(req)
      .pipe(
        finalize(() => {
          this.uploading = false;
          this.selectedFiles = [];
        })
      )
      .subscribe(
        (event: any) => {
          if (event.type === HttpEventType.UploadProgress) {
            this.uploadProgress = Math.round(
              (100 * event.loaded) / event.total
            );
          } else if (event instanceof HttpResponse) {
            this.submissionResults = event.body as ResultadosDeCarga[];
          }
        },
        (error) => {
          // Here, you can either customize the way you want to catch the errors
          this.toastr.error(error.message, 'Error al subir archivos 2');
        }
      );
  }
  humanFileSize(bytes: number): string {
    if (Math.abs(bytes) < 1024) {
      return bytes + ' B';
    }
    const units = ['kB', 'MB', 'GB', 'TB', 'PB', 'EB', 'ZB', 'YB'];
    let u = -1;
    do {
      bytes /= 1024;
      u++;
    } while (Math.abs(bytes) >= 1024 && u < units.length - 1);
    return bytes.toFixed(1) + ' ' + units[u];
  }

}
