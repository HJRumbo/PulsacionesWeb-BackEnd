import { Component, OnInit } from '@angular/core';
import { Persona } from './../models/persona';
import { PersonaService } from './../../services/persona.service';
import { FormGroup, FormBuilder, Validators, AbstractControl} from '@angular/forms';

@Component({
  selector: 'app-persona-registro',
  templateUrl: './persona-registro.component.html',
  styleUrls: ['./persona-registro.component.css']
})
export class PersonaRegistroComponent implements OnInit {

  formGroup: FormGroup;
  persona:  Persona;
  constructor(private personaService: PersonaService, private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.buildForm();
  }

  private buildForm(){
    this.persona = new Persona();
    this.persona.identificacion = '';
    this.persona.nombre = '';
    this.persona.edad = 0;
    this.persona.sexo = '';
    this.persona.pulsaciones = 0;

    this.formGroup = this.formBuilder.group({
      identificacion: [this.persona.identificacion, Validators.required],
      nombre: [this.persona.nombre, Validators.required],
      sexo: [this.persona.sexo, [Validators.required, this.validaSexo]],
      edad: [this.persona.edad, [Validators.required, Validators.min(1)]]
    });
  }

  private validaSexo(control: AbstractControl){
    const sexo = control.value;
    if(sexo.toLocaleUpperCase() !== 'M' && sexo.toLocaleUpperCase() !== 'F'){
      return { validSexo: true, messageSexo: 'Sexo no valido'};
    }else{
      return null;
    }
  }

  get control(){

    return this.formGroup.controls;
  }

  onSubmit(){
    if(this.formGroup.invalid){
      return;
    }
    this.add();
  }

  add() {

    this.persona = this.formGroup.value;
    this.personaService.post(this.persona).subscribe(p => {
      if (p != null) {
        alert('Persona creada!');
        this.persona = p;

      }else{
        alert('Error.');
      }
    });
  }

}
