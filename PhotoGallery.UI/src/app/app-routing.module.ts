import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { AlbumsTableComponent } from './albums-table/albums-table.component';
import { MyAlbumsComponent } from './my-albums/my-albums.component';
import { ErrorPageComponent } from './error-page/error-page.component';

const routes: Routes = [
  { path: 'error', component: ErrorPageComponent },
  { path: 'albums', component: AlbumsTableComponent },
  { path: 'login', component: LoginComponent }, // Define the login route
  { path: '', redirectTo: '/login', pathMatch: 'full' }, // Set the default route to /login
  { path: 'my-albums', component: MyAlbumsComponent }
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
