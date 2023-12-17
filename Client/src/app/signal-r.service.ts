import { EventEmitter, Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Subject, interval } from 'rxjs';
import { takeWhile } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hubConnection: signalR.HubConnection;
  public resultSubject: Subject<string[]> = new Subject<string[]>();
  public isReconnecting: boolean = false;
  public connectionStatus: EventEmitter<string> = new EventEmitter<string>();

  constructor() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:5257/resultHub').build();

    this.startConnection();
  }

  public startConnection(): void {
    this.hubConnection
      .start()
      .then(() => {
        console.log('Connection started');
        this.connectionStatus.emit('connected'); 
        this.isReconnecting = false;
      })
      .catch(err => {
        console.log('Error while starting connection: ' + err);
        this.reconnect();
      });

    // Handle connection closed and attempt to reconnect
    this.hubConnection.onclose((err) => {
      //console.error('Connection closed. Error:', err);
      console.log('Connection closed. Attempting to reconnect...');
      if (!this.isReconnecting) {
        this.reconnect();
      }
    });
  }

  private reconnect(): void {
    this.connectionStatus.emit('retrying'); // Set connection status to false
    this.isReconnecting = true;
    // Retry connection every 5 seconds
    const maxAttempts = 5; 
    let attempts = 0;

const reconnectInterval = setInterval(() => {
      if (attempts < maxAttempts) {
        attempts++;
        console.log(`Reconnection attempt ${attempts}/${maxAttempts}`);
        this.hubConnection
          .start()
          .then(() => {
            console.log('Reconnection successful');
            this.connectionStatus.emit('connected'); 
            clearInterval(reconnectInterval);
            this.isReconnecting = false;
          })
          .catch(err => {
            console.log('Error during reconnection: ' + err);
          });
      } else {
        console.log('Max reconnection attempts reached. Stopping further attempts.');
        console.log('Please refresh this site');
        this.connectionStatus.emit('disconnected'); 
        clearInterval(reconnectInterval);
      }
    }, 5000); // Retry every 5 seconds
  }

  public addResultListener(): void {
    this.hubConnection.on('ReceiveResult', (result: string[]) => {
      this.resultSubject.next(result);
    });
  }
}
