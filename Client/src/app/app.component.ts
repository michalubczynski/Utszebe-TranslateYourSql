import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.css'],

})
export class AppComponent {
  title = 'Angular SQL Chat App';
  index: number = 0; // Initialize index to 0
  newQueryAdditable: boolean = false;
  constructor(private http: HttpClient) {} // Inject HttpClient

  ngOnInit(){
    this.chatComponents.push({});

  }
  refreshPage() {

    const port = 5257; // Change this to your desired port number
    const apiUrl = `http://localhost:${port}/api/Query/`; // Adjust the route as needed

    this.http.delete(apiUrl).subscribe(
      () => {
        // Tutaj możesz obsłużyć sukces usunięcia historii
        console.log('Historia została usunięta.');
      },
      (error) => {
        // Tutaj możesz obsłużyć błąd
        console.error('Błąd podczas usuwania historii:', error);
      }
    );
    // Reload the whole page with inform API of new conversation
    this.index = 0; 
    window.location.reload();
  }
  chatComponents: any[] = [];

  addChatComponent() {
    this.newQueryAdditable = false;
    this.chatComponents.push({});
  }
  updateNewQueryAddable(value: boolean) {
    this.newQueryAdditable = value;
  }
}
