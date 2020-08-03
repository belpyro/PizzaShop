import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { PizzaClient, PizzaDto } from '../../services/pizza.service';

@Component({
  selector: 'app-pizza-list',
  templateUrl: './pizza-list.component.html',
  styleUrls: ['./pizza-list.component.scss'],
})
export class PizzaListComponent implements OnInit {
  pizzas: PizzaDto[] = [];

  constructor(private pizzaClient: PizzaClient, private router: Router) {}

  ngOnInit(): void {
    this.pizzaClient.getAll().subscribe((data) => (this.pizzas = data));
  }

  async goToPizzaPage(id: number) {
    await this.router.navigate(['pizzas', id]);
  }
}
