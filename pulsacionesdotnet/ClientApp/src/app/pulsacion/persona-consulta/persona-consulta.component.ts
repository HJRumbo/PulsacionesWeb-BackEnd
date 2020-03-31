import { Component, OnInit } from '@angular/core';
import { Persona } from '../models/persona';
import { PersonaService } from './../../services/persona.service';
import {Observable, of} from 'rxjs';

@Component({
  selector: 'app-persona-consulta',
  templateUrl: './persona-consulta.component.html',
  styleUrls: ['./persona-consulta.component.css']
})
export class PersonaConsultaComponent implements OnInit {

  personas : Persona[];
  searchText:string;
  constructor(private personaServicio: PersonaService) { }

  ngOnInit() {
    this.get();
  }

  get(){

    this.personaServicio.get().subscribe(result => {
      this.personas = result;
    })
}
}
