import { PizzaDto } from './../models/pizzaDto';
import { LoginService } from './login.service';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class PizzaService {

  serviceId: string;

  constructor(private http: HttpClient) {
    this.serviceId = Date.now().toPrecision(21).toString();
   }

   getPizzaById(id:number) {
      return this.http.get<PizzaDto>(`http://demovm:50698/api/pizzas/all/${id}`);
   }
}
