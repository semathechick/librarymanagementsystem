import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookListForAuthorsComponent } from './book-list-for-authors.component';

describe('BookListForAuthorsComponent', () => {
  let component: BookListForAuthorsComponent;
  let fixture: ComponentFixture<BookListForAuthorsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BookListForAuthorsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BookListForAuthorsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
