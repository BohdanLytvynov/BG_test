import { NgClass } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';

class Author {
  constructor(
    public authorID: string,
    public authorName: string,
    public authorSureName: string,
    public authorBirthday: string
  ) {}
}

@Component({
  selector: 'app-author-form',
  standalone: true,
  imports: [NgClass, FormsModule],
  templateUrl: './author-form.component.html',
  styleUrl: './author-form.component.css'
})
export class AuthorFormComponent {

  @Input() show = false;
  @Output() onChange = new EventEmitter<boolean>();

  handleClose(value: boolean) {
    this.onChange.emit(value)
  };

  authorID: string = '';
  authorName: string = '';
  authorSureName: string = '';
  authorBirthday: string = '';

  addAuthor() {
    const author = new Author(this.authorID, this.authorName, this.authorSureName, this.authorBirthday);

    // action is here
    console.log(author)

    this.handleClean()
    this.handleClose(false)
  };

  handleClean() {
    this.authorID = '';
    this.authorName = '';
    this.authorSureName = '';
    this.authorBirthday = '';
  };

}
