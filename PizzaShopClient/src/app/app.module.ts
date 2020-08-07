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

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    NoopAnimationsModule,
    SimpleNotificationsModule.forRoot(),
    CoreModule.forRoot(),
    PizzaRoutingModule,
  ],
  providers: [,],
  bootstrap: [AppComponent],
})
export class AppModule {}
