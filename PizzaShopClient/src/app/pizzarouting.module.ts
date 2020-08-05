import { AuthGuard } from './core/guards/pizzas.guard';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './core/components/home/home.component';
import { PizzaListComponent } from './pizza/components/pizza-list/pizza-list.component';
import { PizzaInfoComponent } from './pizza/components/pizza-info/pizza-info.component';
import { LoginComponent } from './core/components/login/login.component';
import { ProfileComponent } from './user/components/profile/profile.component';
import { NotFoundComponent } from './core/components/not-found/not-found.component';
import { CoreModule } from './core/core.module';
import { UserModule } from './user/user.module';
import { PizzaModule } from './pizza/pizza.module';
import { SharedModule } from './shared/shared.module';
import { LogoutResolver } from './core/resolvers/logout.resolver';

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
  { path: 'pizzas', component: PizzaListComponent, canActivate: [AuthGuard] },
  { path: 'pizzas/:id', component: PizzaInfoComponent },
  { path: 'login', component: LoginComponent },
  {
    path: 'logout',
    resolve: { data: LogoutResolver },
    component: HomeComponent,
  },
  { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard] },
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: '**', component: NotFoundComponent },
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    SharedModule,
    PizzaModule,
    UserModule,
    RouterModule.forRoot(routes, { enableTracing: false }),
    CoreModule.forRoot(),
  ],
  exports: [CoreModule, SharedModule, RouterModule],
})
export class PizzaRoutingModule {}
