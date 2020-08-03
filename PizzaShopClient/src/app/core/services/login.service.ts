import { environment } from './../../../environments/environment';
import { Injectable, InjectionToken, Inject } from '@angular/core';
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
import { PASSWORD_FLOW_CONFIG, CODE_FLOW_CONFIG } from '../configs/auth.config';

@Injectable()
export class LoginService {
  private loggedOnSubject: BehaviorSubject<UserDto> = new BehaviorSubject<
    UserDto
  >(null);
  private user: UserDto;

  constructor(
    private router: Router,
    private oauth: OAuthService,
    @Inject(PASSWORD_FLOW_CONFIG) private passFlow: AuthConfig,
    @Inject(CODE_FLOW_CONFIG) private codeFlow: AuthConfig
  ) {
    this.oauth.tryLogin();
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

  async loginWithCode() {
    try {
      await this.configureOauth(this.codeFlow);
      this.oauth.initCodeFlow();
    } catch (error) {
      console.log(`cannot login: ${error}`);
    }
  }

  async loginWithPass(userName: string, password: string) {
    //Promise -> Observable
    await this.configureOauth(this.passFlow);
    try {
      const userInfo = await this.oauth.fetchTokenUsingPasswordFlowAndLoadUserProfile(
        userName,
        password
      );
      this.user = Object.assign({} as UserDto, userInfo);
      this.loggedOnSubject.next(this.user);
    } catch (error) {
      console.log(`cannot login: ${error}`);
    }
  }

  logout() {
    this.user = null;
    this.loggedOnSubject.next(null);
    this.oauth.logOut(true);
    this.router.navigate(['home']);
  }

  private async configureOauth(config: AuthConfig) {
    this.oauth.configure(config);
    await this.oauth.loadDiscoveryDocument();
  }
}
