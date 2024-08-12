import { Injectable } from '@angular/core';
import { ErrorResponce, User } from '../../interfaces/intefaces';

@Injectable({
  providedIn: 'root'
})
export class DataExchangeService {

  constructor() { }

  public CurrentUser! : User;

  public Errors! : string[];
}
