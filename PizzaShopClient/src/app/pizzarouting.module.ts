import { AuthGuard } from './core/guards/pizzas.guard';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes, PreloadAllModules } from '@angular/router';
import { HomeComponent } from './core/components/home/home.component';
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
  {
    path: 'pizzas',
    loadChildren: () =>
      import('./pizza/pizza.module').then((d) => d.PizzaModule),
  },
  { path: 'login', component: LoginComponent },
  {
    path: 'logout',
    resolve: { data: LogoutResolver },
    component: HomeComponent,
  },
  {
    path: 'profile',
    loadChildren: () => import('./user/user.module').then((u) => u.UserModule),
  },
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: '**', component: NotFoundComponent },
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes, {
      enableTracing: false,
      // preloadingStrategy: PreloadAllModules,
    }),
  ],
  exports: [RouterModule],
})
export class PizzaRoutingModule {}
