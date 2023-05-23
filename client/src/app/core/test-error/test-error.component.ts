import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { enviornment } from 'src/enviornments/enviornment';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.scss'],
})
export class TestErrorComponent {
  baseUrl = enviornment.apiUrl;
  validationErrors: string[] = [];

  constructor(private httpClient: HttpClient) {}

  get404Error() {
    this.httpClient.get(this.baseUrl + 'product/43').subscribe({
      next: (response) => console.log(response),
      error: (error) => console.log(error),
    });
  }

  get500Error() {
    this.httpClient.get(this.baseUrl + 'BuggyApi/servererror').subscribe({
      next: (response) => console.log(response),
      error: (error) => console.log(error),
    });
  }

  get400Error() {
    this.httpClient.get(this.baseUrl + 'BuggyApi/badrequest').subscribe({
      next: (response) => console.log(response),
      error: (error) => console.log(error),
    });
  }

  get400ValidationError() {
    this.httpClient.get(this.baseUrl + 'product/fourtytwo').subscribe({
      next: (response) => console.log(response),
      error: (error) => {
        console.log(error);
        this.validationErrors = error.errors;
      },
    });
  }
}
