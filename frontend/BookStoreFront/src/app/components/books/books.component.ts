import { Component, Inject } from '@angular/core';
import { BookFormComponent } from '../book-form/book-form.component';
import { DataService } from '../../services/data-service/data.service';
import { Book, ErrorHandler, IErrorHandler, Unauthorized } from '../../interfaces/intefaces';
import { BookEditFormComponent } from '../book-edit-form/book-edit-form.component';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-books',
  standalone: true,
  imports: [BookFormComponent, BookEditFormComponent, CommonModule],
  providers: [DataService],
  templateUrl: './books.component.html',
  styleUrl: './books.component.css'
})
export class BooksComponent {

  errorHandler : IErrorHandler = new ErrorHandler;

  constructor(@Inject(DataService) private dataService: DataService,
  @Inject(Router) private router : Router) {};
  
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
  currentID = 0;

  showBookEditFormComponent(id: number) {
    this.currentID = id;
    this.bookEditForm = true;
    this.showBookEditForm = true;
  }

  hideBookEditFormComponent() {
    this.showBookEditForm = false;
  }

  items: Book[] = [];

  ngOnInit() {
    this.dataService.getBooks<Book[]>().subscribe((resp) => 
    {
       if((resp as Book[]) != null)
       {
          this.items = resp;
          return;
       }
       else 
       {
          this.router.navigate(["/"]);
       }

       
      
    }, err => console.log(err));        
  };

  deleteBook(id: number) {
    //this.items = this.items.filter(item => item.id !== id);

    // action is here
    console.log(`delete book with id: ${id}`);
  };

}
