import { LoginService } from './../../../services/login.service';
import { NotificationService } from './../../../services/notification.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  message: string;
  isLogged = false;

  constructor(private ntf: NotificationService, public loginService: LoginService) { }

  ngOnInit(): void {
    this.ntf.Message$.subscribe(msg => this.message = msg);
    this.loginService.LoggedOn$.subscribe(flag => this.isLogged = flag);
  }

}
