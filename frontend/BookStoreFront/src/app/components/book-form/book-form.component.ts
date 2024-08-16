import { NgClass } from '@angular/common';
import { Component, EventEmitter, Inject, Input, OnInit, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ValidationService } from '../../services/validation/validation.service';
import { ValidatorBase } from '../../services/validation/validation';

@Component({
  selector: 'app-book-form',
  standalone: true,
  imports: [NgClass, FormsModule],
  templateUrl: './book-form.component.html',
  styleUrl: './book-form.component.css'
})
export class BookFormComponent extends ValidatorBase implements OnInit  {

  @Input() show = false;
  @Output() onChange = new EventEmitter<boolean>();

  handleClose(value: boolean) {
    this.onChange.emit(value)
  }
  
constructor(@Inject(ValidationService) private validation : ValidationService)
{
  super();
}
  ngOnInit(): void {
    this.Init(3);
  }

  bookName: string = '';
  bookYear: string = '';
  bookGenre: string = '';

  addBook() {
    

   
    
    this.handleClean();
    this.handleClose(false)
  }

  handleClean() {
    
    this.bookName = '';
    this.bookYear = '';
    this.bookGenre = '';
  }

  //Validation 
onBookNameChange(value : string)
{

}

onBookYearChange(value : string)
{

}



}
