import { PizzasGuard } from './pizzas.guard';
import { NotFoundComponent } from './../components/main/not-found/not-found.component';
import { PizzaListComponent } from './../components/core/pizza-list/pizza-list.component';
import { HomeComponent } from './../components/main/home/home.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from '../components/main/login/login/login.component';

/*
    /home
    /pizzas
    /login
    /logout
    / - root
    /xxx - notfound
 */
export const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'pizzas', component: PizzaListComponent, canActivate: [PizzasGuard] },
  { path: 'login', component: LoginComponent },
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: '**', component: NotFoundComponent },
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class PizzaRoutingModule { }
