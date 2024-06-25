import { Component } from '@angular/core';

import { BookListComponent } from './features/pages/admin/book/book-list/book-list.component';

import { ToastrModule } from 'ngx-toastr';

import { LayoutComponent } from './shared/layout/layout.component';
import { HomepageComponent } from './shared/homepage/homepage.component';
import { LoadingComponent } from "./core/components/loading/loading.component";
import { TopbarComponent } from "./shared/layout/topbar/topbar.component";
import { MiddlebarComponent } from "./shared/layout/middlebar/middlebar.component";
import { BottombarComponent } from "./shared/layout/bottombar/bottombar.component";
import { NavigationEnd, Router, RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';







@Component({
    selector: 'app-root',
    standalone: true,
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss' ],
    imports: [RouterOutlet,CommonModule, BookListComponent, ToastrModule, LayoutComponent, LoadingComponent, TopbarComponent, MiddlebarComponent, BottombarComponent]
})
export class AppComponent {
  showTopbar:boolean=true;
  showBottombar:boolean=true;
 
  constructor(private router: Router) {
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this.showTopbar = !['/login', '/register', '/admin', '/search-results?filter=${encodeURIComponent(this.bookFilter.trim())}' ].includes(event.urlAfterRedirects);
        this.showBottombar = !['/login', '/register', '/admin'].includes(event.urlAfterRedirects);
      
      }
    });
  }




}