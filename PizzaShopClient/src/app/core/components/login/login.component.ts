import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { filter } from 'rxjs/operators';
import {
  FormControl,
  Validators,
  FormGroup,
  FormBuilder,
} from '@angular/forms';
import { LoginService } from '../../services/login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  loginGroup: FormGroup;
  // = new FormGroup({
  //   email: new FormControl('', [Validators.required, Validators.email]),
  //   password: new FormControl('', [Validators.required]),
  //   remember: new FormControl(true),
  // });
  // emailControl = new FormControl('', [Validators.required, Validators.email]);
  // passwordControl = new FormControl('', [Validators.required]);

  constructor(
    private loginService: LoginService,
    private router: Router,
    private fb: FormBuilder
  ) {
    this.loginGroup = this.fb.group({
      username: [''],
      password: [''],
      remember: [true],
    });
  }

  ngOnInit(): void {
    this.loginService.LoggedOn$.subscribe((_) => {
      this.router.navigate(['pizzas']);
    });
  }

  login() {
    this.loginService.loginWithCode();
  }

  loginWithPassword() {
    this.loginService.loginWithPass(
      this.loginGroup.value.username,
      this.loginGroup.value.password
    );
  }
}
