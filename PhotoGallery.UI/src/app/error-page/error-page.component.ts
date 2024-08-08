import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterStateSnapshot } from '@angular/router';

@Component({
  selector: 'app-error',
  templateUrl: './error-page.component.html',
  styleUrls: ['./error-page.component.css']
})
export class ErrorPageComponent implements OnInit {
  errorDetails: any;

  constructor(private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    const queryParams = this.route.snapshot.queryParams;
    console.log('Query Params:', queryParams);

    this.errorDetails = queryParams['errorDetails'];
    console.log('Error Details:', this.errorDetails);
  }
}
