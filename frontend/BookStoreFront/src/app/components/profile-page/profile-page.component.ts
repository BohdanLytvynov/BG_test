import { Component, Inject, OnChanges, OnDestroy, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { DataService } from '../../services/data-service/data.service';
import { User } from '../../interfaces/intefaces';
import { DataExchangeService } from '../../services/data-exchange/data-exchange.service';
import { Subject, Subscription } from 'rxjs';
import { currentUser } from '../../data/mockData';

@Component({
  selector: 'app-profile-page',
  standalone: true,
  imports: [RouterLink],
  providers: [DataService],
  templateUrl: './profile-page.component.html',
  styleUrl: './profile-page.component.css'
})
export class ProfilePageComponent implements OnInit  { 
  
  curentUserNickname: string = '';
  currentUser: User = {
    nickname: '',
    password: '',
    name: '',
    surename: '',
    birthday: '',
    address: ''
  };

  constructor(@Inject(DataExchangeService) private dataExchangeService: DataExchangeService) 
  {
    
  }

  ngOnInit(): void {
    if(this.dataExchangeService.userTransfer != null)
      this.currentUser = this.dataExchangeService.userTransfer;
  }
  
        
}
