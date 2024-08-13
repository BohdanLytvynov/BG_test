import { Injectable } from '@angular/core';
import { ReqError, User } from '../../interfaces/intefaces';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataExchangeService {

public userTransfer$ = new Subject<User>();
public errorTransfer$ = new Subject<ReqError>();  
  
}
