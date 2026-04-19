import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterOutlet} from '@angular/router';
import { CommonModule } from '@angular/common';
import { UrlControl } from "./components/url-control/url-control";


@Component({
  selector: 'app-root',
  imports: [
    FormsModule,
    CommonModule,
    RouterOutlet,
    UrlControl
],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  
}