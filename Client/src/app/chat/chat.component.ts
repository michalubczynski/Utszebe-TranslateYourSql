import { Component, Input } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'; // Import HttpClient
import { AppComponent } from '../app.component';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent {
  @Input() index: number | undefined;

  query: string = '';
  messages: string[] = [];
  translatedQuery: string = '';
  isLoading: boolean = false; 
  inputsDisabled: boolean = false; 
  executeButtonVisible: boolean = false;

  constructor(private http: HttpClient, private parent: AppComponent) {} // Inject HttpClient

  clearQuery() {
    this.query = '';
  }

  translate() {
    this.inputsDisabled = true;

    this.isLoading = true; // Show the loading indicator
    const port = 5257; // Change this to your desired port number
    const apiUrl = `http://localhost:${port}/api/Query/`; // Adjust the route as needed

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
          this.isLoading = false; // Hide the loading indicator when done

          this.messages[0] = "Przetlumaczone SQL query: "+ JSON.stringify(this.query);
          this.translatedQuery = JSON.stringify(result).replace(/"/g, '');

        },
        (error) => {
          this.isLoading = false; // Hide the loading indicator when done

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
