import { Component } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { AccountService } from '../account.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  loginForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', Validators.required),
  });

  returnUrl: string;

  constructor(
    private _accountService: AccountService,
    private router: Router,
    private activateRoute: ActivatedRoute
  ) {
    this.returnUrl =
      this.activateRoute.snapshot.queryParams['returnUrl'] || '/shop';
  }

  onSubmit() {
    this._accountService.login(this.loginForm.value).subscribe({
      next: (user) => this.router.navigateByUrl(this.returnUrl),
    });
  }
}
