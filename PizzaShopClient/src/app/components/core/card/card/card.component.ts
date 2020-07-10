import { NotificationService } from './../../../../services/notification.service';
import { PizzaDto } from './../../../../models/pizzaDto';
import { PizzaService } from './../../../../services/pizza.service';
import {
  Component,
  OnInit,
  SkipSelf,
  Self,
  Optional,
  Input,
} from '@angular/core';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.scss'],
  providers: [],
})
export class CardComponent implements OnInit {
  @Input() pizza: PizzaDto;

  constructor(
    public pizzaService: PizzaService,
    private ntf: NotificationService
  ) {}

  ngOnInit(): void {}
}
