import { Component, inject, Inject, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { map, Observable, Subject, Subscription } from 'rxjs';
import { DataExchangeService } from '../../../services/data-exchange/data-exchange.service';
import { ReqError } from '../../../interfaces/intefaces';


@Component({
  selector: 'app-reg-fail',
  standalone: true,
  imports: [],
  templateUrl: './reg-fail.component.html',
  styleUrl: './reg-fail.component.css'
})
export class RegFailComponent implements OnInit {
  error! : ReqError;
  sub! : Subscription    

  constructor( private route: Router,
    @Inject(DataExchangeService) private dataExchange : DataExchangeService
  ) 
  {
   
  }
  ngOnInit(): void {
    if(this.dataExchange.errorTransfer != null)
      this.error = this.dataExchange.errorTransfer;
  }
        
  backToRegister()
  {
    this.route.navigate(['/start']);
  }      
}
