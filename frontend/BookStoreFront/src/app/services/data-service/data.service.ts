import { inject, Injectable } from '@angular/core';
import { authors, books, users, currentUser } from '../../data/mockData';
import { Author, Book, AuthResponse, User, LoginUser } from '../../interfaces/intefaces';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor() { }

  private authors: Author[] = authors;
  private books: Book[] = books;
  private ApiHttp = "http://localhost:5154/api/";
  private ApiHttps : string = "https://localhost:7230/api/";
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

  registerUser<T>(user: User) : Observable<T> {                    
    return this.httpClient.post<T>(this.ApiHttp + "Accounts/Register", user, 
      { 
        headers: { "Content-Type": "application/json" } 
      });          
  };

  loginUser<T>(user : LoginUser) : Observable<T>
  {      
      return this.httpClient.post<T>(this.ApiHttp + "Accounts/Login", user,
        {
          headers: { "Content-Type": "application/json"},
          withCredentials: true,           
        }
      );
  }

  logoutUser() : Observable<object>
  {
    return this.httpClient.get(this.ApiHttp + "Accounts/LogOut", 
      {
        withCredentials: true,
      }
    );
  }
  
}
