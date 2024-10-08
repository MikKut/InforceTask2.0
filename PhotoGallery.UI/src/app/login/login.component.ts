import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router'; // Import the Router module
import { environment } from '../../environments/environment';
import { catchError, throwError } from 'rxjs';
export interface LoginResponse {
  success: boolean;
  message: string;
  token?: string;
}

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  username: string = 's';
  password: string = 's';
  errorMessage: string = ''; // Initialize an error message variable

  constructor(private http: HttpClient, private router: Router) { }
  handleError(error: any) {
    this.router.navigate(['/error'], {
      queryParams: { errorDetails: JSON.stringify(error) }
    });
  }
  login() {
    let loginPathUrl = environment.loginPath;
    // Send a POST request to your ASP.NET Core backend for authentication
    this.http.post<LoginResponse>(loginPathUrl, { email: this.username, password: this.password })
      .pipe(
        catchError(error => {
          this.handleError(error);
          return throwError(error);
        })
      )
      .subscribe(
        (response) => {
          if (response.success) {
            // If login is successful, store the JWT token and redirect
            if (response.token) {
              // Store the JWT token in local storage (you can also use other storage methods)
              localStorage.setItem('token', response.token);
            }
            // Redirect to the ALBUMS TABLE PAGE
          } else {
            // If login is not successful, display the error message
            this.errorMessage = response.message;
          }
          this.router.navigate(['/albums']);
        },
        (error) => {
          // Handle login error, e.g., display a generic error message
          this.errorMessage = 'An error occurred during login. Please try again.';
          this.handleError(error);
        }
    );
  }
}
