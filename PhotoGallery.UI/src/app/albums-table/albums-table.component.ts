//import { Component, OnInit, ViewChild } from '@angular/core';
//import { MatPaginator } from '@angular/material/paginator';
//import { MatTableDataSource } from '@angular/material/table';
//import { AlbumService } from './album.service'; // Replace with your service
//import { AlbumDto } from './a';

//@Component({
//  selector: 'app-albums-table',
//  templateUrl: './albums-table.component.html',
//  styleUrls: ['./albums-table.component.css']
//})
//export class AlbumsTableComponent implements OnInit {
//  displayedColumns: string[] = ['title', 'description'];
//  dataSource: MatTableDataSource<AlbumDto>;
//  pageSize = 5;
//  pageSizeOptions = [5, 10, 20];

//  @ViewChild(MatPaginator) paginator: MatPaginator;

//  constructor(private albumService: AlbumService) { }

//  ngOnInit(): void {
//    this.fetchAlbums();
//  }

//  fetchAlbums(): void {
//    this.albumService.getAlbums().subscribe((response: PaginatedItemsResponse<AlbumDto>) => {
//      this.dataSource = new MatTableDataSource(response.data);
//      this.dataSource.paginator = this.paginator;
//    });
//  }
//}
