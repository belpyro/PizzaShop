import { CoreModule } from './core/core.module';
import { NavbarComponent } from './core/components/navbar/navbar.component';
import { BrowserModule } from '@angular/platform-browser';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { PizzaRoutingModule } from './pizzarouting.module';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from './core/interceptors/auth.interceptor';
import { SimpleNotificationsModule } from 'angular2-notifications';
import {
  SignalRModule,
  SignalRConfiguration,
  ConnectionTransports,
  ConnectionTransport,
} from 'ng2-signalr';

export function initConfig(): SignalRConfiguration {
  const cfg = new SignalRConfiguration();

  cfg.hubName = 'sample';
  cfg.url = 'http://demovm:50698/';
  cfg.transport = [
    ConnectionTransports.webSockets,
    ConnectionTransports.longPolling,
  ];

  return cfg;
}

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    NoopAnimationsModule,
    SimpleNotificationsModule.forRoot(),
    CoreModule.forRoot(),
    SignalRModule.forRoot(initConfig),
    PizzaRoutingModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
