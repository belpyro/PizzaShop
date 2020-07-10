import { NotificationService } from './../../../services/notification.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  message: string;

  constructor(private ntf: NotificationService) { }

  ngOnInit(): void {
    this.ntf.Message$.subscribe(msg => this.message = msg);
  }

}
