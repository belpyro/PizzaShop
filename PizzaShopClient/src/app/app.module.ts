import { NotFoundComponent } from './components/main/not-found/not-found.component';
import { HomeComponent } from './components/main/home/home.component';
import { PizzaListComponent } from './components/core/pizza-list/pizza-list.component';
import { PizzaRoutingModule } from './pizzarouting/pizzarouting.module';
import { LoginService } from './services/login.service';
import { PizzaService } from './services/pizza.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { NavbarComponent } from './components/main/navbar/navbar.component';
import { LoginComponent } from './components/main/login/login/login.component';
import { CardComponent } from './components/core/card/card/card.component';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { ProfileComponent } from './components/core/profile/profile.component';
import { PizzaInfoComponent } from './components/core/pizza-info/pizza-info.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    LoginComponent,
    CardComponent,
    PizzaListComponent,
    HomeComponent,
    NotFoundComponent,
    ProfileComponent,
    PizzaInfoComponent,
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    HttpClientModule,
    PizzaRoutingModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
