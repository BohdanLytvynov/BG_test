import { Component, Inject } from '@angular/core';
import { AuthorFormComponent } from '../author-form/author-form.component';
import { DataService } from '../../services/data.service';
import { Author } from '../../interfaces/intefaces';
import { AutorEditFormComponent } from '../author-edit-form/autor-edit-form.component';

@Component({
  selector: 'app-authors',
  standalone: true,
  imports: [AuthorFormComponent, AutorEditFormComponent],
  providers: [DataService],
  templateUrl: './authors.component.html',
  styleUrl: './authors.component.css'
})
export class AuthorsComponent {

  constructor(@Inject(DataService) private dataService: DataService) {};

  showAuthorForm = false;
  authorForm = false;

  showAuthorFormComponent() {
    this.authorForm = true;
    this.showAuthorForm = true;
  }

  hideAuthorFormComponent(value: boolean) {
    this.showAuthorForm = value;
  }

  showAuthorEditForm = false;
  authorEditForm = false;
  currentID = '';

  showAuthorEditFormComponent(id: string) {
    this.currentID = id;
    this.authorEditForm = true;
    this.showAuthorEditForm = true;
  };

  hideAuthorEditFormComponent() {
    this.showAuthorEditForm = false;
  };

  items: Author[] = [];

  ngOnInit() {
    this.items = this.dataService.getAutors();
  }

  ngOnChange() {
    this.items = this.dataService.getAutors();
  }

  deleteAuthor(id: string) {
    this.items = this.items.filter(item => item.authorID !== id);
    
    // action is here
    console.log(`delete author with id: ${id}`);
  };

}
