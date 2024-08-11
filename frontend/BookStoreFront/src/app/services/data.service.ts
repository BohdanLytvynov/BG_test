import { inject, Injectable } from '@angular/core';
import { authors, books, users, currentUser } from '../data/mockData';
import { Author, Book, RegisterResponse, User } from '../interfaces/intefaces';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor() { }

  private authors: Author[] = authors;
  private books: Book[] = books;
  private users: User[] = users;
  private currentUser: User = currentUser;

  

  private ApiRequest : string = "https://localhost:7230/api/";
  private httpClient : HttpClient = inject(HttpClient);

  getAutors(): Author[] {
    // create a request to your server and return data
    return this.authors
  };

  getBooks(): Book[] {
    // create a request to your server and return data
    return this.books
  };

  getAuthorByID(id: string) {
    return this.authors.filter(item => item.authorID === id)[0];
  };

  getBookByID(id: string) {
    return this.books.filter(item => item.bookID === id)[0];
  };

  registerUser(user: User) : RegisterResponse {
    this.users.push(user);
        
    let responceObj : RegisterResponse = { success: false };

    this.httpClient.post<RegisterResponse>(this.ApiRequest + "Accounts/Register", user, 
      { 
        headers: { "Content-Type": "application/json" } 
      }).subscribe( response => { responceObj = response } );
      
      return responceObj;
  };

  getCurrentUser() {
    return this.currentUser;
  };

}
