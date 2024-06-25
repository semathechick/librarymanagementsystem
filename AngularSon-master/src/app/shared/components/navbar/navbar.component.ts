import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { TokenService } from '../../../core/services/token.service';
import { AuthService } from '../../../core/services/Auth.service';
import { Member } from '../../../features/models/member';


@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule,RouterModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss',

})
export class NavbarComponent {
constructor(private tokenService:TokenService,private router:Router,public authService:AuthService){}
loggedInMember: Member | null = null;
isLoggedIn():boolean{
  this.loggedInMember=this.authService.loggedInMember;
  return this.tokenService.hasToken();//Token varsa true yoksa false dönecek.
  
}
logOut():void{
  this.authService.loggedInMember=null;
  this.tokenService.removeToken();
  this.router.navigateByUrl('/login');
}


}
