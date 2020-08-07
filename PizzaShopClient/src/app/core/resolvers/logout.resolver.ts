import { LoginService } from 'src/app/core/services/login.service';
import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { Observable, EMPTY, NEVER } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class LogoutResolver implements Resolve<any> {
  /**
   *
   */
  constructor(private loginService: LoginService) {}

  resolve(route: ActivatedRouteSnapshot): Observable<any> | Promise<any> | any {
    this.loginService.logout();
    return EMPTY;
  }
}
