import { UserDto } from './../models/userDto';
import { Injectable } from '@angular/core';
import { Subject, BehaviorSubject } from 'rxjs';
import { Router } from '@angular/router';
import { OAuthService, AuthConfig } from 'angular-oauth2-oidc';

export const oauthConfig: AuthConfig = {
  issuer: 'http://demovm:50698',
  redirectUri: window.location.origin + '/index.html',
  clientId: 'PizzaWebClient',
  dummyClientSecret: 'secret',
  responseType: 'code',
  scope: 'openid profile email api',
  requireHttps: false,
  skipIssuerCheck: true,
  showDebugInformation: true,
  disablePKCE: true,
  postLogoutRedirectUri: window.location.origin + '/login',
};

@Injectable({ providedIn: 'root' })
export class LoginService {
  private loggedOnSubject = new BehaviorSubject<boolean>(true);
  private user: UserDto;

  constructor(private router: Router, private oauth: OAuthService) {
    this.oauth.configure(oauthConfig);
    this.oauth.loadDiscoveryDocumentAndTryLogin();
  }

  get LoggedOn$() {
    return this.loggedOnSubject.asObservable();
  }

  get LoggedOn() {
    return this.loggedOnSubject.value;
  }

  login(userName?: string, password?: string) {
    if (!userName || !password) {
      this.oauth.initLoginFlow();
    }

    this.oauth
      .fetchTokenUsingPasswordFlowAndLoadUserProfile(userName, password)
      .then((userInfo) => {
        this.user = { uid: userInfo.sub, fullName: 'Ivan Ivanov' };
        this.loggedOnSubject.next(true);
      })
      .catch((reason) => console.error(reason));
  }

  logout() {
    this.user = null;
    this.loggedOnSubject.next(false);
    this.oauth.logOut(true);
    this.router.navigate(['home']);
  }
}
