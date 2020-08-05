import { RouterModule } from '@angular/router';
import { environment } from './../../environments/environment';
import {
  NgModule,
  ModuleWithProviders,
  Optional,
  SkipSelf,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './components/login/login.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { ReactiveFormsModule } from '@angular/forms';
import { OAuthModule } from 'angular-oauth2-oidc';
import { HomeComponent } from './components/home/home.component';
import { LoginService } from './services/login.service';
import { NotificationService } from './services/notification.service';
import {
  CODE_FLOW_CONFIG,
  oauthCodeConfig,
  PASSWORD_FLOW_CONFIG,
  oauthPasswordConfig,
} from './configs/auth.config';

@NgModule({
  declarations: [
    LoginComponent,
    NotFoundComponent,
    HomeComponent,
    NavbarComponent,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule,
    OAuthModule.forRoot({
      resourceServer: {
        sendAccessToken: true,
        allowedUrls: [environment.backendUrl],
      },
    }),
  ],
  providers: [
    NotificationService,
    { provide: CODE_FLOW_CONFIG, useValue: oauthCodeConfig },
    { provide: PASSWORD_FLOW_CONFIG, useValue: oauthPasswordConfig },
  ],
  exports: [
    NavbarComponent,
    LoginComponent,
    NotFoundComponent,
    HomeComponent,
    OAuthModule,
  ],
})
export class CoreModule {
  constructor(@Optional() @SkipSelf() coreModule: CoreModule) {
    if (coreModule) {
      throw new Error('CoreModule already loaded.');
    }
  }

  static forRoot(): ModuleWithProviders<CoreModule> {
    return {
      ngModule: CoreModule,
      providers: [NotificationService, LoginService],
    };
  }
}
