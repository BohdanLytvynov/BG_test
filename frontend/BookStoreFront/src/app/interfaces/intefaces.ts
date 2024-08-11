export interface Author {
  authorID: string;
  authorName: string;
  authorSureName: string;
  authorBirthday: string;
}

export interface Book {
  bookID: string;
  bookName: string;
  bookYear: string;
  bookGenre: string;
}

export interface User {
  nickname: string;
  password: string;
  name: string;
  surename: string;
  birthday: string;
  address: string;
}