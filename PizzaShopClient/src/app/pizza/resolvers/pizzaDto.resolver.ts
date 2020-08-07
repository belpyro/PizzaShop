import { PizzaDto, PizzaClient } from './../services/pizza.service';
import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { Observable, EMPTY } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable()
export class PizzaDtoResolver implements Resolve<PizzaDto> {
  /**
   *
   */
  constructor(private client: PizzaClient) {}

  resolve(
    route: ActivatedRouteSnapshot
  ): Observable<PizzaDto> | Promise<PizzaDto> | PizzaDto {
    const id = +route.params['id'];
    return this.client
      .getById(id)
      .pipe(map((v) => Object.assign({} as PizzaDto, v)));
  }
}
