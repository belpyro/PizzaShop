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

@NgModule({
  declarations: [
    LoginComponent,
    NavbarComponent,
    NotFoundComponent,
    HomeComponent,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    OAuthModule.forRoot({
      resourceServer: {
        sendAccessToken: true,
        allowedUrls: [environment.backendUrl],
      },
    }),
  ],
  providers: [LoginService, NotificationService],
  exports: [
    LoginComponent,
    NavbarComponent,
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
      providers: [LoginService, NotificationService],
    };
  }
}
