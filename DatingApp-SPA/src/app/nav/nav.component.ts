import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};

  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
      console.log('Logged in successfully.')
    }, error => {
      console.error('Failed to login.')
    });
  }

  loggedIn() {
    // TODO check whether token is expired
    const token = localStorage.getItem("token");
    return !!token;
  }

  logOut() {
    localStorage.removeItem("token");
    console.log('Successfully logged out.');
  }

}
