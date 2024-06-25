import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookListForIsbnComponent } from './book-list-for-isbn.component';

describe('BookListForIsbnComponent', () => {
  let component: BookListForIsbnComponent;
  let fixture: ComponentFixture<BookListForIsbnComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BookListForIsbnComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BookListForIsbnComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
