import { Component, ElementRef, EventEmitter, Inject, Input, OnInit, Output, ViewChild } from '@angular/core';
import { NgClass } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ValidatorBase } from '../../services/validation/validation';
import { ValidationService } from '../../services/validation/validation.service';
import { AuthResponse, LoginUser, User } from '../../interfaces/intefaces';
import { DataService } from '../../services/data-service/data.service';
import { DataExchangeService } from '../../services/data-exchange/data-exchange.service';
import { CookieService } from 'ngx-cookie-service';
import { HttpResponse } from '@angular/common/http';

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

    // this.dataService.loginUser<AuthResponse>(user).subscribe(
    //   (resp) =>
    //   {                  
    //       let authDto = resp!;
    //       this.dataExchangeService.CurrentUser = { 
    //         name : authDto.name, 
    //         surename : authDto.surename
    //       , birthday : authDto.birthday, 
    //         password : '', 
    //         nickname : authDto.nickname,
    //         address : authDto.address }
           
    //         this.handleClean();
    //         this.router.navigate(["/main"]);        
    //   }      
    // );
            let u : User = { nickname: 'test', name: 'testName', surename:'surename',
              birthday:'test', password : '', address : 'Some address'
             }        

            this.dataExchangeService.userTransfer$.next(u);
            this.handleClean();
            this.router.navigate(["/main"]);   
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
