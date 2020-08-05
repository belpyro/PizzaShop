import { CurrencyPipe } from './components/pipes/currency.pipe';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardComponent } from './components/card/card.component';

@NgModule({
  declarations: [CardComponent, CurrencyPipe],
  imports: [CommonModule],
  exports: [CardComponent, CurrencyPipe],
})
export class SharedModule {}
