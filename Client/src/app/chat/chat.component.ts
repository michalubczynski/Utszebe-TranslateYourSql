import { Component, Input, OnInit} from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'; // Import HttpClient
import { AppComponent } from '../app.component';
import { SignalRService } from '../signal-r.service'
import { ChatService } from '../chat.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {
  @Input() index: number | undefined;

  query: string = '';
  message: string = '';
  translatedQuery: string = '';
  isLoading: boolean = false;  // IF True, blurs window and spinner appear on middle of page
  inputsDisabled: boolean = false; 
  executeButtonVisible: boolean = false;


  constructor(private http: HttpClient, private parent: AppComponent, private signalRService: SignalRService, private chatService: ChatService) {} // Inject HttpClient

  ngOnInit(){

    const chatData = this.chatService.getChatData(this.index!);

    this.query = chatData.query;
    
    console.log("Query On INIT: "+this.query);
    console.log("Answer On INIT: "+this.message);

    this.signalRService.addResultListener();

    this.signalRService.resultSubject.subscribe(result => {

      // Tutaj dostaniesz się do nowych wyników i możesz zaktualizować this.messages[0]
      console.log("Wynik z chata: "+ result );
      console.log("Query Po otrzymaniu wynikow: "+this.query);
      const updatedData = { query: this.query, message: 'Odpowiedź: '+result };
        this.chatService.updateChatData(this.index!, updatedData);
    });
  }
  clearQuery() {
    this.query = '';
  }

  translate() {
    this.inputsDisabled = true;
    const port = 5257; // Change this to your desired port number
    const apiUrl = `http://localhost:${port}/api/Query/result`; // Adjust the route as needed

    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });
 
    const message = {
      Id: this.index,
      UserInput: this.query
    };

    this.http.post<string>(apiUrl, JSON.stringify(message), { headers })
      .subscribe(
        (result: string) => {
          console.log("Query: "+this.query);
          console.log("Answer: "+this.message);

          // Update chatData.message directly in the service
          const updatedData = { query: this.query, message: 'Odpowiedź: ' + result };
          this.chatService.updateChatData(this.index!, updatedData);

          this.translatedQuery = result;
          this.executeButtonVisible = true;
        },
        (error) => {
          console.error('Error:', error);
        }
        
      );
    }
  clearChat() {
    this.query = '';
    this.message = '';
  }
  executeQuery(){

    //logic of executing query to database...

    this.parent.updateNewQueryAddable(true);
    this.executeButtonVisible = false;
  }
}
