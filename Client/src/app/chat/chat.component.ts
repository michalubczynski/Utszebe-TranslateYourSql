import { Component, Input, OnInit} from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'; // Import HttpClient
import { AppComponent } from '../app.component';
import { SignalRService } from '../signal-r.service'

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {
  @Input() index: number | undefined;

  query: string = '';
  messages: string[] = [];
  translatedQuery: string = '';
  isLoading: boolean = false;  // IF True, blurs window and spinner appear on middle of page
  inputsDisabled: boolean = false; 
  executeButtonVisible: boolean = false;

  constructor(private http: HttpClient, private parent: AppComponent, private signalRService: SignalRService) {} // Inject HttpClient

  ngOnInit(){
    this.signalRService.addResultListener();

    this.signalRService.resultSubject.subscribe(result => {
      console.error(result);
      // Tutaj dostaniesz się do nowych wyników i możesz zaktualizować this.messages[0]
      this.messages[0] = 'Received result: ' + result;
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
          this.translatedQuery = result;
        },
        (error) => {
          console.error('Error:', error);
          // Handle the error response here
        }
        
      );
      this.executeButtonVisible = true;
    }
  clearChat() {
    this.query = '';
    this.messages = [];
  }
  executeQuery(){

    //logic of executing query to database...

    this.parent.updateNewQueryAddable(true);
    this.executeButtonVisible = false;
  }
}
