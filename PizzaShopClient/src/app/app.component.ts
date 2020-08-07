import { LoginService } from 'src/app/core/services/login.service';
import { Component, OnInit } from '@angular/core';
import { NotificationsService } from 'angular2-notifications';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'PizzaShop';
  /**
   *
   */
  constructor(private auth: LoginService, private ntf: NotificationsService) {}

  ngOnInit(): void {
    this.auth.LoggedOn$.subscribe((u) =>
      this.ntf.success('Success', `Hello ${u === null ? 'Anonymous' : u.email}`)
    );
  }
}
