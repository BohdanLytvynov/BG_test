import { Component, inject, Inject, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { map, Observable, Subject } from 'rxjs';
import { DataExchangeService } from '../../../services/data-exchange/data-exchange.service';
import { ReqError } from '../../../interfaces/intefaces';


@Component({
  selector: 'app-reg-fail',
  standalone: true,
  imports: [],
  templateUrl: './reg-fail.component.html',
  styleUrl: './reg-fail.component.css'
})
export class RegFailComponent implements OnInit, OnDestroy {
  error! : ReqError;
      
  constructor( private route: Router,
    @Inject(DataExchangeService) private dataExchange : DataExchangeService
  ) {
        
  }
  ngOnDestroy(): void {
    this.dataExchange.errorTransfer$.unsubscribe();
  }
  


  ngOnInit(): void {
      this.dataExchange.errorTransfer$.subscribe(
        err => 
        {
          this.error = err;
        }
      );
  }
  
  backToRegister()
  {
    this.route.navigate(['/start']);
  }
      
}
