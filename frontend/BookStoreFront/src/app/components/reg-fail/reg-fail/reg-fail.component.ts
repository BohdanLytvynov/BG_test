import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { map, Observable } from 'rxjs';


@Component({
  selector: 'app-reg-fail',
  standalone: true,
  imports: [],
  templateUrl: './reg-fail.component.html',
  styleUrl: './reg-fail.component.css'
})
export class RegFailComponent  {
  state$!: Observable<object>;
  
  constructor( private route: Router) {
        
  }
  
  backToRegister()
  {
    this.route.navigate(['/start']);
  }
      
}
