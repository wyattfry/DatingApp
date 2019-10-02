import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Input() valuesFromHome: any;
  // output must only send events
  @Output() cancelRegister = new EventEmitter();
  model: any = {};

  constructor() { }

  ngOnInit() {
  }

  register() {
    console.log(this.model);
  }

  cancel() {
    console.log('registerComponent.cancel(false) called');
    this.cancelRegister.emit(false); // you can emit anything, e.g. primatives, objs etc
    console.log('cancelled.');
  }

}
