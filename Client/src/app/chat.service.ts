import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  private chatData: { [index: number]: { query: string; message: string } } = {};

  getChatData(index: number): { query: string; message: string } {
    if (!this.chatData[index]) {
      this.chatData[index] = { query: '', message: '' };
    }
    return this.chatData[index];
  }

  updateChatData(index: number, data: { query: string; message: string }): void {
    this.chatData[index] = data;
  }
}
