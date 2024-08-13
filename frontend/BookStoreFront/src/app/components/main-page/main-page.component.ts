import { Component, Inject, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { Route, Router, RouterLink } from '@angular/router';
import { BooksComponent } from '../books/books.component';
import { AuthorsComponent } from '../authors/authors.component';
import { DataService } from '../../services/data-service/data.service';
import { HttpResponse } from '@angular/common/http';
import { DataExchangeService } from '../../services/data-exchange/data-exchange.service';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-main-page',
  standalone: true,
  imports: [RouterLink, BooksComponent, AuthorsComponent],
  templateUrl: './main-page.component.html',
  styleUrl: './main-page.component.css'
})
export class MainPageComponent implements OnInit {

  constructor(
    @Inject(DataService) private dataService : DataService,
    @Inject(Router) private router : Router,
    @Inject(DataExchangeService) private dataExchange : DataExchangeService
  )
  {}

  ngOnInit(): void {
    
  }
        
  onLogOutClicked()
  {
    // this.dataService.logoutUser().subscribe(
    //   resp =>
    //   {       
    //       this.router.navigate(["/"]);                      
    //   }
    // );

    this.router.navigate(["/"]); 
  }

}
