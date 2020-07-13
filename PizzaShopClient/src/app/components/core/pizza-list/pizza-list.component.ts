import { PizzaService } from './../../../services/pizza.service';
import { PizzaDto } from './../../../models/pizzaDto';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-pizza-list',
  templateUrl: './pizza-list.component.html',
  styleUrls: ['./pizza-list.component.scss']
})
export class PizzaListComponent implements OnInit {

  pizzas: PizzaDto[] = [];

  constructor(private pizzaService: PizzaService) { }

  ngOnInit(): void {
    this.pizzaService.getAll()
      .subscribe(data => this.pizzas = data);
  }

}
