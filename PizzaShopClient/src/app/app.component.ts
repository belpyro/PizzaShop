import { NotificationService } from './services/notification.service';
import { PizzaDto } from './models/pizzaDto';
import { PizzaService } from './services/pizza.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'PizzaShopClient';
  pizzas: PizzaDto[] = [];
  /**
   *
   */
  constructor(private pzz: PizzaService, private ntf: NotificationService) {}

  ngOnInit(): void {
    this.pzz.getAllPizzas().subscribe((data) => {
      this.pizzas = data;
      this.ntf.notify('Loaded');
    });
  }
}
