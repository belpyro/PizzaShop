import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { PizzaListComponent } from './components/pizza-list/pizza-list.component';
import { PizzaInfoComponent } from './components/pizza-info/pizza-info.component';
import { PizzaClient } from './services/pizza.service';



@NgModule({
  declarations: [PizzaListComponent, PizzaInfoComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    RouterModule,
    SharedModule
  ],
  providers: [PizzaClient],
  exports: [PizzaListComponent, PizzaInfoComponent]
})
export class PizzaModule { }
