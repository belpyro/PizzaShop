import { PizzaDto } from './../../../pizza/services/pizza.service';
import { NotificationsService } from 'angular2-notifications';
import { SignalR, ISignalRConnection } from 'ng2-signalr';
import { UserDto } from '../../model/userDto';
import { LoginService } from '../../../core/services/login.service';
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
  private connection: ISignalRConnection;
  // private subscription: Subscription;

  constructor(
    private loginService: LoginService,
    private hub: SignalR,
    private ntf: NotificationsService
  ) {}

  ngOnDestroy(): void {
    // this.subscription.unsubscribe();
  }

  ngOnInit(): void {
    this.user$ = this.loginService.LoggedOn$;
    this.isLogged$ = this.loginService.isLoggedOn$;

    this.hub
      .connect()
      .then((c) => {
        this.connection = c;
        this.connection
          .listenFor<string>('UpdateMessage')
          .subscribe((msg) => this.ntf.warn('Message', msg));
        this.connection
          .listenFor<PizzaDto>('PizzaAdded')
          .subscribe((dto) => this.ntf.success('Pizza Added', dto));
      })
      .catch((reason) =>
        console.error(`Cannot connect to hub sample ${reason}`)
      );
    // this.subscription = this.loginService.LoggedOn$.subscribe();
  }

  logout() {
    this.loginService.logout();
  }

  sendMessage(msg: string) {
    if (this.connection) {
      this.connection.invoke('SendMessage', msg);
    }
  }
}
