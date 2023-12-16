import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hubConnection: signalR.HubConnection;
  public resultSubject: Subject<string[]> = new Subject<string[]>();

  constructor() {
    
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:5257/resultHub').build();

    this.startConnection();
  }



  private startConnection(): void {
    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err));
  }

  public addResultListener(): void {
    this.hubConnection.on('ReceiveResult', (result: string[]) => {
      this.resultSubject.next(result);
    });
  }
}