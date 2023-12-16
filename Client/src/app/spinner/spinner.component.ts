// spinner.component.ts
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-spinner',
  template: `
    <div *ngIf="show" class="spinner">
      <!-- Add your spinner HTML/CSS here -->
      <div class="spinner-circle"></div>
    </div>
  `,
  styleUrls: ['./spinner.component.css']
})
export class SpinnerComponent {
  @Input() show: boolean = false;
}
