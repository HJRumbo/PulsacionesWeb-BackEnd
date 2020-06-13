import { Injectable, EventEmitter, Inject } from '@angular/core';
import { Persona } from '../pulsacion/models/persona';
import { HttpClient } from '@angular/common/http';
import { HandleHttpErrorService } from '../@base/handle-http-error.service';
import * as signalR from "@aspnet/signalr";

@Injectable({
  providedIn: 'root'
})
export class SignalRService {

  baseUrl: string;
  private hubConnection: signalR.HubConnection;
  signalReceived = new EventEmitter<Persona>();

  constructor( private http: HttpClient, @Inject('BASE_URL') baseUrl: string, private handleErrorService: HandleHttpErrorService) {
    this.baseUrl = baseUrl;
    this.buildConnection();
    this.startConnection();
  }
  private buildConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder().withUrl(this.baseUrl + "signalHub").build();
  }
  private startConnection = () => {
    this.hubConnection
    .start()
    .then(() => {
      console.log("Connection Started...");
      this.registerSignalEvents();
    })
    .catch(err => {
      console.log("Error ehile starting connection:" + err);
      setTimeout(function(){
        this.startConnection();
      }, 3000);
    });
  }
  private registerSignalEvents(){
    this.hubConnection.on("PersonaRegistrada", (data: Persona) => {
      this.signalReceived.emit(data);
    })
  }
}
