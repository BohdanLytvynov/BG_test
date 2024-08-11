import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { BooksComponent } from '../books/books.component';
import { AuthorsComponent } from '../authors/authors.component';

@Component({
  selector: 'app-main-page',
  standalone: true,
  imports: [RouterLink, BooksComponent, AuthorsComponent],
  templateUrl: './main-page.component.html',
  styleUrl: './main-page.component.css'
})
export class MainPageComponent {

}
