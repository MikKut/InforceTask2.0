import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AlbumsTableComponent } from './albums-table.component';

describe('AlbumsTableComponent', () => {
  let component: AlbumsTableComponent;
  let fixture: ComponentFixture<AlbumsTableComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AlbumsTableComponent]
    });
    fixture = TestBed.createComponent(AlbumsTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
