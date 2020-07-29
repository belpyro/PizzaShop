import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { PizzaRoutingModule } from './pizzarouting.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    PizzaRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule { }
