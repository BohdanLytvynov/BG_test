import { NgClass } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';

class Book {
  constructor(
    public bookID: string,
    public bookName: string,
    public bookYear: string,
    public bookGenre: string
  ) {}
}

@Component({
  selector: 'app-book-form',
  standalone: true,
  imports: [NgClass, FormsModule],
  templateUrl: './book-form.component.html',
  styleUrl: './book-form.component.css'
})
export class BookFormComponent {

  @Input() show = false;
  @Output() onChange = new EventEmitter<boolean>();

  handleClose(value: boolean) {
    this.onChange.emit(value)
  }

  bookID: string = '';
  bookName: string = '';
  bookYear: string = '';
  bookGenre: string = '';

  addBook() {
    const book = new Book(this.bookID, this.bookName, this.bookYear, this.bookGenre);

    // action is here
    console.log(book);
    
    this.handleClean();
    this.handleClose(false)
  }

  handleClean() {
    this.bookID = '';
    this.bookName = '';
    this.bookYear = '';
    this.bookGenre = '';
  }

}
