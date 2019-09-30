import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators'

// this decorator allows it to be injected. components are automatically injectable, but services
// are not. Provided in root specifices which module is providing the service (app). Must be
// added to app.module.ts > providers array.
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = 'http://localhost:5000/api/auth/';

  constructor(private http: HttpClient) {}

  login(model: any) {
    // to do something with an observable that comes back from a server, we need to use
    // rxjs operators, for which we need the `pipe` method. we can then chain rxjs operators
    // to the request. (why not subscribe?)
    return this.http.post(this.baseUrl + 'login', model)
      .pipe(
        map((response: any) => {
          const user = response;
          if (user) {
            localStorage.setItem('token', user.token);
          }
        })
      );
  }
}
