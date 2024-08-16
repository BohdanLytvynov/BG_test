import { NgClass } from '@angular/common';
import { Component, EventEmitter, Inject, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { DataService } from '../../services/data-service/data.service';

class Author {
  constructor(
    public authorID: string,
    public authorName: string,
    public authorSureName: string,
    public authorBirthday: string
  ) {}
}

@Component({
  selector: 'app-autor-edit-form',
  standalone: true,
  imports: [NgClass, FormsModule],
  providers: [DataService],
  templateUrl: './autor-edit-form.component.html',
  styleUrl: './autor-edit-form.component.css'
})
export class AutorEditFormComponent {

  constructor(@Inject(DataService) private dataService: DataService) {};

  @Input() show = false;
  @Output() onChange = new EventEmitter<boolean>();

  @Input() id = '';

  handleClose(value: boolean) {
    this.onChange.emit(value)
  };

  author: Author  = {
    authorID: '',
    authorName: '',
    authorSureName: '',
    authorBirthday: ''
  };

  ngOnInit() {
    this.author = this.dataService.getAuthorByID(this.id);
  }

  ngOnChanges() {
    this.author = this.dataService.getAuthorByID(this.id);
  }

  editAuthor() {

    // action is here
    console.log(this.author)

    this.handleClose(false)
  };

}
