import { Component, Inject, OnChanges, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { DataService } from '../../services/data-service/data.service';
import { User } from '../../interfaces/intefaces';
import { DataExchangeService } from '../../services/data-exchange/data-exchange.service';

@Component({
  selector: 'app-profile-page',
  standalone: true,
  imports: [RouterLink],
  providers: [DataService],
  templateUrl: './profile-page.component.html',
  styleUrl: './profile-page.component.css'
})
export class ProfilePageComponent { 

  curentUserNickname: string = '';
  currentUser: User = {
    nickname: '',
    password: '',
    name: '',
    surename: '',
    birthday: '',
    address: ''
  };

  constructor(@Inject(DataExchangeService) private dataExchangeService: DataExchangeService) {}

  ngOnInit() {
    this.currentUser = this.dataExchangeService.CurrentUser;
    console.log(this.dataExchangeService.CurrentUser)
  }
  
}
