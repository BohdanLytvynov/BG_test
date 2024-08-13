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
export class ProfilePageComponent implements OnInit { 

  private subs! : Subscription;

  curentUserNickname: string = '';
  currentUser: User = {
    nickname: '',
    password: '',
    name: '',
    surename: '',
    birthday: '',
    address: ''
  };

  constructor(@Inject(DataExchangeService) private dataExchangeService: DataExchangeService) {
    // this.dataExchangeService.userTransfer$
    //  .subscribe((user) => this.currentUser = user)
  }
  
  ngOnInit() {
     this.subs = this.dataExchangeService.userTransfer$
     .subscribe((user) => this.currentUser = user)
  }
    

}
