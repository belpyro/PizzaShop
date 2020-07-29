import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable()
export class NotificationService {

  private messageSubject = new Subject<string>();

  constructor() { }

  get Message$() {
    return this.messageSubject.asObservable();
  }

  notify(message: string) {
    this.messageSubject.next(message);
  }
}
