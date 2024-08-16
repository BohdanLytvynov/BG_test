import { NgClass } from '@angular/common';
import { Component, EventEmitter, Inject, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { DataService } from '../../services/data-service/data.service';
import { Book } from '../../interfaces/intefaces';

@Component({
  selector: 'app-book-edit-form',
  standalone: true,
  imports: [NgClass, FormsModule],
  providers: [DataService],
  templateUrl: './book-edit-form.component.html',
  styleUrl: './book-edit-form.component.css'
})
export class BookEditFormComponent {

  constructor(@Inject(DataService) private dataService: DataService) {}

  @Input() show = false;
  @Output() onChange = new EventEmitter<boolean>();

  @Input() id = -1;

  handleClose(value: boolean) {
    this.onChange.emit(value)
  };

  book: Book = {
    id: -1,
    name: '',
    pubYear: -1,
    geners: []
  };

  ngOnInit() {
    //this.book = this.dataService.getBookByID(this.id);
  };

  ngOnChanges() {
    //this.book = this.dataService.getBookByID(this.id);
  };

  edidBook() {

    // action is here
    console.log(this.book);
    
    this.handleClose(false)
  };

}
