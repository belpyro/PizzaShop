import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { PizzaClient, PizzaDto } from '../../services/pizza.service';

@Component({
  selector: 'app-pizza-info',
  templateUrl: './pizza-info.component.html',
  styleUrls: ['./pizza-info.component.scss'],
})
export class PizzaInfoComponent implements OnInit {
  pizza: PizzaDto;

  constructor(private route: ActivatedRoute, private pizzaClient: PizzaClient) {
    this.route.data.subscribe((data) => (this.pizza = data.pizza));
  }

  ngOnInit(): void {}
}
