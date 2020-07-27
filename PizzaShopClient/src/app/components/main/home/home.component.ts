import { Component, OnInit } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  access_token: string;

  constructor(private oauth: OAuthService) {}

  ngOnInit(): void {
    if (this.oauth.hasValidAccessToken()) {
      this.access_token = this.oauth.getAccessToken();
    }
  }
}
