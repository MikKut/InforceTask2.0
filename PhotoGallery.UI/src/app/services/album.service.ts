import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { AlbumDto } from '../interfaces/album-dto.interface';
import { PaginatedItemsResponse } from '../interfaces/paginated-items-response.interface';

@Injectable({
  providedIn: 'root'
})
export class AlbumService {
  private apiUrl = environment.albumPath; // Replace with the actual API endpoint URL

  constructor(private http: HttpClient) { }

  getAlbums(): Observable<PaginatedItemsResponse<AlbumDto>> {
    return this.http.post<PaginatedItemsResponse<AlbumDto>>(`${this.apiUrl}/Albums`, {});
  }
}
