import { Component } from '@angular/core';

// this file is responsible for giving data to the view (*.html)

// typescript means it can use decorators to give components (typescript classes) angular features
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'App';
}
