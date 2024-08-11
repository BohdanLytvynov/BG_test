import { Component, EventEmitter, Inject, Input, Output } from '@angular/core';
import { NgClass } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { DataService } from '../../services/data.service';
import { User } from '../../interfaces/intefaces';

// class User {
//   constructor(
//     public nickname: string,
//     public password: string,
//     public name: string,
//     public surename: string,
//     public birthday: string,
//     public address: string
//   ) {}
// }

@Component({
  selector: 'app-sign-up',
  standalone: true,
  imports: [NgClass, FormsModule],
  providers: [DataService],
  templateUrl: './sign-up.component.html',
  styleUrl: './sign-up.component.css'
})

export class SignUpComponent {

  constructor(
    private router: Router, 
    @Inject(DataService) private dataService: DataService
  ) {}

  @Input() show = false;
  @Output() onChange = new EventEmitter<boolean>();

  handleClose(value: boolean) {
    this.onChange.emit(value);
  };

  nickname: string = '';
  password: string = '';
  name: string = '';
  surename: string = '';
  birthday: string = '';
  address: string = '';

  addUser() {
    const user: User = {
      nickname: this.nickname,
      password: this.password,
      name: this.name,
      surename: this.surename,
      birthday: this.birthday,
      address: this.address
    };
    
    // action is here
    this.dataService.addUser(user)
    console.log(user)

    this.handleClean()
    this.router.navigate(["/main"])
  };

  handleClean() {
    this.nickname = '';
    this.password = '';
    this.name = '',
    this.surename = '',
    this.birthday = '',
    this.address = ''
  }

}
