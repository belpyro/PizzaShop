import { SampleModel } from './../../models/sample.model';
import {
  Component,
  OnInit,
  Input,
  ChangeDetectionStrategy,
} from '@angular/core';

@Component({
  selector: 'app-sample',
  templateUrl: './sample.component.html',
  styleUrls: ['./sample.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class SampleComponent implements OnInit {
  @Input() model: SampleModel = { title: 'Hello' };

  // get sampleModel(): SampleModel {
  //   console.log('Sample model is loaded');
  //   return { title: 'First Title' };
  // }

  constructor() {}

  ngOnInit(): void {}
}
