import { Component, ElementRef, EventEmitter, Inject, Input, OnInit, Output, ViewChild } from '@angular/core';
import { NgClass } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ValidatorBase } from '../../services/validation/validation';
import { ValidationService } from '../../services/validation/validation.service';
import { AuthResponse, LoginUser } from '../../interfaces/intefaces';
import { DataService } from '../../services/data-service/data.service';
import { DataExchangeService } from '../../services/data-exchange/data-exchange.service';
import { CookieService } from 'ngx-cookie-service';

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

export class LogInComponent extends ValidatorBase implements OnInit {

  constructor(private router: Router, 
    @Inject(ValidationService) private validService : ValidationService,
    @Inject(DataService) private dataService : DataService,
    @Inject(DataExchangeService) private dataExchangeService : DataExchangeService,
    @Inject(CookieService) private cookieService : CookieService
  ) 
  {
    super();
  }
  ngOnInit(): void {
    this.Init(2);    
  }

  @Input() show = false;
  @Output() onChange = new EventEmitter<boolean>();  

  handleClose(value: boolean) {
    this.onChange.emit(value);
  }

  nickname: string = '';
  password: string = '';

  all_correct : boolean = false;
  
  handleLogIn() {

    if(!this.all_correct)
      return;

    let user : LoginUser = { nickname: this.nickname, password : this.password }

    this.dataService.loginUser<AuthResponse>(user).subscribe(
      (resp) =>
      {          
        if(resp.status)
        {
          this.dataExchangeService.CurrentUser = { 
            name : resp.name, surename : resp.surename
          , birthday : resp.birthday, password : '', nickname : resp.nickname,
            address : resp.address }

            let cookies = this.cookieService.getAll();

            this.handleClean();
            this.router.navigate(["/main"]);
        }
      },
      err => 
        {

        }
    );
            
    
  };

  handleClean() {
    this.nickname = '';
    this.password = '';
  }    

  loginChanged(value : string) : void
  {
    this.validArray[0] = this.validService.ValidateTextNotEmpty(value);
    this.all_correct = this.CheckValidArray();    
  }

  passwordChanged(value : string) : void
  {
    this.validArray[1] = this.validService.ValidateTextNotEmpty(value);
    this.all_correct = this.CheckValidArray();    
  }

}
