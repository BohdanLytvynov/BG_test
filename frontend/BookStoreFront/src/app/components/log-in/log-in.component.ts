import { Component, EventEmitter, Input, Output } from '@angular/core';
import { NgClass } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

class UserLogIn {
  constructor(
    public nickname: string,
    public password: string
  ) {}
}

@Component({
  selector: 'app-log-in',
  standalone: true,
  imports: [NgClass, FormsModule],
  templateUrl: './log-in.component.html',
  styleUrl: './log-in.component.css'
})

export class LogInComponent {

  constructor(private router: Router) {}

  @Input() show = false;
  @Output() onChange = new EventEmitter<boolean>();

  handleClose(value: boolean) {
    this.onChange.emit(value);
  }

  nickname: string = '';
  password: string = '';

  loginedUser = {
    nickname: this.nickname,
    password: this.password
  }

  handleLogIn() {
    this.loginedUser.nickname = this.nickname;
    this.loginedUser.password = this.password;

    // action is here
    console.log(this.loginedUser)
    
    this.handleClean();
    this.router.navigate(["/main"]);
  };

  handleClean() {
    this.nickname = '';
    this.password = '';
    this.loginedUser.nickname = '';
    this.loginedUser.password = '';
  }

}
