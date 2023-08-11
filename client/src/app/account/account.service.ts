import { Injectable } from '@angular/core';
import { ReplaySubject, map, of } from 'rxjs';
import { enviornment } from 'src/enviornments/enviornment';
import { Address, User } from '../shared/models/user';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  private baseUrl = enviornment.apiUrl;
  private currentUserSource = new ReplaySubject<User | null>(1);
  currentUserSource$ = this.currentUserSource.asObservable();

  constructor(private httpClient: HttpClient, private router: Router) {}

  loadCurrentUser(token: string | null) {
    if (token === null) {
      this.currentUserSource.next(null);
      return of(null);
    }
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);

    return this.httpClient
      .get<User>(this.baseUrl + 'account', { headers })
      .pipe(
        map((user) => {
          if (user) {
            localStorage.setItem('token', user.token);
            this.currentUserSource.next(user);
            return user;
          } else {
            return null;
          }
        })
      );
  }

  login(values: any) {
    return this.httpClient
      .post<User>(this.baseUrl + 'account/login', values)
      .pipe(
        map((user) => {
          localStorage.setItem('token', user.token),
            this.currentUserSource.next(user);
        })
      );
  }

  register(values: any) {
    return this.httpClient
      .post<User>(this.baseUrl + 'account/register', values)
      .pipe(
        map((user) => {
          localStorage.setItem('token', user.token),
            this.currentUserSource.next(user);
        })
      );
  }

  logout() {
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/');
  }

  checkEmailExists(email: string) {
    console.log(email);
    return this.httpClient.get<boolean>(
      this.baseUrl + 'account/emailexists?email=' + email
    );
  }

  getUserAddress() {
    return this.httpClient.get<Address>(this.baseUrl + 'account/address');
  }

  updateUserAddress(address: Address) {
    return this.httpClient.put(this.baseUrl + 'account/address', address);
  }
}
