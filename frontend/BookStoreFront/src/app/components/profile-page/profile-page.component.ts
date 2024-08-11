import { Component, Inject, OnChanges, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { DataService } from '../../services/data.service';
import { User } from '../../interfaces/intefaces';

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

  constructor(@Inject(DataService) private dataService: DataService) {}

  ngOnInit() {
    this.currentUser = this.dataService.getCurrentUser();
    console.log(this.dataService.getCurrentUser())
  }
  
}
