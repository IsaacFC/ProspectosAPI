import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { FormGroup, FormControl } from '@angular/forms';
import { Prospecto } from './prospecto.model';

@Injectable({
  providedIn: 'root'
})

export class CapturaService {

  constructor(private http: HttpClient) { }

  formData: Prospecto = new Prospecto();
  formDataListado: Prospecto = new Prospecto();
  formDataEvaluacion: Prospecto = new Prospecto();

  readonly baseURL = "http://localhost:49447/api/Prospecto";

  list: Prospecto[];

  postCliente() {
    return this.http.post(this.baseURL, this.formData, { responseType: 'text' });
  }

  putCliente() {
    return this.http.put(`${this.baseURL}/${this.formDataEvaluacion.idProspecto}`, this.formDataEvaluacion, { responseType: 'text' });
  }

  deleteCliente(id: number) {
    return this.http.delete(`${this.baseURL}/${id}`, { responseType: 'text' });
  }

  refreshList() {
    return this.http.get(this.baseURL)
      .toPromise()
      .then(res =>
        this.list = res as Prospecto[]);
  }
  download(id: any) {
    console.log(id);
    return this.http.get(`${this.baseURL}/archivos/${id}`, { responseType: 'blob' });

  }
}
