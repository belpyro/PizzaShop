import { CurrencyPipe } from './components/pipes/currency.pipe';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardComponent } from './components/card/card.component';
import { SampleComponent } from './components/sample/sample.component';

@NgModule({
  declarations: [CardComponent, CurrencyPipe, SampleComponent],
  imports: [CommonModule],
  exports: [CardComponent, CurrencyPipe, SampleComponent],
})
export class SharedModule {}
