import { environment } from './../../../environments/environment';
import { Injectable } from '@angular/core';
import { Subject, BehaviorSubject, Observable, from } from 'rxjs';
import { Router } from '@angular/router';
import {
  OAuthService,
  AuthConfig,
  OAuthEvent,
  OAuthInfoEvent,
  OAuthSuccessEvent,
} from 'angular-oauth2-oidc';
import { UserDto } from '../model/userDto';
import { filter, switchMap, map } from 'rxjs/operators';

export const oauthConfig: AuthConfig = {
  issuer: environment.issuerUrl,
  redirectUri: window.location.origin + '/index.html',
  clientId: 'PizzaUserClient',
  dummyClientSecret: 'secret',
  // responseType: 'code',
  scope: 'openid profile email api',
  requireHttps: false,
  skipIssuerCheck: true,
  showDebugInformation: true,
  disablePKCE: true,
  oidc: false,
  postLogoutRedirectUri: window.location.origin + '/login',
};

@Injectable()
export class LoginService {
  private loggedOnSubject: BehaviorSubject<UserDto> = new BehaviorSubject<
    UserDto
  >(null);
  private user: UserDto;

  constructor(private router: Router, private oauth: OAuthService) {
    this.oauth.configure(oauthConfig);
    this.oauth.loadDiscoveryDocumentAndTryLogin();
    this.oauth.events
      .pipe(
        filter((value) => value.type === 'token_received'),
        map((_) => Object.assign({} as UserDto, this.oauth.getIdentityClaims()))
      )
      .subscribe((u) => this.loggedOnSubject.next(u));
  }

  get LoggedOn$(): Observable<UserDto> {
    return this.loggedOnSubject.asObservable();
  }

  get LoggedOn() {
    return this.loggedOnSubject.value;
  }

  login(userName?: string, password?: string) {
    if (!userName || !password) {
      this.oauth.initLoginFlow();
    }

    //Promise -> Observable
    this.oauth
      .fetchTokenUsingPasswordFlowAndLoadUserProfile(userName, password)
      .then((userInfo) => {
        this.user = Object.assign(<UserDto>{}, userInfo);
        this.loggedOnSubject.next(this.user);
      })
      .catch((reason) => console.error(reason));
  }

  logout() {
    this.user = null;
    this.loggedOnSubject.next(null);
    this.oauth.logOut(true);
    this.router.navigate(['home']);
  }
}
