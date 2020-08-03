import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.scss'],
  providers: [],
})
export class CardComponent implements OnInit {
  @Input() title = 'No Title';
  @Input() description = 'No description';
  @Input('label') btn_descr = 'No description';

  @Output('onClicked') btn_clicked = new EventEmitter();

  constructor() {}

  ngOnInit(): void {}

  onClick(){
    this.btn_clicked.emit();
  }
}
