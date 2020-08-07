import { PizzaDtoResolver } from './resolvers/pizzaDto.resolver';
import { CoreModule } from './../core/core.module';
import { environment } from './../../environments/environment';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { PizzaListComponent } from './components/pizza-list/pizza-list.component';
import { PizzaInfoComponent } from './components/pizza-info/pizza-info.component';
import { PizzaClient, API_BASE_URL } from './services/pizza.service';
import { AuthGuard } from '../core/guards/pizzas.guard';
import { AuthInterceptor } from '../core/interceptors/auth.interceptor';

// /pizzas -> ''
// /pizzas/popular -> /popular
export const routes: Routes = [
  { path: '', component: PizzaListComponent, canActivate: [AuthGuard] },
  {
    path: ':id',
    component: PizzaInfoComponent,
    canActivate: [AuthGuard],
    resolve: { pizza: PizzaDtoResolver },
  },
];

@NgModule({
  declarations: [PizzaListComponent, PizzaInfoComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    SharedModule,
    RouterModule.forChild(routes),
  ],
  providers: [
    PizzaClient,
    { provide: API_BASE_URL, useValue: environment.backendUrl },
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    PizzaDtoResolver,
  ],
  exports: [PizzaListComponent, PizzaInfoComponent],
})
export class PizzaModule {}
