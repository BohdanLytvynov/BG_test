import { Component, Inject } from '@angular/core';
import { BookFormComponent } from '../book-form/book-form.component';
import { DataService } from '../../services/data.service';
import { Book } from '../../interfaces/intefaces';
import { BookEditFormComponent } from '../book-edit-form/book-edit-form.component';

@Component({
  selector: 'app-books',
  standalone: true,
  imports: [BookFormComponent, BookEditFormComponent],
  providers: [DataService],
  templateUrl: './books.component.html',
  styleUrl: './books.component.css'
})
export class BooksComponent {

  constructor(@Inject(DataService) private dataService: DataService) {};
  
  showBookForm = false;
  bookForm = false;

  showBookFormComponent() {
    this.bookForm = true;
    this.showBookForm = true;
  };

  hideBookFormComponent(value: boolean) {
    this.showBookForm = value;
  };

  showBookEditForm = false;
  bookEditForm = false;
  currentID = '';

  showBookEditFormComponent(id: string) {
    this.currentID = id;
    this.bookEditForm = true;
    this.showBookEditForm = true;
  }

  hideBookEditFormComponent() {
    this.showBookEditForm = false;
  }

  items: Book[] = [];

  ngOnInit() {
    this.items = this.dataService.getBooks();
  };

  deleteBook(id: string) {
    this.items = this.items.filter(item => item.bookID !== id);

    // action is here
    console.log(`delete book with id: ${id}`);
  };

}
