import { SampleModel } from './../../../shared/models/sample.model';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  model: SampleModel = { title: 'Default' };

  constructor() {}

  ngOnInit(): void {}

  onClick() {
    this.model = { title: 'Changed' };
  }
}
