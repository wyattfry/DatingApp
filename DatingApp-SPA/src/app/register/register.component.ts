import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  // output must only send events
  @Output() cancelRegister = new EventEmitter();
  model: any = {};

  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  register() {
    this.authService.register(this.model).subscribe(() => {
      console.log('Registration successful.');
    }, (error) => {
      console.log('Registration failed.', error);
    });
  }

  cancel() {
    console.log('registerComponent.cancel(false) called');
    this.cancelRegister.emit(false); // you can emit anything, e.g. primatives, objs etc
    console.log('cancelled.');
  }

}
