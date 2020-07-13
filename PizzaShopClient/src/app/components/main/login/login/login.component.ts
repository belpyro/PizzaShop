import { Component, OnInit } from '@angular/core';
import { LoginService } from 'src/app/services/login.service';
import { Router } from '@angular/router';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(private loginService: LoginService, private router: Router) { }

  ngOnInit(): void {
    this.loginService.LoggedOn$.pipe(filter(_ => _)).subscribe(_ => {
      this.router.navigate(['pizzas']);
    });
  }

  login() {
    this.loginService.login();
  }
}
