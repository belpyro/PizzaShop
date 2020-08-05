import { AuthConfig } from 'angular-oauth2-oidc';
import { environment } from 'src/environments/environment';
import { InjectionToken } from '@angular/core';

export const oauthPasswordConfig: AuthConfig = {
  issuer: environment.issuerUrl,
  redirectUri: window.location.origin,
  clientId: 'PizzaUserClient',
  dummyClientSecret: 'secret',
  scope: 'openid profile email api',
  requireHttps: false,
  showDebugInformation: !environment.production,
  oidc: false,
};

export const oauthCodeConfig: AuthConfig = {
  issuer: environment.issuerUrl,
  redirectUri: window.location.origin,
  clientId: 'PizzaWebClient',
  dummyClientSecret: 'secret',
  responseType: 'code',
  scope: 'openid profile email api',
  requireHttps: false,
  showDebugInformation: !environment.production,
  disablePKCE: true,
};

export const PASSWORD_FLOW_CONFIG = new InjectionToken<AuthConfig>(
  'password.flow.config'
);
export const CODE_FLOW_CONFIG = new InjectionToken<AuthConfig>(
  'code.flow.config'
);
