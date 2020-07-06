import { PizzaDto } from './../../../../models/pizzaDto';
import { PizzaService } from './../../../../services/pizza.service';
import { Component, OnInit, SkipSelf, Self, Optional } from '@angular/core';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.scss'],
  providers: []
})
export class CardComponent implements OnInit {

  pizza: PizzaDto;

  constructor(public pizzaService: PizzaService) {
   }

  ngOnInit(): void {
    this.pizzaService.getPizzaById(3).subscribe(data => this.pizza = data);
  }

}
