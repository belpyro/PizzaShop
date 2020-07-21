import { UserDto } from './../models/userDto';
import { Injectable } from '@angular/core';
import { Subject, BehaviorSubject } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({ providedIn: 'root' })
export class LoginService {
  private loggedOnSubject = new BehaviorSubject<boolean>(true);
  private user: UserDto;

  constructor(private router: Router) {}

  get LoggedOn$() {
    return this.loggedOnSubject.asObservable();
  }

  get LoggedOn() {
    return this.loggedOnSubject.value;
  }

  login(userName?: string, password?: string) {
    setTimeout(() => {
      this.user = { uid: '111-111-111', fullName: 'Ivan Ivanov' };
      this.loggedOnSubject.next(true);
    }, 2000);
  }

  logout() {
    this.user = null;
    this.loggedOnSubject.next(false);
    this.router.navigate(['home']);
  }
}
