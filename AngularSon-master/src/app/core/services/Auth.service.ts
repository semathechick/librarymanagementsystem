import { Injectable } from '@angular/core';
import { Member } from '../../features/models/member';
import { JWT_ROLES } from '../constants/jwtAttributes';
import { TokenService } from './token.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private isLoggedIn = false;
 private _loggedInMember: Member | null = null;
  
  constructor(private tokenService:TokenService) { }

  login() {
    this.isLoggedIn = true;
    localStorage.setItem('isLoggedIn', 'true'); // Oturum durumunu localStorage'a kaydet
    
  }

  logout() {
    if (this.isLoggedIn) {
      this.isLoggedIn = false;
      localStorage.removeItem('isLoggedIn'); // Oturum durumunu localStorage'dan kaldır
      localStorage.clear(); // Diğer localStorage verilerini temizle
    }
  }
  
  isAuthenticated(): boolean {
    return JSON.parse(localStorage.getItem('isLoggedIn') || 'false' );
  }
  
  get loggedInMember(): Member| null{
    if(!this._loggedInMember){
      const memberData= localStorage.getItem('loggedInMember');
      this._loggedInMember=memberData? JSON.parse(memberData):null;

    }
    return this._loggedInMember;
  }
 
  set loggedInMember(member: Member| null){
    this._loggedInMember=member;
    if(member){
      localStorage.setItem('loggedInMember',JSON.stringify(member));
    }
    else{
      localStorage.removeItem('loggedInMember');
    }
  }
}


