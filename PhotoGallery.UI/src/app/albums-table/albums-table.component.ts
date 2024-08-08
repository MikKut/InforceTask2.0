import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { AlbumWithImageDto } from '../interfaces/album-with-image-dto';
import { JwtHelperService } from '@auth0/angular-jwt';
import { PaginatedItemsResponse } from '../interfaces/paginated-items-response.interface';
import { AlbumDto } from '../interfaces/album-dto.interface';
import { DeleteRequest } from '../interfaces/delete-request.interface';
import { environment } from '../../environments/environment';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';

@Component({
  selector: 'app-albums-table',
  templateUrl: './albums-table.component.html',
  styleUrls: ['./albums-table.component.css']
})
export class AlbumsTableComponent implements OnInit {
  pageSizeOptions: number[] = [5, 10, 20];
  albums: AlbumWithImageDto[] = [];
  totalItems: number = 0;
  pageSize: number = 5;
  isAdmin: boolean = false;
  userId: string = ''; // Get user ID from your local storage
  dataSource!: MatTableDataSource<AlbumWithImageDto>;
  displayedColumns: string[] = ['title', 'description'];
  constructor(private router: Router, private http: HttpClient, private route: ActivatedRoute, private jwtHelper: JwtHelperService) { console.log('in costructor'); }

  ngOnInit(): void {
    console.log('on Init');
    this.dataSource = new MatTableDataSource<AlbumWithImageDto>(this.albums);
    this.isAdmin = this.isAdminUser(); // Check if the user is an admin
    this.fetchAlbums(1, this.pageSize); // Fetch the first page of albums
    console.log('after on Init');
  }

  fetchAlbums(pageIndex: number, pageSize: number): void {
    // Make an HTTP request to your API to fetch albums
    this.http
      .post<PaginatedItemsResponse<AlbumWithImageDto>>(environment.albumPath + 'albums', {
        pageIndex,
        pageSize
      })
      .subscribe(response => {
        this.albums = response.data;
        this.totalItems = response.count;

      });
  }

  deleteAlbum(album: AlbumWithImageDto): void {
    if (this.isAdmin) {
      const deleteEndpoint = `${environment.albumPath}/Delete`;
      const albumId = album.albumDto.id;

      this.http
        .delete<DeleteRequest<AlbumDto>>(deleteEndpoint, { body: album.albumDto })
        .pipe(
          catchError(error => {
            console.error('error');
            this.handleError(error);
            return throwError(error);
          })
        )
        .subscribe(() => {
          // Remove the deleted album from the local array
          this.albums = this.albums.filter((a) => a.albumDto.id !== albumId);
        });
    }
  }


  isAdminUser(): boolean {
  const token = localStorage.getItem('access_token');
  if (token && !this.jwtHelper.isTokenExpired(token)) {
    const decodedToken = this.jwtHelper.decodeToken(token);
    const userRole = decodedToken['role'];
    return userRole === 'admin';
  }
  return false;
  }
  handleError(error: any) {
    console.log('error handling');
    this.router.navigate(['/error'], {
      queryParams: { errorDetails: JSON.stringify(error) }
    });
  }
}
