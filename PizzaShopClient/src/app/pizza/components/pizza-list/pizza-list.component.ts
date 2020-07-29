import { Component, OnInit } from '@angular/core';
import { PizzaClient, PizzaDto } from '../../services/pizza.service';

@Component({
  selector: 'app-pizza-list',
  templateUrl: './pizza-list.component.html',
  styleUrls: ['./pizza-list.component.scss']
})
export class PizzaListComponent implements OnInit {

  pizzas: PizzaDto[] = [];

  constructor(private pizzaClient: PizzaClient) { }

  ngOnInit(): void {
    this.pizzaClient.getAll()
      .subscribe(data => this.pizzas = data);
  }

}
