import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-my-albums',
  templateUrl: './my-albums.component.html',
  styleUrls: ['./my-albums.component.css']
})
export class MyAlbumsComponent {
  constructor(private router: Router) { }
  handleError(error: any) {
    this.router.navigate(['/error'], {
      queryParams: { errorDetails: JSON.stringify(error) }
    });
  }
}
