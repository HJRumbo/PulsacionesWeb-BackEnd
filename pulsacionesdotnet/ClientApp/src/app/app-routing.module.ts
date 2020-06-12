import { PersonaRegistroComponent } from './pulsacion/persona-registro/persona-registro.component';

import { PersonaConsultaComponent } from './pulsacion/persona-consulta/persona-consulta.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthGuard } from './services/auth.guard';
import { LoginComponent } from './login/login.component';
import { UserRegistroComponent } from './pulsacion/user-registro/user-registro.component';


import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [

{
path: 'personaConsulta',
component: PersonaConsultaComponent, canActivate: [AuthGuard]
},
{
path: 'personaRegistro',
component: PersonaRegistroComponent, canActivate: [AuthGuard]
},
{
  path: 'login', component: LoginComponent
},
{
  path: 'userRegistro', 
  component: UserRegistroComponent
}

];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes)
  ],

  exports:[RouterModule]
})
export class AppRoutingModule { }
