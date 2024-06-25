import { CommonModule } from '@angular/common';
import { Component, HostListener } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { Member } from '../../../features/models/member';
import { TokenService } from '../../../core/services/token.service';
import { AuthService } from '../../../core/services/Auth.service';
import { FilterBookListForIsbnPipePipe } from '../../../core/pipes/FilterBookListForIsbnPipe.pipe';
import { FormsModule } from '@angular/forms';


@Component({
  selector: 'app-middlebar',
  standalone: true,
  imports: [RouterModule, CommonModule, FormsModule],
  templateUrl: './middlebar.component.html',
  styleUrl: './middlebar.component.scss'
})
export class MiddlebarComponent {

  loggedInMember: Member | null=null;
  //showAdminIcon:boolean= false;
  bookFilter: string = '';
constructor(public tokenService: TokenService, private router: Router, public authService: AuthService){}



  isLoggedIn():boolean{
    this.loggedInMember=this.authService.loggedInMember;
    return this.tokenService.hasToken();
   
  }

  logOut():void{
    this.authService.loggedInMember=null;
    this.tokenService.removeToken();
    this.router.navigateByUrl('/login');
    }


   /*  isAuthenticated(){
   if(this.authService.isAuthenticated()){
    return true;
   }
   else{
    return false;
   }

  }; */

 /*  isMenuOpen: boolean=false;
  toggleMenu(){
    this.isMenuOpen= !this.isMenuOpen;
  }
  @HostListener('document:mouseover', ['$event'])
  onMouseOver(event: MouseEvent) {
    const targetElement = event.target as HTMLElement;
    if (!targetElement.closest('.navbar')) {
      this.isMenuOpen = false;
    }
  } */

  onSearch(): void {
    this.router.navigate(['/search-results'], { queryParams: { filter: this.bookFilter } });
  }
  onSearchh(): void {
    if (this.bookFilter.trim()) {
      window.open(`/search-results?filter=${encodeURIComponent(this.bookFilter.trim())}`, '_blank');
    }
  } 
}
