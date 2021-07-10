import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProspectoCapturaComponent } from './prospecto-captura/prospecto-captura.component';
import { ProspectoEvaluacionComponent } from './prospecto-evaluacion/prospecto-evaluacion.component';
import { ProspectoListadoComponent } from './prospecto-listado/prospecto-listado.component';

const routes: Routes = [
  { path: 'Captura', component: ProspectoCapturaComponent},
  { path: 'Listado', component: ProspectoListadoComponent},
  { path: 'Evaluacion', component: ProspectoEvaluacionComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
export const routingComponents = [ProspectoCapturaComponent, ProspectoListadoComponent, ProspectoEvaluacionComponent]
