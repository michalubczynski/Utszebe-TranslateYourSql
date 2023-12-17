import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http'; 

import { AppComponent } from './app.component';
import { ChatComponent } from './chat/chat.component';
import { SpinnerComponent } from './spinner/spinner.component';
import { SignalRService } from './signal-r.service';

@NgModule({
  declarations: [
    AppComponent,
    ChatComponent,
    SpinnerComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule, 
  ],
  providers: [SignalRService],
  bootstrap: [AppComponent]
})
export class AppModule { }
