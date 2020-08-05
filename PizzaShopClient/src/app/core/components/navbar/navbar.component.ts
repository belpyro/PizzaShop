import { UserDto } from '../../model/userDto';
import { LoginService } from '../../../core/services/login.service';
import { NotificationService } from '../../services/notification.service';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent implements OnInit, OnDestroy {
  message: string;
  email: string;
  user$: Observable<UserDto>;
  isLogged$: Observable<boolean>;
  // private subscription: Subscription;

  constructor(private loginService: LoginService) {}

  ngOnDestroy(): void {
    // this.subscription.unsubscribe();
  }

  ngOnInit(): void {
    this.user$ = this.loginService.LoggedOn$;
    this.isLogged$ = this.loginService.isLoggedOn$;
    // this.subscription = this.loginService.LoggedOn$.subscribe();
  }

  logout() {
    this.loginService.logout();
  }
}
